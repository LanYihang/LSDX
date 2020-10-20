namespace LSDX
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.stripBottom = new System.Windows.Forms.StatusStrip();
            this.tsslExplain = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsTop = new System.Windows.Forms.ToolStrip();
            this.tslType = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslCNFilingn = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslCNFilingnTecType = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslBaseData = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tslHTMLParser = new System.Windows.Forms.ToolStripLabel();
            this.stripBottom.SuspendLayout();
            this.tsTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBottom
            // 
            this.stripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslExplain});
            this.stripBottom.Location = new System.Drawing.Point(0, 491);
            this.stripBottom.Name = "stripBottom";
            this.stripBottom.Size = new System.Drawing.Size(724, 22);
            this.stripBottom.TabIndex = 0;
            this.stripBottom.Text = "statusStrip1";
            // 
            // tsslExplain
            // 
            this.tsslExplain.ForeColor = System.Drawing.Color.Red;
            this.tsslExplain.Name = "tsslExplain";
            this.tsslExplain.Size = new System.Drawing.Size(0, 17);
            // 
            // tsTop
            // 
            this.tsTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslType,
            this.toolStripSeparator1,
            this.tslCNFilingn,
            this.toolStripSeparator2,
            this.tslCNFilingnTecType,
            this.toolStripSeparator3,
            this.tslBaseData,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStripSeparator5,
            this.tslHTMLParser});
            this.tsTop.Location = new System.Drawing.Point(0, 0);
            this.tsTop.Name = "tsTop";
            this.tsTop.Size = new System.Drawing.Size(724, 25);
            this.tsTop.TabIndex = 1;
            this.tsTop.Text = "toolStrip1";
            // 
            // tslType
            // 
            this.tslType.Name = "tslType";
            this.tslType.Size = new System.Drawing.Size(56, 22);
            this.tslType.Text = "处理分类";
            this.tslType.Visible = false;
            this.tslType.Click += new System.EventHandler(this.tslType_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // tslCNFilingn
            // 
            this.tslCNFilingn.Name = "tslCNFilingn";
            this.tslCNFilingn.Size = new System.Drawing.Size(152, 22);
            this.tslCNFilingn.Text = "同族申请号（依分类解析）";
            this.tslCNFilingn.Visible = false;
            this.tslCNFilingn.Click += new System.EventHandler(this.tslCNFilingn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // tslCNFilingnTecType
            // 
            this.tslCNFilingnTecType.Name = "tslCNFilingnTecType";
            this.tslCNFilingnTecType.Size = new System.Drawing.Size(189, 22);
            this.tslCNFilingnTecType.Text = "同族申请号(依分类/技术分类解析)";
            this.tslCNFilingnTecType.Visible = false;
            this.tslCNFilingnTecType.Click += new System.EventHandler(this.tslCNFilingnTecType_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // tslBaseData
            // 
            this.tslBaseData.Name = "tslBaseData";
            this.tslBaseData.Size = new System.Drawing.Size(80, 22);
            this.tslBaseData.Text = "基础数据加载";
            this.tslBaseData.Visible = false;
            this.tslBaseData.Click += new System.EventHandler(this.tslBaseData_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "网络制式校验";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 466);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tslHTMLParser
            // 
            this.tslHTMLParser.Name = "tslHTMLParser";
            this.tslHTMLParser.Size = new System.Drawing.Size(56, 22);
            this.tslHTMLParser.Text = "网页爬虫";
            this.tslHTMLParser.Click += new System.EventHandler(this.tslHTMLParser_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(724, 513);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tsTop);
            this.Controls.Add(this.stripBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LSXLS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.stripBottom.ResumeLayout(false);
            this.stripBottom.PerformLayout();
            this.tsTop.ResumeLayout(false);
            this.tsTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stripBottom;
        private System.Windows.Forms.ToolStripStatusLabel tsslExplain;
        private System.Windows.Forms.ToolStrip tsTop;
        private System.Windows.Forms.ToolStripLabel tslType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslCNFilingn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslBaseData;
        private System.Windows.Forms.ToolStripLabel tslCNFilingnTecType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel tslHTMLParser;
    }
}