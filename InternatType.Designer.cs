namespace LSDX
{
    partial class InternatType
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
            this.btnN1 = new System.Windows.Forms.Button();
            this.txtN1 = new System.Windows.Forms.TextBox();
            this.lblN1 = new System.Windows.Forms.Label();
            this.lblN2 = new System.Windows.Forms.Label();
            this.txtN2 = new System.Windows.Forms.TextBox();
            this.btnN2 = new System.Windows.Forms.Button();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.gbase = new System.Windows.Forms.GroupBox();
            this.txtInter = new System.Windows.Forms.TextBox();
            this.lblN5 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblN4 = new System.Windows.Forms.Label();
            this.txtSheet = new System.Windows.Forms.TextBox();
            this.lblN3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSplitCode = new System.Windows.Forms.TextBox();
            this.lblSplitCode = new System.Windows.Forms.Label();
            this.txtInter1 = new System.Windows.Forms.TextBox();
            this.lblN8 = new System.Windows.Forms.Label();
            this.txtType1 = new System.Windows.Forms.TextBox();
            this.lblN7 = new System.Windows.Forms.Label();
            this.txtSheet1 = new System.Windows.Forms.TextBox();
            this.lblN6 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.gbase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnN1
            // 
            this.btnN1.Location = new System.Drawing.Point(442, 43);
            this.btnN1.Name = "btnN1";
            this.btnN1.Size = new System.Drawing.Size(75, 23);
            this.btnN1.TabIndex = 0;
            this.btnN1.Text = "选择文件";
            this.btnN1.UseVisualStyleBackColor = true;
            this.btnN1.Click += new System.EventHandler(this.btnN1_Click);
            // 
            // txtN1
            // 
            this.txtN1.Location = new System.Drawing.Point(160, 45);
            this.txtN1.Name = "txtN1";
            this.txtN1.ReadOnly = true;
            this.txtN1.Size = new System.Drawing.Size(254, 21);
            this.txtN1.TabIndex = 1;
            // 
            // lblN1
            // 
            this.lblN1.AutoSize = true;
            this.lblN1.Location = new System.Drawing.Point(53, 48);
            this.lblN1.Name = "lblN1";
            this.lblN1.Size = new System.Drawing.Size(101, 12);
            this.lblN1.TabIndex = 2;
            this.lblN1.Text = "制式分类源码表：";
            // 
            // lblN2
            // 
            this.lblN2.AutoSize = true;
            this.lblN2.Location = new System.Drawing.Point(77, 152);
            this.lblN2.Name = "lblN2";
            this.lblN2.Size = new System.Drawing.Size(77, 12);
            this.lblN2.TabIndex = 3;
            this.lblN2.Text = "校验数据表：";
            // 
            // txtN2
            // 
            this.txtN2.Location = new System.Drawing.Point(160, 149);
            this.txtN2.Name = "txtN2";
            this.txtN2.ReadOnly = true;
            this.txtN2.Size = new System.Drawing.Size(254, 21);
            this.txtN2.TabIndex = 4;
            // 
            // btnN2
            // 
            this.btnN2.Location = new System.Drawing.Point(442, 147);
            this.btnN2.Name = "btnN2";
            this.btnN2.Size = new System.Drawing.Size(75, 23);
            this.btnN2.TabIndex = 5;
            this.btnN2.Text = "选择文件";
            this.btnN2.UseVisualStyleBackColor = true;
            this.btnN2.Click += new System.EventHandler(this.btnN2_Click);
            // 
            // txtShow
            // 
            this.txtShow.Location = new System.Drawing.Point(12, 294);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ReadOnly = true;
            this.txtShow.Size = new System.Drawing.Size(691, 154);
            this.txtShow.TabIndex = 6;
            // 
            // gbase
            // 
            this.gbase.Controls.Add(this.txtInter);
            this.gbase.Controls.Add(this.lblN5);
            this.gbase.Controls.Add(this.txtType);
            this.gbase.Controls.Add(this.lblN4);
            this.gbase.Controls.Add(this.txtSheet);
            this.gbase.Controls.Add(this.lblN3);
            this.gbase.Location = new System.Drawing.Point(55, 76);
            this.gbase.Name = "gbase";
            this.gbase.Size = new System.Drawing.Size(519, 60);
            this.gbase.TabIndex = 8;
            this.gbase.TabStop = false;
            this.gbase.Text = "源表参数";
            // 
            // txtInter
            // 
            this.txtInter.Location = new System.Drawing.Point(430, 24);
            this.txtInter.Name = "txtInter";
            this.txtInter.Size = new System.Drawing.Size(57, 21);
            this.txtInter.TabIndex = 13;
            this.txtInter.Text = "工作组";
            // 
            // lblN5
            // 
            this.lblN5.AutoSize = true;
            this.lblN5.Location = new System.Drawing.Point(332, 30);
            this.lblN5.Name = "lblN5";
            this.lblN5.Size = new System.Drawing.Size(101, 12);
            this.lblN5.TabIndex = 12;
            this.lblN5.Text = "网络制式列名称：";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(254, 24);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(57, 21);
            this.txtType.TabIndex = 11;
            this.txtType.Text = "标准";
            // 
            // lblN4
            // 
            this.lblN4.AutoSize = true;
            this.lblN4.Location = new System.Drawing.Point(171, 30);
            this.lblN4.Name = "lblN4";
            this.lblN4.Size = new System.Drawing.Size(89, 12);
            this.lblN4.TabIndex = 10;
            this.lblN4.Text = "标准号列名称：";
            // 
            // txtSheet
            // 
            this.txtSheet.Location = new System.Drawing.Point(89, 24);
            this.txtSheet.Name = "txtSheet";
            this.txtSheet.Size = new System.Drawing.Size(57, 21);
            this.txtSheet.TabIndex = 9;
            this.txtSheet.Text = "Sheet1";
            // 
            // lblN3
            // 
            this.lblN3.AutoSize = true;
            this.lblN3.Location = new System.Drawing.Point(16, 30);
            this.lblN3.Name = "lblN3";
            this.lblN3.Size = new System.Drawing.Size(71, 12);
            this.lblN3.TabIndex = 8;
            this.lblN3.Text = "Excel表名：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSplitCode);
            this.groupBox1.Controls.Add(this.lblSplitCode);
            this.groupBox1.Controls.Add(this.txtInter1);
            this.groupBox1.Controls.Add(this.lblN8);
            this.groupBox1.Controls.Add(this.txtType1);
            this.groupBox1.Controls.Add(this.lblN7);
            this.groupBox1.Controls.Add(this.txtSheet1);
            this.groupBox1.Controls.Add(this.lblN6);
            this.groupBox1.Location = new System.Drawing.Point(53, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 85);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "校验表参数";
            // 
            // txtSplitCode
            // 
            this.txtSplitCode.Location = new System.Drawing.Point(89, 55);
            this.txtSplitCode.Name = "txtSplitCode";
            this.txtSplitCode.Size = new System.Drawing.Size(57, 21);
            this.txtSplitCode.TabIndex = 15;
            this.txtSplitCode.Text = "/";
            // 
            // lblSplitCode
            // 
            this.lblSplitCode.AutoSize = true;
            this.lblSplitCode.Location = new System.Drawing.Point(16, 61);
            this.lblSplitCode.Name = "lblSplitCode";
            this.lblSplitCode.Size = new System.Drawing.Size(77, 12);
            this.lblSplitCode.TabIndex = 14;
            this.lblSplitCode.Text = "制式分隔符：";
            // 
            // txtInter1
            // 
            this.txtInter1.Location = new System.Drawing.Point(432, 27);
            this.txtInter1.Name = "txtInter1";
            this.txtInter1.Size = new System.Drawing.Size(57, 21);
            this.txtInter1.TabIndex = 13;
            this.txtInter1.Text = "工作组";
            // 
            // lblN8
            // 
            this.lblN8.AutoSize = true;
            this.lblN8.Location = new System.Drawing.Point(332, 30);
            this.lblN8.Name = "lblN8";
            this.lblN8.Size = new System.Drawing.Size(101, 12);
            this.lblN8.TabIndex = 12;
            this.lblN8.Text = "网络制式列名称：";
            // 
            // txtType1
            // 
            this.txtType1.Location = new System.Drawing.Point(256, 24);
            this.txtType1.Name = "txtType1";
            this.txtType1.Size = new System.Drawing.Size(57, 21);
            this.txtType1.TabIndex = 11;
            this.txtType1.Text = "标准";
            // 
            // lblN7
            // 
            this.lblN7.AutoSize = true;
            this.lblN7.Location = new System.Drawing.Point(172, 30);
            this.lblN7.Name = "lblN7";
            this.lblN7.Size = new System.Drawing.Size(89, 12);
            this.lblN7.TabIndex = 10;
            this.lblN7.Text = "标准号列名称：";
            // 
            // txtSheet1
            // 
            this.txtSheet1.Location = new System.Drawing.Point(89, 24);
            this.txtSheet1.Name = "txtSheet1";
            this.txtSheet1.Size = new System.Drawing.Size(57, 21);
            this.txtSheet1.TabIndex = 9;
            this.txtSheet1.Text = "Sheet1";
            // 
            // lblN6
            // 
            this.lblN6.AutoSize = true;
            this.lblN6.Location = new System.Drawing.Point(16, 30);
            this.lblN6.Name = "lblN6";
            this.lblN6.Size = new System.Drawing.Size(71, 12);
            this.lblN6.TabIndex = 8;
            this.lblN6.Text = "Excel表名：";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(598, 100);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 117);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "输    出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // InternatType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 460);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbase);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.btnN2);
            this.Controls.Add(this.txtN2);
            this.Controls.Add(this.lblN2);
            this.Controls.Add(this.lblN1);
            this.Controls.Add(this.txtN1);
            this.Controls.Add(this.btnN1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InternatType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InternatType";
            this.gbase.ResumeLayout(false);
            this.gbase.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnN1;
        private System.Windows.Forms.TextBox txtN1;
        private System.Windows.Forms.Label lblN1;
        private System.Windows.Forms.Label lblN2;
        private System.Windows.Forms.TextBox txtN2;
        private System.Windows.Forms.Button btnN2;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.GroupBox gbase;
        private System.Windows.Forms.Label lblN3;
        private System.Windows.Forms.TextBox txtSheet;
        private System.Windows.Forms.TextBox txtInter;
        private System.Windows.Forms.Label lblN5;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblN4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInter1;
        private System.Windows.Forms.Label lblN8;
        private System.Windows.Forms.TextBox txtType1;
        private System.Windows.Forms.Label lblN7;
        private System.Windows.Forms.TextBox txtSheet1;
        private System.Windows.Forms.Label lblN6;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtSplitCode;
        private System.Windows.Forms.Label lblSplitCode;
    }
}