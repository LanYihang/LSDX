using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LSDX
{
    public partial class InternatType : Form
    {
        /// <summary>
        /// 子线程控制
        /// </summary>
        Thread _Thread = null;
        /// <summary>
        /// 存储制式的字典
        /// </summary>
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        /// <summary>
        /// 异常
        /// </summary>
        private Exception ep = null;
        public InternatType()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //源表Excel文件导入
        private void btnN1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog showFileDialog = new OpenFileDialog();
                showFileDialog.ShowDialog();
                string xlsPath = showFileDialog.FileName;
                if (Path.GetExtension(xlsPath) == ".xls" || Path.GetExtension(xlsPath) == ".xlsx")
                {
                    this.txtN1.Text = xlsPath;
                    if (this.txtN1.Text != "" && this.txtN2.Text != "")
                        this.btnExport.Enabled = true;
                }
                else
                    this.txtShow.AppendText("请选择Excel文件！\r\n");
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        //结果源Excel导入
        private void btnN2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog showFileDialog = new OpenFileDialog();
                showFileDialog.ShowDialog();
                string xlsPath = showFileDialog.FileName;
                if (Path.GetExtension(xlsPath) == ".xls" || Path.GetExtension(xlsPath) == ".xlsx")
                {
                    this.txtN2.Text = xlsPath;
                    if (this.txtN2.Text != "" && this.txtN2.Text != "")
                        this.btnExport.Enabled = true;
                }
                else
                    this.txtShow.AppendText("请选择Excel文件！\r\n");
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        //输出事件
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ep = null;
                txtShow.AppendText("开始加载基础数据表数据....\r\n");
                BaseInfoB info = new BaseInfoB();
                info.Path = this.txtN1.Text;
                info.Sheet = this.txtSheet.Text;
                info.Type = this.txtType.Text;
                info.Inter = this.txtInter.Text;
                _Thread = new Thread(new ParameterizedThreadStart(LoadBasicData));
                _Thread.Start(info);
                while (_Thread.IsAlive)
                {
                    continue;
                }
                if (!_Thread.IsAlive)
                    _Thread = null;
                if (ep != null)
                {
                    txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                    txtShow.AppendText(ep.Message + ", with stack trace: \r\n" + ep.StackTrace + "\r\n");
                }
                else
                {
                    if (dic.Keys.Count > 0)
                    {
                        txtShow.AppendText("数据加载完成，开始校验数据...\r\n");
                        info.Path = this.txtN2.Text.Trim();
                        info.Sheet = this.txtSheet1.Text.Trim();
                        info.Type = this.txtType1.Text.Trim();
                        info.Inter = this.txtInter1.Text.Trim();
                        info.SplitCode = Convert.ToChar(this.txtSplitCode.Text.Trim());
                        _Thread = new Thread(new ParameterizedThreadStart(CreateExcel));
                        _Thread.Start(info);
                        while (_Thread.IsAlive)
                        {
                            continue;
                        }
                        if (!_Thread.IsAlive)
                            _Thread = null;
                        if (ep != null)
                        {
                            txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                            txtShow.AppendText(ep.Message + ", with stack trace: \r\n" + ep.StackTrace + "\r\n");
                        }
                        else
                            txtShow.AppendText(@"生成文件成功，新文件路径为D:\" + Path.GetFileName(info.Path) + "\r\n");
                    }
                    else
                        txtShow.AppendText("基础数据源选择无效！\r\n");
                }
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        /// <summary>
        /// 加载基础数据
        /// </summary>
        private void LoadBasicData(object obj)
        {
            try
            {
                IWorkbook work_book = null;
                BaseInfoB info = null;
                if (obj is BaseInfoB)
                    info = obj as BaseInfoB;
                if (info != null)
                {
                    if (dic.Count > 0)
                        dic.Clear();
                    using (FileStream stream = new FileStream(info.Path, FileMode.Open))
                    {
                        if (Path.GetExtension(info.Path) == ".xls")
                            work_book = new HSSFWorkbook(stream);
                        else if (Path.GetExtension(info.Path) == ".xlsx")
                            work_book = new XSSFWorkbook(stream);
                        ISheet sheet = work_book.GetSheet(info.Sheet);//加载表单
                        int rowsCount = sheet.PhysicalNumberOfRows;
                        int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;
                        //获得project与空口/核心网涉及的标准的列号
                        int typeColumnIndex = 0, standardColumnIndex = 0;
                        for (int p = 0; p < colsCount; p++)
                        {
                            try
                            {
                                string column_name = sheet.GetRow(0).GetCell(p).ToString().Trim();
                                if (column_name == info.Type)
                                    typeColumnIndex = p;
                                else if (column_name == info.Inter)
                                    standardColumnIndex = p;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        for (int i = 0; i < rowsCount; i++)
                        {
                            if (i > 0)
                            {
                                string type = "", inter = "";
                                try
                                {
                                    type = sheet.GetRow(i).GetCell(typeColumnIndex).ToString().Trim();
                                    inter = sheet.GetRow(i).GetCell(standardColumnIndex).ToString().Trim();
                                    if (type != "" && inter != "")
                                    {
                                        string tmp = RemoveSpace(type);
                                        if (!dic.Keys.Contains(tmp))
                                            dic.Add(tmp, inter);
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ep = ex;
            }
        }

        private void CreateExcel(object obj)
        {
            int i = 0;
            try
            {
                BaseInfoB info = null;
                if (obj is BaseInfoB)
                    info = obj as BaseInfoB;
                if (info != null)
                {
                    IWorkbook work_book = null;
                    IWorkbook new_work_book = null;
                    using (FileStream stream = new FileStream(info.Path, FileMode.Open), new_stream = new FileStream("D:/" + Path.GetFileName(info.Path), FileMode.Create, FileAccess.Write))
                    {
                        if (Path.GetExtension(info.Path) == ".xls")
                        {
                            work_book = new HSSFWorkbook(stream);
                            new_work_book = new HSSFWorkbook();
                        }
                        else if (Path.GetExtension(info.Path) == ".xlsx")
                        {
                            work_book = new XSSFWorkbook(stream);
                            new_work_book = new XSSFWorkbook();
                        }
                        ISheet sheet = work_book.GetSheet(info.Sheet);//获得表一数据
                        ISheet new_sheet = new_work_book.CreateSheet("Sheet1");
                        int rowsCount = sheet.LastRowNum;
                        int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;
                        //获得标准号与制式的位置
                        int biaozhun_column_count = 0;//标准列的列号
                        int zhishi_column_count = 0;//制式的列号
                        for (int p = 0; p < colsCount; p++)
                        {
                            string column_name = sheet.GetRow(0).GetCell(p).ToString().Trim();
                            if (column_name == info.Type)
                                biaozhun_column_count = p;
                            else if (column_name == info.Inter)
                                zhishi_column_count = p;
                        }
                        //开始读取行数据
                        for (; i <= rowsCount; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null)
                                continue;
                            IRow new_row = new_sheet.CreateRow(i);
                            new_row.Height = 200 * 2;
                            string zhishi = "";//制式内容值
                            for (int j = 0; j < colsCount; j++)
                            {
                                ICell new_cell = new_row.CreateCell(j);
                                new_cell.SetCellType(CellType.STRING);
                                new_sheet.SetColumnWidth(j, 20 * 256);
                                if (i == 0)
                                {
                                    if (sheet.GetRow(i).GetCell(j) != null)
                                        new_cell.SetCellValue(sheet.GetRow(i).GetCell(j).ToString().Trim());
                                }
                                else
                                {
                                    if (j == biaozhun_column_count)
                                    {
                                        string biaoZhunHao = "";
                                        if (sheet.GetRow(i)!=null&&sheet.GetRow(i).GetCell(j) != null)
                                        {
                                            biaoZhunHao = sheet.GetRow(i).GetCell(j).ToString().Trim();
                                            string[] biaoZhunHaoArray = biaoZhunHao.Split(info.SplitCode);
                                            if (biaoZhunHaoArray.Length > 1)
                                            {
                                                foreach (string t in biaoZhunHaoArray)
                                                {
                                                    string tmp = RemoveSpace(t.Trim());
                                                    if (dic.Keys.Contains(tmp) && (!zhishi.Contains(dic[tmp])))
                                                        zhishi += dic[tmp] + " ";
                                                    if ((!dic.Keys.Contains(tmp)))
                                                        zhishi += "NA ";
                                                }
                                            }
                                            else if (biaoZhunHaoArray.Length == 1)
                                            {
                                                string tmp = RemoveSpace(biaoZhunHaoArray[0].Trim());
                                                if (dic.Keys.Contains(tmp))
                                                    zhishi = dic[tmp];
                                                else
                                                    zhishi = "NA";
                                            }
                                        }
                                        new_cell.SetCellValue(biaoZhunHao);
                                    }
                                    if (j == zhishi_column_count)
                                    {
                                        new_cell.SetCellValue(zhishi);
                                    }
                                    if (j != biaozhun_column_count && j != zhishi_column_count)
                                    {
                                        if (sheet.GetRow(i) != null && sheet.GetRow(i).GetCell(j) != null)
                                        {
                                            //new_cell.SetCellValue(sheet.GetRow(i).GetCell(j).ToString().Trim());
                                            //判定是否是日期格式
                                            string a = sheet.GetRow(i).GetCell(j).ToString().Trim();
                                            if (a != "" && a.Contains("月"))
                                            {
                                                DateTime time = Convert.ToDateTime(sheet.GetRow(i).GetCell(j).ToString().Trim());
                                                int year = time.Year;
                                                int month = time.Month;
                                                int day = time.Day;
                                                new_cell.SetCellValue(year + "/" + month + "/" + day);
                                            }
                                            else
                                            {
                                                new_cell.SetCellValue(a);
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        MemoryStream memory = new MemoryStream();
                        new_work_book.Write(memory);
                        byte[] buffer = memory.ToArray();
                        new_stream.Write(buffer, 0, buffer.Length);
                        new_stream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                ep = ex;
            }
        }

        private string RemoveSpace(string code)
        {
            try
            {
                if (code.Contains(" "))
                {
                    code = code.Replace(" ", "");
                    RemoveSpace(code);
                }
                return code;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    class BaseInfoB
    {
        private string path;
        private string sheet;
        private string type;
        private string inter;
        private char splitCode;
        /// <summary>
        /// Excel路径
        /// </summary>
        public string Path {
            get { return path; }
            set { path = value; }
        }
        /// <summary>
        /// Excel表单名称
        /// </summary>
        public string Sheet
        {
            get { return sheet; }
            set { sheet = value; }
        }
        /// <summary>
        /// 网络
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        /// <summary>
        /// 制式
        /// </summary>
        public string Inter
        {
            get { return inter; }
            set { inter = value; }
        }
        /// <summary>
        /// 制式分隔符
        /// </summary>
        public char SplitCode
        {
            get { return splitCode; }
            set { splitCode = value; }
        }
    }
}
