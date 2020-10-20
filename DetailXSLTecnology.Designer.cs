namespace LSDX
{
    partial class DetailXSLTecnology
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
            this.btnStart = new System.Windows.Forms.Button();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.txtXLSPath = new System.Windows.Forms.TextBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.lblTop = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(512, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "同族申请号开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtShow
            // 
            this.txtShow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShow.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.txtShow.Location = new System.Drawing.Point(13, 54);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ReadOnly = true;
            this.txtShow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShow.Size = new System.Drawing.Size(690, 394);
            this.txtShow.TabIndex = 8;
            // 
            // txtXLSPath
            // 
            this.txtXLSPath.Location = new System.Drawing.Point(173, 27);
            this.txtXLSPath.Name = "txtXLSPath";
            this.txtXLSPath.ReadOnly = true;
            this.txtXLSPath.Size = new System.Drawing.Size(218, 21);
            this.txtXLSPath.TabIndex = 7;
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(415, 25);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChooseFolder.TabIndex = 6;
            this.btnChooseFolder.Text = "选择文件夹";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTop.Location = new System.Drawing.Point(51, 30);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(97, 15);
            this.lblTop.TabIndex = 5;
            this.lblTop.Text = "选择文件夹：";
            // 
            // DetailXSLTecnology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 460);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.txtXLSPath);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.lblTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailXSLTecnology";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.TextBox txtXLSPath;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.Label lblTop;
    }
}