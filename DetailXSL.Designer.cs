namespace LSDX
{
    partial class s
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s));
            this.lblTop = new System.Windows.Forms.Label();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.txtXLSPath = new System.Windows.Forms.TextBox();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSheet = new System.Windows.Forms.TextBox();
            this.txtTiaojian = new System.Windows.Forms.TextBox();
            this.txtAnswerLine = new System.Windows.Forms.TextBox();
            this.gbExplain = new System.Windows.Forms.GroupBox();
            this.lblExplain = new System.Windows.Forms.Label();
            this.gbChoose = new System.Windows.Forms.GroupBox();
            this.btnCount = new System.Windows.Forms.Button();
            this.cbFLZT = new System.Windows.Forms.CheckBox();
            this.cbType = new System.Windows.Forms.CheckBox();
            this.cbSRR = new System.Windows.Forms.CheckBox();
            this.gbExplain.SuspendLayout();
            this.gbChoose.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTop.Location = new System.Drawing.Point(50, 33);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(122, 15);
            this.lblTop.TabIndex = 0;
            this.lblTop.Text = "选择Excel文件：";
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(414, 28);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChooseFolder.TabIndex = 1;
            this.btnChooseFolder.Text = "选择文件";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // txtXLSPath
            // 
            this.txtXLSPath.Location = new System.Drawing.Point(172, 30);
            this.txtXLSPath.Name = "txtXLSPath";
            this.txtXLSPath.ReadOnly = true;
            this.txtXLSPath.Size = new System.Drawing.Size(218, 21);
            this.txtXLSPath.TabIndex = 2;
            // 
            // txtShow
            // 
            this.txtShow.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShow.ForeColor = System.Drawing.Color.Red;
            this.txtShow.Location = new System.Drawing.Point(12, 233);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ReadOnly = true;
            this.txtShow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShow.Size = new System.Drawing.Size(682, 211);
            this.txtShow.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(518, 28);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开  始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(50, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "Excel表单名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(254, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "条件列名称:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(442, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "解析列名称：";
            // 
            // txtSheet
            // 
            this.txtSheet.Location = new System.Drawing.Point(161, 68);
            this.txtSheet.Name = "txtSheet";
            this.txtSheet.Size = new System.Drawing.Size(86, 21);
            this.txtSheet.TabIndex = 9;
            this.txtSheet.Text = "Sheet1";
            // 
            // txtTiaojian
            // 
            this.txtTiaojian.Location = new System.Drawing.Point(344, 68);
            this.txtTiaojian.Name = "txtTiaojian";
            this.txtTiaojian.Size = new System.Drawing.Size(86, 21);
            this.txtTiaojian.TabIndex = 10;
            this.txtTiaojian.Text = "制式分类";
            this.txtTiaojian.TextChanged += new System.EventHandler(this.txtTiaojian_TextChanged);
            // 
            // txtAnswerLine
            // 
            this.txtAnswerLine.Location = new System.Drawing.Point(533, 69);
            this.txtAnswerLine.Name = "txtAnswerLine";
            this.txtAnswerLine.Size = new System.Drawing.Size(86, 21);
            this.txtAnswerLine.TabIndex = 12;
            this.txtAnswerLine.Text = "US公开号";
            // 
            // gbExplain
            // 
            this.gbExplain.Controls.Add(this.lblExplain);
            this.gbExplain.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbExplain.ForeColor = System.Drawing.Color.Red;
            this.gbExplain.Location = new System.Drawing.Point(53, 151);
            this.gbExplain.Name = "gbExplain";
            this.gbExplain.Size = new System.Drawing.Size(566, 76);
            this.gbExplain.TabIndex = 13;
            this.gbExplain.TabStop = false;
            this.gbExplain.Text = "说明";
            // 
            // lblExplain
            // 
            this.lblExplain.AutoSize = true;
            this.lblExplain.Location = new System.Drawing.Point(22, 19);
            this.lblExplain.Name = "lblExplain";
            this.lblExplain.Size = new System.Drawing.Size(49, 14);
            this.lblExplain.TabIndex = 0;
            this.lblExplain.Text = "label3";
            // 
            // gbChoose
            // 
            this.gbChoose.Controls.Add(this.btnCount);
            this.gbChoose.Controls.Add(this.cbFLZT);
            this.gbChoose.Controls.Add(this.cbType);
            this.gbChoose.Controls.Add(this.cbSRR);
            this.gbChoose.Location = new System.Drawing.Point(53, 96);
            this.gbChoose.Name = "gbChoose";
            this.gbChoose.Size = new System.Drawing.Size(566, 49);
            this.gbChoose.TabIndex = 14;
            this.gbChoose.TabStop = false;
            this.gbChoose.Text = "筛选";
            // 
            // btnCount
            // 
            this.btnCount.Enabled = false;
            this.btnCount.Location = new System.Drawing.Point(509, 20);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(51, 23);
            this.btnCount.TabIndex = 3;
            this.btnCount.Text = "计 数";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // cbFLZT
            // 
            this.cbFLZT.AutoSize = true;
            this.cbFLZT.Location = new System.Drawing.Point(217, 22);
            this.cbFLZT.Name = "cbFLZT";
            this.cbFLZT.Size = new System.Drawing.Size(96, 16);
            this.cbFLZT.TabIndex = 2;
            this.cbFLZT.Text = "授权法律状态";
            this.cbFLZT.UseVisualStyleBackColor = true;
            // 
            // cbType
            // 
            this.cbType.AutoSize = true;
            this.cbType.Location = new System.Drawing.Point(101, 23);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(15, 14);
            this.cbType.TabIndex = 1;
            this.cbType.UseVisualStyleBackColor = true;
            // 
            // cbSRR
            // 
            this.cbSRR.AutoSize = true;
            this.cbSRR.Location = new System.Drawing.Point(25, 23);
            this.cbSRR.Name = "cbSRR";
            this.cbSRR.Size = new System.Drawing.Size(60, 16);
            this.cbSRR.TabIndex = 0;
            this.cbSRR.Text = "受让人";
            this.cbSRR.UseVisualStyleBackColor = true;
            // 
            // s
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 460);
            this.Controls.Add(this.gbChoose);
            this.Controls.Add(this.gbExplain);
            this.Controls.Add(this.txtAnswerLine);
            this.Controls.Add(this.txtTiaojian);
            this.Controls.Add(this.txtSheet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.txtXLSPath);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.lblTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "s";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XLS工具";
            this.Load += new System.EventHandler(this.DetailXSL_Load);
            this.gbExplain.ResumeLayout(false);
            this.gbExplain.PerformLayout();
            this.gbChoose.ResumeLayout(false);
            this.gbChoose.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.TextBox txtXLSPath;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSheet;
        private System.Windows.Forms.TextBox txtTiaojian;
        private System.Windows.Forms.TextBox txtAnswerLine;
        private System.Windows.Forms.GroupBox gbExplain;
        private System.Windows.Forms.Label lblExplain;
        private System.Windows.Forms.GroupBox gbChoose;
        private System.Windows.Forms.CheckBox cbSRR;
        private System.Windows.Forms.CheckBox cbType;
        private System.Windows.Forms.CheckBox cbFLZT;
        private System.Windows.Forms.Button btnCount;
    }
}