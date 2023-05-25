using AI_ImageTraining.Classes;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using AI_ImageTraining.Classes;
using Microsoft.ML.Data;
using Microsoft.ML.Vision;
using System.Windows.Forms;
using Tensorflow.Keras.Engine;

namespace AI_ImageTraining.Forms
{
    public partial class frmTrain : Form
    {

        #region Variables

        private string rootFolderPath;
        private string modelFilePath;
        private MLContext mlContext;
        private ITransformer trainedModel;

        private List<string> imageFilePaths;
        private int currentImageIndex;

        #endregion
        public frmTrain()
        {
            InitializeComponent();
            mlContext = new MLContext();
        }

        private void btnSelectRootFolder_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog
            {
                Description = "Выберите корневую папку с обучающими изображениями"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rootFolderPath = dialog.SelectedPath;
                txtRootFolder.Text = rootFolderPath;
            }
        }

        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "ML.NET модель|*.mlnet",
                Title = "Выбрать модель"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                modelFilePath = dialog.FileName;
                txtModelPath.Text = modelFilePath;
            }
        }

        private async void btnFineTune_ClickAsync(object sender, EventArgs e)
        {
            // Загрузка данных из папки
            var folderPath = rootFolderPath;

            imageFilePaths = GetImageFilePaths(folderPath);

            progressBar.Minimum = 0;
            progressBar.Maximum = imageFilePaths.Count + 1;
            MessageBox.Show($"Итого {imageFilePaths.Count}");
            await Task.Delay(1000);
            progressBar.Value = 0;

            var trainingData = MLT.LoadImageFromFolder(mlContext, folderPath, progressBar, txtProgress);

            progressBar.Value = 0;
            // Создание модели
            trainedModel = await Task.Run(async () => MLT.RetrainModel(mlContext, trainingData, progressBar, txtProgress, rootFolderPath));

            MessageBox.Show("Обучение завершено.");

            // Очистка PictureBox и лога
            picImage.Image = null;
            txtProgress.Text = string.Empty;
            progressBar.Value = 0;
        }

        private ITransformer TrainModel(MLContext mlContext)
        {
            var pipeline = BuildPipeline(mlContext);

            for (int i = 0; i < imageFilePaths.Count; i++)
            {
                currentImageIndex = i;
                var currentImagePath = imageFilePaths[i];

                using (var stream = new FileStream(currentImagePath, FileMode.Open))
                {
                    picImage.Image = System.Drawing.Image.FromStream(stream);
                    UpdateLogTextBox($"Обучение на изображении: {currentImagePath}");
                }

                var trainData = LoadImageFromFile(mlContext, currentImagePath);
                trainedModel = pipeline.Fit(trainData);

                progressBar.Value = i + 1;
            }

            return trainedModel;
        }

        public void UpdateLogTextBox(string message)
        {
            if (txtProgress.InvokeRequired)
            {
                txtProgress.Invoke(new Action<string>(UpdateLogTextBox), message);
            }
            else
            {
                txtProgress.AppendText(message + Environment.NewLine);
            }
        }

        public static void UpdateLogTextBox(Form form, TextBox logTextBox, string message)
        {
            if (logTextBox.InvokeRequired)
            {
                logTextBox.Invoke(new Action(() =>
                {
                    logTextBox.AppendText(message + "\n");
                }));
            }
            else
            {
                logTextBox.AppendText(message + "\n");
            }
        }

        public static void UpdateProgressBar(Form form, ProgressBar progressBar, int value, int maximum)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action(() =>
                {
                    progressBar.Maximum = maximum;
                    progressBar.Value = value;
                }));
            }
            else
            {
                progressBar.Maximum = maximum;
                progressBar.Value = value;
            }
        }


        private IDataView LoadImageFromFile(MLContext mlContext, string imagePath)
        {
            var res = new List<MLT.ModelInput>();

            var imageLabel = Path.GetFileName(Path.GetDirectoryName(imagePath));
            var imageBytes = File.ReadAllBytes(imagePath);

            res.Add(new MLT.ModelInput
            {
                Label = imageLabel,
                ImageSource = imageBytes,
            });

            return mlContext.Data.LoadFromEnumerable(res);
        }

        private IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: "Label", addKeyValueAnnotationsAsText: false)
                .Append(mlContext.MulticlassClassification.Trainers.ImageClassification(labelColumnName: "Label", scoreColumnName: "Score", featureColumnName: "ImageSource"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel", inputColumnName: "PredictedLabel"));

            return pipeline;
        }

        private async void predictButton_Click(object sender, EventArgs e)
        {
            if (trainedModel == null)
            {
                MessageBox.Show("Модель не была обучена. Пожалуйста, выполните обучение модели.");
                return;
            }

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var imageFilePath = openFileDialog.FileName;

                var predictionEngine = mlContext.Model.CreatePredictionEngine<MLT.ModelInput, MLT.ModelOutput>(trainedModel);

                using (var stream = new FileStream(imageFilePath, FileMode.Open))
                {
                    var input = new MLT.ModelInput
                    {
                        ImageSource = new byte[stream.Length]
                    };

                    await stream.ReadAsync(input.ImageSource, 0, (int)stream.Length);

                    var prediction = predictionEngine.Predict(input);

                    var predictedLabel = prediction.PredictedLabel;

                    MessageBox.Show($"Предсказанная метка: {predictedLabel}");
                }
            }
        }

        private List<string> GetImageFilePaths(string folderPath)
        {
            var allowedImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".gif" };
            var rootDirectoryInfo = new DirectoryInfo(folderPath);
            var subDirectories = rootDirectoryInfo.GetDirectories();

            if (subDirectories.Length == 0)
            {
                throw new Exception("Не удалось найти подкаталоги.");
            }

            var filePaths = new List<string>();

            foreach (var directory in subDirectories)
            {
                var imageList = directory.EnumerateFiles().Where(f => allowedImageExtensions.Contains(f.Extension.ToLower()));
                if (imageList.Count() > 0)
                {
                    filePaths.AddRange(imageList.Select(i => i.FullName));
                }
            }

            return filePaths;
        }
    }
}


