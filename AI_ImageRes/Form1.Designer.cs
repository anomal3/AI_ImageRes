namespace AI_ImageRes
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pic = new PictureBox();
            bOpen = new Button();
            lblResult = new Label();
            txtPathModel = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            // 
            // pic
            // 
            pic.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Location = new Point(14, 14);
            pic.Margin = new Padding(4, 3, 4, 3);
            pic.Name = "pic";
            pic.Size = new Size(936, 594);
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.DragDrop += pic_DragDrop;
            pic.DragEnter += pic_DragEnter;
            pic.DragLeave += pic_DragLeave;
            // 
            // bOpen
            // 
            bOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bOpen.Location = new Point(14, 625);
            bOpen.Margin = new Padding(4, 3, 4, 3);
            bOpen.Name = "bOpen";
            bOpen.Size = new Size(292, 61);
            bOpen.TabIndex = 1;
            bOpen.Text = "Открыть и распознать";
            bOpen.UseVisualStyleBackColor = true;
            bOpen.Click += bOpen_Click;
            // 
            // lblResult
            // 
            lblResult.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResult.BorderStyle = BorderStyle.FixedSingle;
            lblResult.FlatStyle = FlatStyle.System;
            lblResult.Location = new Point(314, 664);
            lblResult.Margin = new Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(533, 22);
            lblResult.TabIndex = 2;
            lblResult.Text = "Это {0} - {1}% затрачено  на распознование {2} сек";
            lblResult.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtPathModel
            // 
            txtPathModel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtPathModel.Location = new Point(314, 625);
            txtPathModel.Name = "txtPathModel";
            txtPathModel.Size = new Size(533, 23);
            txtPathModel.TabIndex = 3;
            txtPathModel.Text = "Кликните дважды чтобы указать путь до модели";
            txtPathModel.MouseDoubleClick += txtPathModel_MouseDoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 700);
            Controls.Add(txtPathModel);
            Controls.Add(lblResult);
            Controls.Add(bOpen);
            Controls.Add(pic);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "ИИ который может...";
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pic;
        private Button bOpen;
        private Label lblResult;
        private TextBox txtPathModel;
    }
}