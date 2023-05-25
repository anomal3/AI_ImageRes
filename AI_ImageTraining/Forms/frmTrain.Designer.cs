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
            this.btnSelectRootFolder = new System.Windows.Forms.Button();
            this.btnSelectModel = new System.Windows.Forms.Button();
            this.btnFineTune = new System.Windows.Forms.Button();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtModelPath = new System.Windows.Forms.TextBox();
            this.txtProgress = new System.Windows.Forms.TextBox();
            this.predictButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectRootFolder
            // 
            this.btnSelectRootFolder.Location = new System.Drawing.Point(12, 411);
            this.btnSelectRootFolder.Name = "btnSelectRootFolder";
            this.btnSelectRootFolder.Size = new System.Drawing.Size(140, 23);
            this.btnSelectRootFolder.TabIndex = 0;
            this.btnSelectRootFolder.Text = "Папка для обучения";
            this.btnSelectRootFolder.UseVisualStyleBackColor = true;
            this.btnSelectRootFolder.Click += new System.EventHandler(this.btnSelectRootFolder_Click);
            // 
            // btnSelectModel
            // 
            this.btnSelectModel.Location = new System.Drawing.Point(158, 411);
            this.btnSelectModel.Name = "btnSelectModel";
            this.btnSelectModel.Size = new System.Drawing.Size(140, 23);
            this.btnSelectModel.TabIndex = 0;
            this.btnSelectModel.Text = "Выбрать модель";
            this.btnSelectModel.UseVisualStyleBackColor = true;
            this.btnSelectModel.Click += new System.EventHandler(this.btnSelectModel_Click);
            // 
            // btnFineTune
            // 
            this.btnFineTune.Location = new System.Drawing.Point(304, 411);
            this.btnFineTune.Name = "btnFineTune";
            this.btnFineTune.Size = new System.Drawing.Size(140, 23);
            this.btnFineTune.TabIndex = 0;
            this.btnFineTune.Text = "Начать дообучение";
            this.btnFineTune.UseVisualStyleBackColor = true;
            this.btnFineTune.Click += new System.EventHandler(this.btnFineTune_ClickAsync);
            // 
            // picImage
            // 
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Location = new System.Drawing.Point(12, 12);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(432, 393);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 1;
            this.picImage.TabStop = false;
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Location = new System.Drawing.Point(450, 12);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.ReadOnly = true;
            this.txtRootFolder.Size = new System.Drawing.Size(338, 22);
            this.txtRootFolder.TabIndex = 2;
            this.txtRootFolder.Text = "Путь до папки";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 440);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 23);
            this.progressBar.TabIndex = 3;
            // 
            // txtModelPath
            // 
            this.txtModelPath.Location = new System.Drawing.Point(450, 40);
            this.txtModelPath.Name = "txtModelPath";
            this.txtModelPath.ReadOnly = true;
            this.txtModelPath.Size = new System.Drawing.Size(338, 22);
            this.txtModelPath.TabIndex = 4;
            this.txtModelPath.Text = "Путь до модели";
            // 
            // txtProgress
            // 
            this.txtProgress.Location = new System.Drawing.Point(450, 68);
            this.txtProgress.Multiline = true;
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.ReadOnly = true;
            this.txtProgress.Size = new System.Drawing.Size(338, 337);
            this.txtProgress.TabIndex = 5;
            this.txtProgress.Text = "Лог...";
            // 
            // predictButton
            // 
            this.predictButton.Location = new System.Drawing.Point(450, 411);
            this.predictButton.Name = "predictButton";
            this.predictButton.Size = new System.Drawing.Size(137, 23);
            this.predictButton.TabIndex = 6;
            this.predictButton.Text = "predictButton";
            this.predictButton.UseVisualStyleBackColor = true;
            this.predictButton.Click += new System.EventHandler(this.predictButton_Click);
            // 
            // frmTrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 472);
            this.Controls.Add(this.predictButton);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.txtModelPath);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtRootFolder);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.btnFineTune);
            this.Controls.Add(this.btnSelectModel);
            this.Controls.Add(this.btnSelectRootFolder);
            this.Name = "frmTrain";
            this.Text = "frmTrain";
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}