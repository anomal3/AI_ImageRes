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
            this.pic = new System.Windows.Forms.PictureBox();
            this.bOpen = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic.Location = new System.Drawing.Point(12, 12);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(803, 515);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.DragDrop += new System.Windows.Forms.DragEventHandler(this.pic_DragDrop);
            this.pic.DragEnter += new System.Windows.Forms.DragEventHandler(this.pic_DragEnter);
            this.pic.DragLeave += new System.EventHandler(this.pic_DragLeave);
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOpen.Location = new System.Drawing.Point(12, 542);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(250, 53);
            this.bOpen.TabIndex = 1;
            this.bOpen.Text = "Открыть и распознать";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.Location = new System.Drawing.Point(358, 542);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(457, 53);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "Это {0} - {1}% затрачено  на распознование {2} сек";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 607);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.pic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Ёбанный ИИ который может распознать Машину, Кота, Собаку и Человека";
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pic;
        private Button bOpen;
        private Label lblResult;
    }
}