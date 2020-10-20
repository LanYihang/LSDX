using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Com.Ls.Lsdx.Util;

namespace LSDX
{
    public partial class BaseForm : Form
    {
        private Thread _thread = null;//存储操作数据的线程

        public BaseForm()
        {
            InitializeComponent();
            this.lblWarning.Text = "录入Standard Project中关键字以英文逗号,分隔，否则无法录入!\r\n\r\n关键字对大小写敏感";
            InitKeyWordsByType(this.cbType.Text);
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 导入基础数据按钮
        /// 导入过程中既要把数据导入到字典类中也要生成XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.txtProject.Text.Trim() != "" || this.txtStandard.Text.Trim() != "")
            {
                this.txtWarning.AppendText("\r\nStart serialization data,wait for a moment.......... \r\n");
                _thread = new Thread(new ThreadStart(DetailSource));
                _thread.Start();
            }
            else
            {
                this.txtWarning.AppendText("\r\nPlease enter standard or project data....\r\n");
            }
        }
        /// <summary>
        /// 操作数据源，将其序列化到本地文件及内存中
        /// </summary>
        private void DetailSource()
        {
            try
            {
                string[] standards = this.txtStandard.Text.Trim().Split(',');
                string[] projects = this.txtProject.Text.Trim().Split(',');
                string type = this.cbType.Text;
                LoadDataInBaseSource(type, standards, projects);
                //将数据序列化至XML中
                XMLUtil.SerializationDataForXML();
            }
            catch (Exception ex)
            {
                this.txtWarning.AppendText("\r\n**************** Serialization data failed **************** \r\n");
                this.txtWarning.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
            finally
            {
                if (this._thread != null)
                {
                    this.txtWarning.AppendText("\r\n**************** Load data success **************** \r\n");
                    this._thread.Abort();
                }
            }
        }

        /// <summary>
        /// 下拉类别索引值发生变化，关键字内容也随之发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitKeyWordsByType(this.cbType.Text);
        }
        /// <summary>
        /// 依据分类类别来加载关键字信息
        /// </summary>
        /// <param name="type">分类类别</param>
        private void InitKeyWordsByType(string type)
        {
            try
            {
                this.txtStandard.Text = "";
                this.txtProject.Text = "";
                if (Sysinfo.XLS_TYPE_DICTIONARA.Keys.Contains(type))
                {
                    BaseInfo info = Sysinfo.XLS_TYPE_DICTIONARA[type];
                    for (int i = 0; i < info.Standards.Count; i++)
                    {
                        if (i < info.Standards.Count - 1)
                            this.txtStandard.Text += info.Standards[i].ToString() + ",";
                        else
                            this.txtStandard.Text += info.Standards[i].ToString();
                    }
                    for (int i = 0; i < info.Projects.Count; i++)
                    {
                        if (i < info.Projects.Count - 1)
                            this.txtProject.Text += info.Projects[i].ToString() + ",";
                        else
                            this.txtProject.Text += info.Projects[i].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 加载数据进入基础数据中
        /// </summary>
        private void LoadDataInBaseSource(string type,string[] standards,string[] projects)
        {
            try
            {
                BaseInfo info = new BaseInfo();
                foreach (string standard in standards)
                    info.Standards.Add(standard.Trim());
                foreach (string project in projects)
                    info.Projects.Add(project.Trim());
                if (Sysinfo.XLS_TYPE_DICTIONARA.Keys.Contains(type))
                    //包含关键字，则将值进行修改
                    Sysinfo.XLS_TYPE_DICTIONARA[type] = info;
                else
                    //如果不包含该关键字,则添加新的关键字
                    Sysinfo.XLS_TYPE_DICTIONARA.Add(type, info);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportToLocal_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Sysinfo.BASE_DICTIONARY_XML_PATH))
                {
                    FolderBrowserDialog folder = new FolderBrowserDialog();
                    folder.ShowDialog();
                    string folderpath = folder.SelectedPath;
                    File.Copy(Sysinfo.BASE_DICTIONARY_XML_PATH, folderpath + @"\" + Sysinfo.BASE_DICTIONARY_XML_PATH, true);
                    this.txtWarning.AppendText("\r\n**************** Export file success **************** \r\n");
                    this.txtWarning.AppendText(folderpath + @"\" + Sysinfo.BASE_DICTIONARY_XML_PATH+"\r\n");
                }
                else
                {
                    MessageBox.Show("未创建基础数据文件，请创建！");
                }
            }catch(Exception ex)
            {
                this.txtWarning.AppendText("\r\n**************** Export base file failed **************** \r\n");
                this.txtWarning.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        /// <summary>
        /// 导入基础数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportFileToSoft_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file_dialog = new OpenFileDialog();
                file_dialog.ShowDialog();
                string base_file_path = file_dialog.FileName;
                if (Path.GetFileName(base_file_path) == Sysinfo.BASE_DICTIONARY_XML_PATH)
                {
                    File.Copy(base_file_path, Sysinfo.BASE_DICTIONARY_XML_PATH, true);
                    XMLUtil.LoadXMLData();
                    this.txtWarning.AppendText("\r\n**************** Import base data success **************** \r\n");
                }
                else
                {
                    this.txtWarning.AppendText("\r\n**************** Import base file error **************** \r\n");
                    this.txtWarning.AppendText("\r\nimport file name is base.xml\r\n");
                }
            }
            catch (Exception ex)
            {
                this.txtWarning.AppendText("\r\n**************** Import base file failed **************** \r\n");
                this.txtWarning.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
    }
}
