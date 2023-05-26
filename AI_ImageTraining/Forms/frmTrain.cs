using AI_ImageTraining.Classes;
using Microsoft.ML;
using AI_ImageRes;
using System.Windows.Forms;
using System.Threading;

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
            Update.Start();
        }

        private void btnSelectRootFolder_Click(object sender, EventArgs e)
        {
            txtProgress.Text = string.Empty;

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
            btnFineTune.Enabled = false;
            btnSelectRootFolder.Enabled = false;
            predictButton.Enabled = false;

            // Загрузка данных из папки
            var folderPath = rootFolderPath;

            imageFilePaths = GetImageFilePaths(folderPath);

            progressBar.Minimum = 0;
            progressBar.Maximum = imageFilePaths.Count + 1;

            var trainingData = await MLT.LoadImageFromFolder(mlContext, folderPath, progressBar, txtProgress);
            await Task.Delay(1000);
            progressBar.Style = ProgressBarStyle.Marquee;
            UpdateLogTextBox("Ждите... Идёт обучение модели");
            progressBar.Value = 0;


            var dialog = new SaveFileDialog()
            {
                Filter = "ML.NET модель|*.mlnet",
                Title = "Выбрать место для сохранения модели"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var modelFilePath = dialog.FileName;

                Thread thread = new Thread((async () =>
                {
                    #region Имитация...
                    //TODO : Отловить прогресс
                    foreach (string imageFilePath in imageFilePaths)
                    {
                        await Task.Delay(40);
                        picImage.Image = Image.FromFile(imageFilePath);
                        await Task.Delay(40);
                        Bitmap originalImage = new Bitmap(imageFilePath);
                        Bitmap processedImage = new Bitmap(originalImage.Width, originalImage.Height);
                        AdjustBrightnessContrast(originalImage, processedImage, 11f, 2.5f);
                        picImage.Image = processedImage;
                    }

                    #endregion
                }));
                thread.Start();
                
                // Создание модели
                trainedModel = await Task.Run(async () =>
                    MLT.RetrainModel(mlContext, trainingData, progressBar, txtProgress, modelFilePath));
            }

            UpdateLogTextBox("\"Обучение завершено.\"");

            btnFineTune.Enabled = true;
            btnSelectRootFolder.Enabled = true;
            predictButton.Enabled = true;

            progressBar.Style = ProgressBarStyle.Blocks;
            // Очистка PictureBox и лога
            picImage.Image = null;
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
                    logTextBox.AppendText(message + "\r\n");
                }));
            }
            else
            {
                logTextBox.AppendText(message + "\r\n");
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

        private void Update_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DataStatic.logUpdate))
                txtProgress.AppendText(DataStatic.logUpdate + "\r\n");
        }

        private void AdjustBrightnessContrast(Bitmap sourceImage, Bitmap destinationImage, float brightness, float contrast)
        {
            // Получение ширины и высоты изображения
            int width = sourceImage.Width;
            int height = sourceImage.Height;

            // Итерация по пикселям изображения
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Получение цвета пикселя
                    Color originalColor = sourceImage.GetPixel(x, y);

                    // Изменение яркости и контрастности пикселя
                    int newRed = (int)(originalColor.R * 0.299 + originalColor.G * 0.587 + originalColor.B * 0.114 * contrast + brightness);
                    int newGreen = (int)(originalColor.R * 0.299 + originalColor.G * 0.587 + originalColor.B * 0.114 * contrast + brightness);
                    int newBlue = (int)(originalColor.R * 0.299 + originalColor.G * 0.587 + originalColor.B * 0.114 * contrast + brightness);

                    // Ограничение значений цветов в пределах 0-255
                    newRed = Math.Max(0, Math.Min(255, newRed));
                    newGreen = Math.Max(0, Math.Min(255, newGreen));
                    newBlue = Math.Max(0, Math.Min(255, newBlue));

                    // Создание нового цвета с измененными значениями
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);

                    // Установка нового цвета пикселя в обработанном изображении
                    destinationImage.SetPixel(x, y, newColor);

                    // Установка нового цвета пикселя в обработанном изображении
                    destinationImage.SetPixel(x, y, newColor);
                }
            }
        }
    }
}


