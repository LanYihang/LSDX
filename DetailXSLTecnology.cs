using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Com.Ls.Lsdx.Bean;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LSDX
{
    public partial class DetailXSLTecnology : Form
    {
        private Thread thread = null;
        private Dictionary<ExcelOtherRowBean, bool> cn_dictionary = new Dictionary<ExcelOtherRowBean, bool>();

        public DetailXSLTecnology()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

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

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn_dictionary.Count > 0)
                    cn_dictionary.Clear();
                thread = new Thread(new ThreadStart(LoadExcelFile));
                thread.Start();
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }

        //加载Excel文件
        private void LoadExcelFile()
        {
            int i = 0;
            try
            {
                IWorkbook work_book = null;
                IWorkbook new_work_book = null;
                int cn_cols = 0;//CN同族申请号列数
                int type_cols = 0;//分类列数
                int technology_cols = 0;//技术分类列数
                //处理CN同族申请号
                using (FileStream stream = new FileStream(this.txtXLSPath.Text, FileMode.Open))
                {
                    txtShow.AppendText("Start reading excel file...\r\n");
                    if (Path.GetExtension(this.txtXLSPath.Text) == ".xls")
                        work_book = new HSSFWorkbook(stream);
                    else if (Path.GetExtension(this.txtXLSPath.Text) == ".xlsx")
                        work_book = new XSSFWorkbook(stream);
                    ISheet sheet = work_book.GetSheet("Sheet1") == null ? work_book.GetSheet("去重") : work_book.GetSheet("Sheet1");//获得表一数据
                    if (sheet == null) 
                    {
                        txtShow.AppendText("\r\n**************** Read Sheet failed **************** \r\n");
                    }
                    int rowsCount = sheet.PhysicalNumberOfRows;
                    int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;
                    for (int rowIndex = 0; rowIndex <= rowsCount; rowIndex++)
                    {
                        if (cn_cols == 0 && type_cols == 0)//获得列名对应的列数
                        {
                            for (int colIndex = 0; colIndex < colsCount; colIndex++)
                            {
                                if (sheet.GetRow(rowIndex).GetCell(colIndex) != null && sheet.GetRow(rowIndex).GetCell(colIndex).ToString() == "分类")
                                    type_cols = colIndex;
                                if (sheet.GetRow(rowIndex).GetCell(colIndex) != null && sheet.GetRow(rowIndex).GetCell(colIndex).ToString() == "技术分类")
                                    technology_cols = colIndex;
                                if (sheet.GetRow(rowIndex).GetCell(colIndex) != null && sheet.GetRow(rowIndex).GetCell(colIndex).ToString() == "CN同族申请号")
                                    cn_cols = colIndex;
                            }
                        }
                        if (rowIndex > 1 && sheet.GetRow(rowIndex) != null && sheet.GetRow(rowIndex).GetCell(cn_cols) != null)
                        {
                            IRow r = sheet.GetRow(rowIndex);
                            if (r != null)
                            {
                                string type = r.GetCell(type_cols) != null ? r.GetCell(type_cols).ToString().Trim() : "";//分类
                                string technology_type = r.GetCell(technology_cols) != null ? r.GetCell(technology_cols).ToString().Trim() : "";//技术分类
                                string cn_filingn = r.GetCell(cn_cols) != null ? r.GetCell(cn_cols).ToString().Trim().ToUpper() : "";//CN同族申请号
                                if (type != "" && technology_type != "" && cn_filingn != "")
                                {
                                    txtShow.AppendText(rowIndex + " " + sheet.GetRow(rowIndex).GetCell(cn_cols).ToString() + "\r\n");
                                    ExcelOtherRowBean other_excel_bean = new ExcelOtherRowBean();
                                    other_excel_bean.InformationType = type;
                                    other_excel_bean.TechnologyType = technology_type;
                                    other_excel_bean.CNFilingn = cn_filingn;
                                    //将分类与CN同族申请号唯一的组合存入字典当中，保证字典中的数据都是唯一组合
                                    if (Array.IndexOf(cn_dictionary.Keys.ToArray(), other_excel_bean) == -1)
                                        cn_dictionary.Add(other_excel_bean, false);
                                }
                            }
                        }
                    }
                }
                txtShow.AppendText("Start generating excel files,please later...\r\n");
                //开始复制数据
                using (FileStream stream = new FileStream(this.txtXLSPath.Text, FileMode.Open), newStream = new FileStream("D:/" + Path.GetFileName(this.txtXLSPath.Text), FileMode.Create, FileAccess.Write))
                {
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
                    ISheet sheet = work_book.GetSheet("Sheet1") == null ? work_book.GetSheet("去重") : work_book.GetSheet("Sheet1"); ;
                    ISheet new_sheet = new_work_book.CreateSheet("Sheet1");//创建的新表格
                    int rowCount = sheet.PhysicalNumberOfRows;//获得最大的行数
                    int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;//获得每行最多的列数
                    for (i = 0; i < rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            IRow new_row = new_sheet.CreateRow(i);
                            new_row.Height = 200 * 2;
                            string row_type = row.GetCell(type_cols) == null ? "" : row.GetCell(type_cols).ToString().Trim();//获得行中联网分类值
                            string technology_type = row.GetCell(technology_cols) == null ? "" : row.GetCell(technology_cols).ToString().Trim();//获得行中技术分类值
                            string cn_filingn = row.GetCell(cn_cols) == null ? "" : row.GetCell(cn_cols).ToString().Trim().ToUpper();//获得行中CN同族申请号
                            ExcelOtherRowBean other_row_bean = new ExcelOtherRowBean();//行对应的处理数据
                            other_row_bean.InformationType = row_type;
                            other_row_bean.TechnologyType = technology_type;
                            other_row_bean.CNFilingn = cn_filingn;
                            other_row_bean.LineNum = i;//数据对应行号
                            string copy_cn_filingn = "";//在行中是否已出现CNFilingn
                            for (int j = 0; j < colsCount + 19; j++)
                            {
                                ICell new_cell = new_row.CreateCell(j);
                                new_cell.SetCellType(CellType.STRING);
                                new_sheet.SetColumnWidth(j, 20 * 256);
                                if (i > 0)//数据开始
                                {
                                    if (j == cn_cols + 1)//4G 核心网 CN同族申请号
                                    {
                                        if (row_type == "4G" && technology_type.Contains("核心网") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现4G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            if (technology_type != "核心网/空口" && technology_type != "空口/核心网")
                                                cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "DG" && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于4G是否存在
                                            ExcelOtherRowBean demo_4g_bean=new ExcelOtherRowBean();
                                            demo_4g_bean.InformationType = "4G";
                                            demo_4g_bean.TechnologyType = technology_type;
                                            demo_4g_bean.CNFilingn = cn_filingn;
                                            if(Array.IndexOf(cn_dictionary.Keys.ToArray(),demo_4g_bean)==-1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if (row_type == "TG" && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于4G是否存在
                                            ExcelOtherRowBean demo_4g_bean = new ExcelOtherRowBean();
                                            demo_4g_bean.InformationType = "4G";
                                            demo_4g_bean.TechnologyType = technology_type;
                                            demo_4g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_4g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 2)//4G 空口 CN同族申请号
                                    {
                                        if (row_type == "4G" && technology_type.Contains("空口") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现4G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "DG" && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于4G是否存在
                                            ExcelOtherRowBean demo_4g_bean = new ExcelOtherRowBean();
                                            demo_4g_bean.InformationType = "4G";
                                            demo_4g_bean.TechnologyType = technology_type;
                                            demo_4g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_4g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if (row_type == "TG" && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于4G是否存在
                                            ExcelOtherRowBean demo_4g_bean = new ExcelOtherRowBean();
                                            demo_4g_bean.InformationType = "4G";
                                            demo_4g_bean.TechnologyType = technology_type;
                                            demo_4g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_4g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 3)//4G OTHERS CN同族申请号对应的列
                                    {
                                        if (row_type == "4G" && technology_type == "OTHERS" && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现4G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "DG" && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于4G是否存在
                                            ExcelOtherRowBean demo_4g_bean = new ExcelOtherRowBean();
                                            demo_4g_bean.InformationType = "4G";
                                            demo_4g_bean.TechnologyType = technology_type;
                                            demo_4g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_4g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if (row_type == "TG" && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于4G是否存在
                                            ExcelOtherRowBean demo_4g_bean = new ExcelOtherRowBean();
                                            demo_4g_bean.InformationType = "4G";
                                            demo_4g_bean.TechnologyType = technology_type;
                                            demo_4g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_4g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 4)//3G 核心网 CN同族申请号
                                    {
                                        if (row_type == "3G" && technology_type.Contains("核心网") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现3G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            if (technology_type != "核心网/空口" && technology_type != "空口/核心网")
                                                cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "DG" && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if (row_type == "TG" && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 5)//3G 空口 CN同族申请号
                                    {
                                        if (row_type == "3G" && technology_type.Contains("空口") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现3G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "DG" && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if (row_type == "TG" && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 6)//3G OTHERS CN同族申请号
                                    {
                                        if (row_type == "3G" && technology_type == "OTHERS" && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现3G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "DG" && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if (row_type == "TG" && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于3G是否存在
                                            ExcelOtherRowBean demo_3g_bean = new ExcelOtherRowBean();
                                            demo_3g_bean.InformationType = "3G";
                                            demo_3g_bean.TechnologyType = technology_type;
                                            demo_3g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_3g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 7)//2G 核心网 CN同族申请号
                                    {
                                        if (row_type == "2G" && technology_type.Contains("核心网") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现2G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            if (technology_type != "核心网/空口" && technology_type != "空口/核心网")
                                                cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "TG" && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于2G是否存在
                                            ExcelOtherRowBean demo_2g_bean = new ExcelOtherRowBean();
                                            demo_2g_bean.InformationType = "2G";
                                            demo_2g_bean.TechnologyType = technology_type;
                                            demo_2g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_2g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type.Contains("核心网") && cn_filingn != "")
                                        {
                                            //该类型对于2G是否存在
                                            ExcelOtherRowBean demo_2g_bean = new ExcelOtherRowBean();
                                            demo_2g_bean.InformationType = "2G";
                                            demo_2g_bean.TechnologyType = technology_type;
                                            demo_2g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_2g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 8)//2G 空口 CN同族申请号
                                    {
                                        if (row_type == "2G" && technology_type.Contains("空口") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现2G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "TG" && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于2G是否存在
                                            ExcelOtherRowBean demo_2g_bean = new ExcelOtherRowBean();
                                            demo_2g_bean.InformationType = "2G";
                                            demo_2g_bean.TechnologyType = technology_type;
                                            demo_2g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_2g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type.Contains("空口") && cn_filingn != "")
                                        {
                                            //该类型对于2G是否存在
                                            ExcelOtherRowBean demo_2g_bean = new ExcelOtherRowBean();
                                            demo_2g_bean.InformationType = "2G";
                                            demo_2g_bean.TechnologyType = technology_type;
                                            demo_2g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_2g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 9)//2G OTHERS CN同族申请号
                                    {
                                        if (row_type == "2G" && technology_type == "OTHERS" && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现2G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                        if (row_type == "TG" && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于2G是否存在
                                            ExcelOtherRowBean demo_2g_bean = new ExcelOtherRowBean();
                                            demo_2g_bean.InformationType = "2G";
                                            demo_2g_bean.TechnologyType = technology_type;
                                            demo_2g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_2g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type=="OTHERS" && cn_filingn != "")
                                        {
                                            //该类型对于2G是否存在
                                            ExcelOtherRowBean demo_2g_bean = new ExcelOtherRowBean();
                                            demo_2g_bean.InformationType = "2G";
                                            demo_2g_bean.TechnologyType = technology_type;
                                            demo_2g_bean.CNFilingn = cn_filingn;
                                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), demo_2g_bean) == -1)//如果该DG值对应的4G于字典中不存在则显示
                                                new_cell.SetCellValue(cn_filingn);
                                        }
                                    }
                                    else if (j == cn_cols + 10)//TG 核心网 CN同族申请号
                                    {
                                        if (row_type == "TG" && technology_type.Contains("核心网") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现TG及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            if (technology_type != "核心网/空口" && technology_type != "空口/核心网")
                                                cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 11)//TG 空口 CN同族申请号
                                    {
                                        if (row_type == "TG" && technology_type.Contains("空口") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现TG及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 12)//TG OTHERS CN同族申请号
                                    {
                                        if (row_type == "TG" && technology_type == "OTHERS" && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现TG及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 13)//3G/2G 核心网 CN同族申请号
                                    {
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type.Contains("核心网") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现3G/2G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            if (technology_type != "核心网/空口" && technology_type != "空口/核心网")
                                                cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 14)//3G/2G 空口 CN同族申请号
                                    {
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type.Contains("空口") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现3G/2G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 15)//3G/2G OTHERS CN同族申请号
                                    {
                                        if ((row_type == "3G/2G" || row_type == "2G/3G") && technology_type == "OTHERS" && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现3G/2G及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 16)//DG 核心网 CN同族申请号
                                    {
                                        if (row_type == "DG" && technology_type.Contains("核心网") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现DG及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            if (technology_type != "核心网/空口" && technology_type != "空口/核心网")
                                                cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 17)//DG 空口 CN同族申请号
                                    {
                                        if (row_type == "DG" && technology_type.Contains("空口") && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现DG及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 18)//DG OTHERS CN同族申请号
                                    {
                                        if (row_type == "DG" && technology_type == "OTHERS" && cn_filingn != "" && cn_dictionary.Keys.Contains(other_row_bean) && cn_dictionary[other_row_bean] == false)
                                        {
                                            //第一次出现DG及某技术分类
                                            new_cell.SetCellValue(cn_filingn);
                                            cn_dictionary[other_row_bean] = true;//修改值代表已出现
                                            copy_cn_filingn = cn_filingn;
                                        }
                                    }
                                    else if (j == cn_cols + 19)//重复 CN同族申请号
                                    {
                                        if (copy_cn_filingn == "")//对于行而言如果该值是空的就代表在之前的列数据中并未出现
                                            new_cell.SetCellValue(cn_filingn);
                                    }
                                    else
                                    {
                                        ICell cell = null;
                                        if (j <= cn_cols)
                                            cell = row.GetCell(j);
                                        else
                                            cell = row.GetCell(j - 19);
                                        if (cell != null)
                                            new_cell.SetCellValue(cell.ToString().Trim());
                                    }
                                }
                                else //标题部分
                                {
                                    if (j == cn_cols + 1)
                                        new_cell.SetCellValue(i == 0 ? "4G 核心网 CN同族申请号" : "");
                                    else if (j == cn_cols + 2)
                                        new_cell.SetCellValue(i == 0 ? "4G 空口 CN同族申请号" : "");
                                    else if (j == cn_cols + 3)
                                        new_cell.SetCellValue(i == 0 ? "4G OTHERS CN同族申请号" : "");
                                    else if (j == cn_cols + 4)
                                        new_cell.SetCellValue(i == 0 ? "3G 核心网 CN同族申请号" : "");
                                    else if (j == cn_cols + 5)
                                        new_cell.SetCellValue(i == 0 ? "3G 空口 CN同族申请号" : "");
                                    else if (j == cn_cols + 6)
                                        new_cell.SetCellValue(i == 0 ? "3G OTHERS CN同族申请号" : "");
                                    else if (j == cn_cols + 7)
                                        new_cell.SetCellValue(i == 0 ? "2G 核心网 CN同族申请号" : "");
                                    else if (j == cn_cols + 8)
                                        new_cell.SetCellValue(i == 0 ? "2G 空口 CN同族申请号" : "");
                                    else if (j == cn_cols + 9)
                                        new_cell.SetCellValue(i == 0 ? "2G OTHERS CN同族申请号" : "");
                                    else if (j == cn_cols + 10)
                                        new_cell.SetCellValue(i == 0 ? "TG 核心网 CN同族申请号" : "");
                                    else if (j == cn_cols + 11)
                                        new_cell.SetCellValue(i == 0 ? "TG 空口 CN同族申请号" : "");
                                    else if (j == cn_cols + 12)
                                        new_cell.SetCellValue(i == 0 ? "TG OTHERS CN同族申请号" : "");
                                    else if (j == cn_cols + 13)
                                        new_cell.SetCellValue(i == 0 ? "3G/2G 核心网 CN同族申请号" : "");
                                    else if (j == cn_cols + 14)
                                        new_cell.SetCellValue(i == 0 ? "3G/2G 空口 CN同族申请号" : "");
                                    else if (j == cn_cols + 15)
                                        new_cell.SetCellValue(i == 0 ? "3G/2G OTHERS CN同族申请号" : "");
                                    else if (j == cn_cols + 16)
                                        new_cell.SetCellValue(i == 0 ? "DG 核心网 CN同族申请号" : "");
                                    else if (j == cn_cols + 17)
                                        new_cell.SetCellValue(i == 0 ? "DG 空口 CN同族申请号" : "");
                                    else if (j == cn_cols + 18)
                                        new_cell.SetCellValue(i == 0 ? "DG OTHERS CN同族申请号" : "");
                                    else if (j == cn_cols + 19)
                                        new_cell.SetCellValue(i == 0 ? "重复 CN同族申请号" : "");
                                    else
                                    {
                                        ICell cell = null;
                                        if (j <= cn_cols)
                                            cell = row.GetCell(j);
                                        else
                                            cell = row.GetCell(j-19);
                                        if (cell != null)
                                            new_cell.SetCellValue(cell.ToString().Trim());
                                    }
                                }
                            }
                        }
                    }
                    MemoryStream memory_stream = new MemoryStream();
                    new_work_book.Write(memory_stream);
                    byte[] buf = memory_stream.ToArray();
                    newStream.Write(buf, 0, buf.Length);
                    newStream.Flush();
                    txtShow.AppendText("CN同族申请号唯一总数：" + cn_dictionary.Count.ToString() + "\r\n");
                    txtShow.AppendText("Generate excel file path is D:/" + Path.GetFileName(this.txtXLSPath.Text) + "\r\n");
                }
            }
            catch (Exception ex)
            {
                if (txtShow != null)
                {
                    txtShow.AppendText("\r\n**************** Read Excel failed **************** \r\n");
                    txtShow.AppendText(i + " " + ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
                }
            }
            finally
            {
                if (this.thread != null)
                    this.thread.Abort();
            }
        }
    }
}
