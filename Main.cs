using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Com.Ls.Lsdx.Util;

namespace LSDX
{
    public partial class Main : Form
    {
        private s xsl = null;//同族申请号处理窗体
        private DetailTypeXSL typeXSL = null;//类型处理窗体
        private BaseForm baseXSL = null;//基础数据加载窗体
        private DetailXSLTecnology technologyXSL = null;//同族申请号技术分类处理窗体
        private InternatType interXSL = null;//网络制式校验
        private ParserForm parser = null;//网络爬虫
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                //先加载基础数据
                XMLUtil.LoadXMLData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据异常！");
                this.tslType.Enabled = false;
                this.tslCNFilingn.Enabled = false;
                this.tslBaseData.Enabled = false;
            }
        }
        /// <summary>
        /// 分类窗体实例化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslType_Click(object sender, EventArgs e)
        {
            if (xsl != null)
            {
                xsl.Close();
                xsl = null;
            }
            if (baseXSL != null)
            {
                baseXSL.Close();
                baseXSL = null;
            }
            if (technologyXSL != null)
            {
                technologyXSL.Close();
                technologyXSL = null;
            }
            if (interXSL != null)
            {
                interXSL.Close();
                interXSL = null;
            }
            typeXSL = new DetailTypeXSL();
            typeXSL.MdiParent = this;
            typeXSL.Show();
            this.tsslExplain.Text = "依据Project Standard获得分类";
        }
        /// <summary>
        /// 激活同族申请申请号处理的窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslCNFilingn_Click(object sender, EventArgs e)
        {
            if (typeXSL != null)
            {
                typeXSL.Close();
                typeXSL = null;
            }
            if (baseXSL != null)
            {
                baseXSL.Close();
                baseXSL = null;
            }
            if (technologyXSL != null) 
            {
                technologyXSL.Close();
                technologyXSL = null;
            }
            if (interXSL != null)
            {
                interXSL.Close();
                interXSL = null;
            }
            xsl = new s();
            xsl.MdiParent = this;
            xsl.Show();
            this.tsslExplain.Text = "依据分类处理同族申请号";
        }
        /// <summary>
        /// 依据分类/技术分类处理同族申请号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslCNFilingnTecType_Click(object sender, EventArgs e)
        {
            if (typeXSL != null)
            {
                typeXSL.Close();
                typeXSL = null;
            }
            if (baseXSL != null)
            {
                baseXSL.Close();
                baseXSL = null;
            }
            if (xsl != null)
            {
                xsl.Close();
                xsl = null;
            }
            if (interXSL != null)
            {
                interXSL.Close();
                interXSL = null;
            }
            technologyXSL = new DetailXSLTecnology();
            technologyXSL.MdiParent = this;
            technologyXSL.Show();
            this.tsslExplain.Text = "依据分类/技术分类处理同族申请号";
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (typeXSL != null)
            {
                typeXSL.Close();
                typeXSL = null;
            }
            if (baseXSL != null)
            {
                baseXSL.Close();
                baseXSL = null;
            }
            if (technologyXSL != null)
            {
                technologyXSL.Close();
                technologyXSL = null;
            }
            if (xsl != null)
            {
                xsl.Close();
                xsl = null;
            }
            interXSL = new InternatType();
            interXSL.MdiParent = this;
            interXSL.Show();
            this.tsslExplain.Text = "网络制式名称校验";
        }
        /// <summary>
        /// 基础数据加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslBaseData_Click(object sender, EventArgs e)
        {
            if (xsl != null)
            {
                xsl.Close();
                xsl = null;
            }
            if (typeXSL != null)
            {
                typeXSL.Close();
                typeXSL = null;
            }
            if (technologyXSL != null)
            {
                technologyXSL.Close();
                technologyXSL = null;
            }
            if (interXSL != null)
            {
                interXSL.Close();
                interXSL = null;
            }
            baseXSL = new BaseForm();
            baseXSL.MdiParent = this;
            baseXSL.Show();
            this.tsslExplain.Text = "分类关键字project standard字典录入";
        }
        /// <summary>
        /// 网页爬虫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslHTMLParser_Click(object sender, EventArgs e)
        {
            if (xsl != null)
            {
                xsl.Close();
                xsl = null;
            }
            if (baseXSL != null)
            {
                baseXSL.Close();
                baseXSL = null;
            }
            if (technologyXSL != null)
            {
                technologyXSL.Close();
                technologyXSL = null;
            }
            if (interXSL != null)
            {
                interXSL.Close();
                interXSL = null;
            }
            if (typeXSL != null)
            {
                typeXSL.Close();
                typeXSL = null;
            }
            parser = new ParserForm();
            parser.MdiParent = this;
            parser.Show();
        }
        /// <summary>
        /// 窗体关闭时释放所有的控件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        
    }
}
