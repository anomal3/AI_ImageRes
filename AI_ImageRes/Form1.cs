using System.Diagnostics;
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

            //Версия CPU
            //var context = new MLContext();

            //engine = context.Model.CreatePredictionEngine<EnviromentModel.ModelInput, EnviromentModel.ModelOutput>(
            //    context.Model.Load(Path.GetFullPath("EnviromentModel.zip"), out _));

            Settings.Default.WelcomeStart++;
            Settings.Default.Save();
        }

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

            pic.Image = Image.FromFile(file);
            pic.SizeMode = PictureBoxSizeMode.Zoom;


            var time = Stopwatch.StartNew();
            var imageBytes = File.ReadAllBytes(file);

            EnviromentModel.ModelInput result = new EnviromentModel.ModelInput()
            {
                ImageSource = imageBytes,
            };

            var sortedScoresWithLabel = EnviromentModel.PredictAllLabels(result);
            var model = sortedScoresWithLabel.OrderByDescending(x => x.Value).First();

            lblResult.Text = $@"Это {model.Key}  - {model.Value:p0} затрачено на распознование ~{Math.Round(time.Elapsed.TotalSeconds, 2)} сек";

            //Версия CPU
            //var result = engine.Predict(new EnviromentModel.ModelInput
            //{
            //    ImageSource = imageBytes
            //});
            //lblResult.Text = $@"Это {result.PredictedLabel}  - {result.Score.Max():p0}% затрачено на распознование ~{Math.Round(time.Elapsed.TotalSeconds, 2)} сек";

        }
    }
}