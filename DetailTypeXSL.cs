using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using Com.Ls.Lsdx.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LSDX
{
    public partial class DetailTypeXSL : Form
    {
        private Thread _thread = null;

        public DetailTypeXSL()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //选择操作的文件
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog showFileDialog = new OpenFileDialog();
                showFileDialog.ShowDialog();
                string xlsPath = showFileDialog.FileName;
                if (Path.GetExtension(xlsPath) == ".xls" || Path.GetExtension(xlsPath) == ".xlsx")
                {
                    this.txtXLSPath.Text = xlsPath;
                    this.btnStart.Enabled = true;
                    this.txtShow.Text = "";
                    this.txtShow.AppendText("Load the excel file successfully...\r\n");
                }
                else
                {
                    MessageBox.Show("Please select excel file！\r\n");
                }
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        //分类开始事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            try 
            {
                _thread = new Thread(new ThreadStart(ReadExcel));
                _thread.Start();
            }catch(Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        /// <summary>
        /// 读取Excel
        /// </summary>
        private void ReadExcel() 
        {
            int i = 0;
            try
            {
                IWorkbook work_book = null;
                IWorkbook new_work_book = null;
                using (FileStream stream = new FileStream(this.txtXLSPath.Text, FileMode.Open), new_stream = new FileStream("D:/" + Path.GetFileName(this.txtXLSPath.Text), FileMode.Create, FileAccess.Write))
                {
                    txtShow.AppendText("Start reading excel file...\r\n");
                    if (Path.GetExtension(this.txtXLSPath.Text) == ".xls")
                    {
                        work_book = new HSSFWorkbook(stream);
                        new_work_book = new HSSFWorkbook();
                    }
                    else if (Path.GetExtension(this.txtXLSPath.Text) == ".xlsx")
                    {
                        work_book = new XSSFWorkbook(stream);
                        new_work_book = new XSSFWorkbook();
                    }
                    ISheet sheet = work_book.GetSheet("Sheet1") == null ? work_book.GetSheet("未去重") : work_book.GetSheet("Sheet1");//获得表一数据
                    ISheet new_sheet = new_work_book.CreateSheet("Sheet1");
                    int rowsCount = sheet.PhysicalNumberOfRows;
                    int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;
                    //获得project与空口/核心网涉及的标准的列号
                    int project_column_count = 0;//project的列号
                    int standard_column_count = 0;//空口/核心网涉及的标准的列号
                    int type_column_count = 0;//分类的列号
                    for (int p = 0; p < colsCount; p++) 
                    {
                        string column_name = sheet.GetRow(0).GetCell(p).ToString().Trim();
                        if (column_name == "Project")
                            project_column_count = p;
                        else if (column_name == "空口/核心网涉及的标准")
                            standard_column_count = p;
                        else if (column_name == "分类")
                            type_column_count = p;
                    }
                    //开始读取行数据
                    for (; i < rowsCount;i++ ) 
                    {
                        IRow row=sheet.GetRow(i);
                        IRow new_row = new_sheet.CreateRow(i);
                        new_row.Height = 200 * 2;
                        for (int j = 0; j < colsCount; j++)
                        {
                            ICell new_cell = new_row.CreateCell(j);
                            new_cell.SetCellType(CellType.STRING);
                            new_sheet.SetColumnWidth(j, 20 * 256);
                            if (i > 0 && j == type_column_count)
                            {
                                string standards = row.GetCell(standard_column_count) == null ? "" : row.GetCell(standard_column_count).ToString().Trim();
                                string projects = row.GetCell(project_column_count) == null ? "" : row.GetCell(project_column_count).ToString().Trim();
                                //开始加载分类
                                SetTypeValue(new_cell, standards, projects);
                            }
                            else
                                if (sheet.GetRow(i).GetCell(j) != null)
                                    new_cell.SetCellValue(sheet.GetRow(i).GetCell(j).ToString().Trim());
                        }
                    }

                    MemoryStream memory = new MemoryStream();
                    new_work_book.Write(memory);
                    byte[] buffer = memory.ToArray();
                    new_stream.Write(buffer, 0, buffer.Length);
                    new_stream.Flush();
                    txtShow.AppendText("Generate excel file path is D:/" + Path.GetFileName(this.txtXLSPath.Text) + "\r\n");
                }
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText("line no:" + i + "\t" + ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
            finally 
            {
                if (_thread != null)
                    _thread.Abort();
            }
        }
        /// <summary>
        /// 为分类列添加值
        /// </summary>
        /// <param name="cell">分类所对应的列对象</param>
        /// <param name="standards">空口/核心网涉及的标准多关键字组合字符串</param>
        /// <param name="projects">Project多关键字组合字符串</param>
        private void SetTypeValue(ICell cell, string standards, string projects)
        {
            try
            {
                string[] standard_array = standards.Split('\n');
                for (int i = 0; i < standard_array.Length; i++)
                    if (standard_array[i] != "" && standard_array[i].Contains("."))
                        standard_array[i] = standard_array[i] == "" ? "" : standard_array[i].Substring(0, standard_array[i].IndexOf("."));
                string[] project_array = projects.Split('\n');
                List<string> type_array = new List<string>();//存储类别关键字的集合
                //通过project查询字典
                foreach (string project in project_array)
                {
                    foreach (string key in Sysinfo.XLS_TYPE_DICTIONARA.Keys)
                    {
                        if (Array.IndexOf(Sysinfo.XLS_TYPE_DICTIONARA[key].Projects.ToArray(), project) != -1 && Array.IndexOf(type_array.ToArray(), key) == -1)
                        {
                            type_array.Add(key);
                            continue;
                        }
                    }
                }
                //通过standard查询字典
                foreach (string standard in standard_array)
                {
                    foreach (string key in Sysinfo.XLS_TYPE_DICTIONARA.Keys)
                    {
                        if (Array.IndexOf(Sysinfo.XLS_TYPE_DICTIONARA[key].Standards.ToArray(), standard) != -1 && Array.IndexOf(type_array.ToArray(), key) == -1)
                        {
                            type_array.Add(key);
                            continue;
                        }
                    }
                }
                //开始遍历类别集合，从而生成类别数据
                string type = "";
                bool uk = Array.IndexOf(type_array.ToArray(), "UK") != -1 ? true : false;//是否存在UK
                for (int j = 0; j < type_array.Count; j++)
                {
                    if (type_array[j] == "OT" && uk)
                        continue;
                    if (j == type_array.Count - 1)
                        type += type_array[j];
                    else
                        type += type_array[j] + ",";
                }
                cell.SetCellValue(type);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
