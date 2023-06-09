using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using AI_ImageRes.Properties;
using Microsoft.ML;
using Timer = System.Threading.Timer;

namespace AI_ImageRes
{
    public partial class Form1 : Form
    {
        #region .ctor

        public Form1()
        {
            InitializeComponent();
            if (Settings.Default.WelcomeStart == 0)
                MessageBox.Show(Settings.Default.WelcomeMessage, "���������! ����� �����", MessageBoxButtons.OK, MessageBoxIcon.Information);

            loaded_event += FileLoadedEvent;

            //������ CPU
            //var context = new MLContext();

            //engine = context.Model.CreatePredictionEngine<EnviromentModel.ModelInput, EnviromentModel.ModelOutput>(
            //    context.Model.Load("D:\\MLTU\\testModel.mlnet", out _));

            Settings.Default.WelcomeStart++;
            Settings.Default.Save();

            pic.AllowDrop = true;
            pic.BackColor = Color.White;

            txtPathModel.Text = string.IsNullOrWhiteSpace(Settings.Default.ModelPath) ? "�������� ����� � ������� ���� �� ����� � �������" : Settings.Default.ModelPath;

            if (!string.IsNullOrWhiteSpace(Settings.Default.ModelPath)) EnviromentModel.MLNetModelPath = Settings.Default.ModelPath;
        }

        #endregion

        #region Variables

        private readonly PredictionEngine<EnviromentModel.ModelInput, EnviromentModel.ModelOutput> engine;
        delegate void FileLoaded(string _filePath);
        event FileLoaded loaded_event;

        #endregion

        #region Drag'n'Drop

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


        #endregion

        #region Event


        private async void bOpen_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "�������� ��������",
                Filter = "�������� (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp",
                CheckFileExists = true
            };

            if (dialog.ShowDialog() == DialogResult.Cancel) return;

            lblResult.Text = "��������...";
            await Task.Delay(1);

            var file = dialog.FileName;

            loaded_event.Invoke(file);
            //������ CPU
            //var result = engine.Predict(new EnviromentModel.ModelInput
            //{
            //    ImageSource = imageBytes
            //});
            //lblResult.Text = $@"��� {result.PredictedLabel}  - {result.Score.Max():p0}% ��������� �� ������������� ~{Math.Round(time.Elapsed.TotalSeconds, 2)} ���";

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

            lblResult.Text = $@"��� {model.Key}  - {model.Value:p0} ��������� �� ������������� ~{Math.Round(time.Elapsed.TotalSeconds, 2)} ���";
        }

        #endregion

        private void txtPathModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "ML.NET ������|*.mlnet",
                Title = "������� ������"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var modelFilePath = dialog.FileName;
                txtPathModel.Text = modelFilePath;
                Settings.Default.ModelPath = modelFilePath;
                Settings.Default.Save();
            }

            EnviromentModel.MLNetModelPath = Settings.Default.ModelPath;
        }
    }
}