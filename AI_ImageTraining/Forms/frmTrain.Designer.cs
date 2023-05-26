namespace AI_ImageTraining.Forms
{
    partial class frmTrain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrain));
            btnSelectRootFolder = new Button();
            btnSelectModel = new Button();
            btnFineTune = new Button();
            picImage = new PictureBox();
            txtRootFolder = new TextBox();
            progressBar = new ProgressBar();
            txtModelPath = new TextBox();
            txtProgress = new TextBox();
            predictButton = new Button();
            Update = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            SuspendLayout();
            // 
            // btnSelectRootFolder
            // 
            btnSelectRootFolder.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectRootFolder.Location = new Point(14, 299);
            btnSelectRootFolder.Margin = new Padding(4, 3, 4, 3);
            btnSelectRootFolder.Name = "btnSelectRootFolder";
            btnSelectRootFolder.Size = new Size(163, 27);
            btnSelectRootFolder.TabIndex = 0;
            btnSelectRootFolder.Text = "Папка для обучения";
            btnSelectRootFolder.UseVisualStyleBackColor = true;
            btnSelectRootFolder.Click += btnSelectRootFolder_Click;
            // 
            // btnSelectModel
            // 
            btnSelectModel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectModel.Enabled = false;
            btnSelectModel.Location = new Point(210, 299);
            btnSelectModel.Margin = new Padding(4, 3, 4, 3);
            btnSelectModel.Name = "btnSelectModel";
            btnSelectModel.Size = new Size(163, 27);
            btnSelectModel.TabIndex = 0;
            btnSelectModel.Text = "Выбрать модель";
            btnSelectModel.UseVisualStyleBackColor = true;
            btnSelectModel.Click += btnSelectModel_Click;
            // 
            // btnFineTune
            // 
            btnFineTune.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFineTune.Location = new Point(416, 299);
            btnFineTune.Margin = new Padding(4, 3, 4, 3);
            btnFineTune.Name = "btnFineTune";
            btnFineTune.Size = new Size(163, 27);
            btnFineTune.TabIndex = 0;
            btnFineTune.Text = "Начать обучение";
            btnFineTune.UseVisualStyleBackColor = true;
            btnFineTune.Click += btnFineTune_ClickAsync;
            // 
            // picImage
            // 
            picImage.BorderStyle = BorderStyle.FixedSingle;
            picImage.Location = new Point(14, 14);
            picImage.Margin = new Padding(4, 3, 4, 3);
            picImage.Name = "picImage";
            picImage.Size = new Size(359, 203);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 1;
            picImage.TabStop = false;
            // 
            // txtRootFolder
            // 
            txtRootFolder.Location = new Point(14, 223);
            txtRootFolder.Margin = new Padding(4, 3, 4, 3);
            txtRootFolder.Name = "txtRootFolder";
            txtRootFolder.ReadOnly = true;
            txtRootFolder.Size = new Size(359, 23);
            txtRootFolder.TabIndex = 2;
            txtRootFolder.Text = "Путь до папки";
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 335);
            progressBar.Margin = new Padding(4, 3, 4, 3);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(784, 16);
            progressBar.TabIndex = 3;
            // 
            // txtModelPath
            // 
            txtModelPath.Location = new Point(14, 252);
            txtModelPath.Margin = new Padding(4, 3, 4, 3);
            txtModelPath.Name = "txtModelPath";
            txtModelPath.ReadOnly = true;
            txtModelPath.Size = new Size(359, 23);
            txtModelPath.TabIndex = 4;
            txtModelPath.Text = "Путь до модели";
            // 
            // txtProgress
            // 
            txtProgress.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtProgress.Location = new Point(381, 14);
            txtProgress.Margin = new Padding(4, 3, 4, 3);
            txtProgress.Multiline = true;
            txtProgress.Name = "txtProgress";
            txtProgress.ReadOnly = true;
            txtProgress.ScrollBars = ScrollBars.Both;
            txtProgress.Size = new Size(392, 262);
            txtProgress.TabIndex = 5;
            txtProgress.Text = "Лог...";
            // 
            // predictButton
            // 
            predictButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            predictButton.Location = new Point(611, 299);
            predictButton.Margin = new Padding(4, 3, 4, 3);
            predictButton.Name = "predictButton";
            predictButton.Size = new Size(160, 27);
            predictButton.TabIndex = 6;
            predictButton.Text = "Протестировать обучение";
            predictButton.UseVisualStyleBackColor = true;
            predictButton.Click += predictButton_Click;
            // 
            // Update
            // 
            Update.Tick += Update_Tick;
            // 
            // frmTrain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 351);
            Controls.Add(predictButton);
            Controls.Add(txtProgress);
            Controls.Add(txtModelPath);
            Controls.Add(progressBar);
            Controls.Add(txtRootFolder);
            Controls.Add(picImage);
            Controls.Add(btnFineTune);
            Controls.Add(btnSelectModel);
            Controls.Add(btnSelectRootFolder);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(800, 390);
            Name = "frmTrain";
            Text = "Тренер личностного роста для ИИ";
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectRootFolder;
        private Button btnSelectModel;
        private Button btnFineTune;
        private PictureBox picImage;
        private TextBox txtRootFolder;
        private ProgressBar progressBar;
        private TextBox txtModelPath;
        private TextBox txtProgress;
        private Button predictButton;
        private System.Windows.Forms.Timer Update;
    }
}