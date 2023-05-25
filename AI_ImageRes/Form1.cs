using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using AI_ImageRes.Properties;
using Microsoft.ML;
using Timer = System.Threading.Timer;

namespace AI_ImageRes
{
    public partial class Form1 : Form
    {
        private readonly PredictionEngine<EnviromentModel.ModelInput, EnviromentModel.ModelOutput> engine;
        public Form1()
        {
            InitializeComponent();
            if(Settings.Default.WelcomeStart == 0)
                MessageBox.Show(Settings.Default.WelcomeMessage, "Осторожно! умная падла", MessageBoxButtons.OK, MessageBoxIcon.Information);

            loaded_event += FileLoadedEvent;

            //Версия CPU
            //var context = new MLContext();

            //engine = context.Model.CreatePredictionEngine<EnviromentModel.ModelInput, EnviromentModel.ModelOutput>(
            //    context.Model.Load(Path.GetFullPath("EnviromentModel.zip"), out _));

            Settings.Default.WelcomeStart++;
            Settings.Default.Save();

            pic.AllowDrop = true;
            pic.BackColor = Color.White;
        }

        #region Variables

        delegate void FileLoaded(string _filePath);
        event FileLoaded loaded_event;

        #endregion

        private async void bOpen_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Выьерите картинку",
                Filter = "Картинки (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp",
                CheckFileExists = true
            };

            if(dialog.ShowDialog() == DialogResult.Cancel) return;

            lblResult.Text = "Ожидайте...";
            await Task.Delay(1);

            var file = dialog.FileName;
            
            loaded_event.Invoke(file);
            //Версия CPU
            //var result = engine.Predict(new EnviromentModel.ModelInput
            //{
            //    ImageSource = imageBytes
            //});
            //lblResult.Text = $@"Это {result.PredictedLabel}  - {result.Score.Max():p0}% затрачено на распознование ~{Math.Round(time.Elapsed.TotalSeconds, 2)} сек";

        }

        private void pic_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                pic.BackColor = Color.FromKnownColor(KnownColor.Control);
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void pic_DragLeave(object sender, EventArgs e)
        {
            pic.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
        }

        private void pic_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var file = files[0].ToString();

            loaded_event.Invoke(file);
        }


        private void FileLoadedEvent(string _filePath)
        {
            if (string.IsNullOrWhiteSpace(_filePath)) return;

            pic.Image = Image.FromFile(_filePath);
            pic.SizeMode = PictureBoxSizeMode.Zoom;

            var time = Stopwatch.StartNew();
            var imageBytes = File.ReadAllBytes(_filePath);

            EnviromentModel.ModelInput result = new EnviromentModel.ModelInput()
            {
                ImageSource = imageBytes,
            };

            var sortedScoresWithLabel = EnviromentModel.PredictAllLabels(result);
            var model = sortedScoresWithLabel.OrderByDescending(x => x.Value).First();

            lblResult.Text = $@"Это {model.Key}  - {model.Value:p0} затрачено на распознование ~{Math.Round(time.Elapsed.TotalSeconds, 2)} сек";
        }

    }
}