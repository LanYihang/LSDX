namespace LSDX
{
    partial class BaseForm
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
            this.lblType = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStandard = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.txtWarning = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnExportToLocal = new System.Windows.Forms.Button();
            this.btnImportFileToSoft = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(47, 29);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(71, 12);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "分     类：";
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "4G",
            "3G",
            "2G",
            "TG",
            "DG",
            "OT",
            "UK"});
            this.cbType.Location = new System.Drawing.Point(124, 26);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 20);
            this.cbType.TabIndex = 1;
            this.cbType.Text = "4G";
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStandard);
            this.groupBox1.Location = new System.Drawing.Point(49, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 83);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Standard";
            // 
            // txtStandard
            // 
            this.txtStandard.Location = new System.Drawing.Point(6, 13);
            this.txtStandard.Multiline = true;
            this.txtStandard.Name = "txtStandard";
            this.txtStandard.Size = new System.Drawing.Size(305, 62);
            this.txtStandard.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtProject);
            this.groupBox2.Location = new System.Drawing.Point(372, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 83);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project";
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(6, 13);
            this.txtProject.Multiline = true;
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(309, 62);
            this.txtProject.TabIndex = 1;
            // 
            // txtWarning
            // 
            this.txtWarning.Location = new System.Drawing.Point(49, 141);
            this.txtWarning.Multiline = true;
            this.txtWarning.Name = "txtWarning";
            this.txtWarning.ReadOnly = true;
            this.txtWarning.Size = new System.Drawing.Size(644, 245);
            this.txtWarning.TabIndex = 4;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(257, 24);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(87, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "导入基础数据";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarning.ForeColor = System.Drawing.Color.DarkRed;
            this.lblWarning.Location = new System.Drawing.Point(46, 389);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(62, 16);
            this.lblWarning.TabIndex = 6;
            this.lblWarning.Text = "label1";
            // 
            // btnExportToLocal
            // 
            this.btnExportToLocal.Location = new System.Drawing.Point(350, 24);
            this.btnExportToLocal.Name = "btnExportToLocal";
            this.btnExportToLocal.Size = new System.Drawing.Size(138, 23);
            this.btnExportToLocal.TabIndex = 7;
            this.btnExportToLocal.Text = "基础文件导出至本地";
            this.btnExportToLocal.UseVisualStyleBackColor = true;
            this.btnExportToLocal.Click += new System.EventHandler(this.btnExportToLocal_Click);
            // 
            // btnImportFileToSoft
            // 
            this.btnImportFileToSoft.Location = new System.Drawing.Point(494, 24);
            this.btnImportFileToSoft.Name = "btnImportFileToSoft";
            this.btnImportFileToSoft.Size = new System.Drawing.Size(152, 23);
            this.btnImportFileToSoft.TabIndex = 8;
            this.btnImportFileToSoft.Text = "导入基础数据文件至程序";
            this.btnImportFileToSoft.UseVisualStyleBackColor = true;
            this.btnImportFileToSoft.Click += new System.EventHandler(this.btnImportFileToSoft_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 460);
            this.Controls.Add(this.btnImportFileToSoft);
            this.Controls.Add(this.btnExportToLocal);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtWarning);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.lblType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtStandard;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.TextBox txtWarning;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button btnExportToLocal;
        private System.Windows.Forms.Button btnImportFileToSoft;
    }
}