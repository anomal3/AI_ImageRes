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
                MessageBox.Show(Settings.Default.WelcomeMessage, "���������! ����� �����", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //������ CPU
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
                Title = "�������� ��������",
                Filter = "�������� (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp",
                CheckFileExists = true
            };

            if(dialog.ShowDialog() == DialogResult.Cancel) return;

            lblResult.Text = "��������...";
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

            lblResult.Text = $@"��� {model.Key}  - {model.Value:p0} ��������� �� ������������� ~{Math.Round(time.Elapsed.TotalSeconds, 2)} ���";

            //������ CPU
            //var result = engine.Predict(new EnviromentModel.ModelInput
            //{
            //    ImageSource = imageBytes
            //});
            //lblResult.Text = $@"��� {result.PredictedLabel}  - {result.Score.Max():p0}% ��������� �� ������������� ~{Math.Round(time.Elapsed.TotalSeconds, 2)} ���";

        }
    }
}