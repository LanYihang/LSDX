using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using Com.Ls.Lsdx.Bean;
using Com.Ls.Lsdx.Util;

namespace LSDX
{
    public partial class s : Form
    {
        private Thread thread = null;
        private Dictionary<ExcelRowBean, bool> cn_dictionary = new Dictionary<ExcelRowBean, bool>();//保存条件+数据字典
        private Dictionary<String, bool> parseDic = new Dictionary<string, bool>();//保存数据字典
        /// <summary>
        /// 表单名称
        /// </summary>
        private string sheetName = "";
        /// <summary>
        /// 标题行行数
        /// </summary>
        private int lineNo = 1;
        /// <summary>
        /// 条件列名称
        /// </summary>
        private string lineConditions = "";
        /// <summary>
        /// 处理列名称
        /// </summary>
        private string parseName = "";
        /// <summary>
        /// 选择的Excel文件路径
        /// </summary>
        private string chooseXLSPath = "";

        public s()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void DetailXSL_Load(object sender, EventArgs e)
        {
            this.cbType.Text = this.txtTiaojian.Text+"非3GPP";
            this.lblExplain.Text = "1)处理Excel表标题行仅限为一行且标题列不允许为空\r\n\n2)条件列包含的值范围如：4G,3G,2G," +
                "TG,,DG,4G/3G/2G,4G/3G/2G,OT,UK";
        }

        //选择文件事件
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnCount.Enabled = false;
                OpenFileDialog showFileDialog = new OpenFileDialog();
                showFileDialog.ShowDialog();
                string xlsPath = showFileDialog.FileName;
                if (Path.GetExtension(xlsPath) == ".xls" || Path.GetExtension(xlsPath) == ".xlsx")
                {
                    this.txtXLSPath.Text = xlsPath;
                    this.btnStart.Enabled = true;
                    this.txtShow.Text = "";
                    this.txtShow.AppendText("选择文件有效，请点击开始*******\r\n");
                }
                else
                {
                    this.txtShow.AppendText("请选择Excel文件！\r\n");
                }
            }catch(Exception ex)
            {
                txtShow.AppendText("\r\n**************** 选择文件异常 **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }

        //条件文本框值发生改变
        private void txtTiaojian_TextChanged(object sender, EventArgs e)
        {
            this.cbType.Text = this.txtTiaojian.Text + "非3GPP";
        }

        //开始按钮事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtSheet.Text.Trim() == "" || this.txtTiaojian.Text.Trim() == "" || this.txtAnswerLine.Text.Trim() == "") 
                {
                    this.txtShow.AppendText("参数文本框的内容不允许为空*******\r\n");
                    return;
                }
                this.sheetName = this.txtSheet.Text.Trim();
                this.parseName = this.txtAnswerLine.Text.Trim();
                this.lineConditions = this.txtTiaojian.Text.Trim();
                this.chooseXLSPath = this.txtXLSPath.Text;
                if (cn_dictionary.Count > 0)
                    cn_dictionary.Clear();
                thread = new Thread(new ThreadStart(ProcessDataForNewExcel));
                thread.Start();
            }catch(Exception ex)
            {
                txtShow.AppendText("\r\n**************** 操作Excel文件异常 **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }

        //计数按钮事件
        private void btnCount_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCount();
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** 计数异常 **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }

        private void LoadCount()
        {
            try
            {
                int shouRangRenCellIndex = 0, lawStatusCellIndex = 0, conditionCellIndex = 0;
                string targeExcelPath = Sysinfo.NEW_EXCEL_PATH + Path.GetFileName(this.txtXLSPath.Text);
                string shouRangRen = this.cbSRR.Checked ? "受让人" : "";
                string lawStatus = this.cbFLZT.Checked ? "法律状态" : "";
                this.txtShow.AppendText("加载结果Excel文件：" + targeExcelPath + "\r\n");
                ISheet sheet = ExcelUtil.GetExcelSheet(targeExcelPath, this.sheetName);
                if (shouRangRen != "")
                {
                    Dictionary<string, int> dic = ExcelUtil.LoadColumnData(sheet, this.lineNo, shouRangRen);
                    shouRangRenCellIndex = dic.Keys.Contains(shouRangRen) ? dic[shouRangRen] : 0;
                    if (shouRangRenCellIndex == 0)
                    {
                        this.txtShow.AppendText("表中不能存在受让人列，请重新生成\r\n");
                        this.btnCount.Enabled = false;
                        return;
                    }
                }
                if (lawStatus != "")
                {
                    Dictionary<string, int> dic = ExcelUtil.LoadColumnData(sheet, this.lineNo, lawStatus);
                    lawStatusCellIndex = dic.Keys.Contains(lawStatus) ? dic[lawStatus] : 0;
                    if (lawStatusCellIndex == 0)
                    {
                        this.txtShow.AppendText("表中不能存在法律状态列，请重新生成\r\n");
                        this.btnCount.Enabled = false;
                        return;
                    }
                }
                Dictionary<string, int> dict = ExcelUtil.LoadColumnData(sheet, this.lineNo, lineConditions);
                conditionCellIndex = dict.Keys.Contains(lineConditions) ? dict[lineConditions] : 0;
                if (conditionCellIndex == 0)
                {
                    this.txtShow.AppendText("表中不能存在" + lineConditions + "列，请重新生成\r\n");
                    this.btnCount.Enabled = false;
                    return;
                }
                if (this.cbFLZT.Checked == false && this.cbSRR.Checked == false && this.cbType.Checked == false)
                    this.txtShow.AppendText("数据总数：" + (sheet.PhysicalNumberOfRows - 1).ToString());
                else
                {
                    int count = 0;
                    for (int rowIndex = 1; rowIndex < sheet.PhysicalNumberOfRows; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (this.cbFLZT.Checked && this.cbSRR.Checked && this.cbType.Checked)
                        {
                            count = row.GetCell(lawStatusCellIndex).ToString().Trim() == "授权" && row.GetCell(shouRangRenCellIndex).ToString().Trim() != "" && row.GetCell(conditionCellIndex).ToString().Trim() != "非3GPP" ? count + 1 : count + 0;
                        }
                        else if (this.cbFLZT.Checked && this.cbSRR.Checked)
                        {
                            count = row.GetCell(lawStatusCellIndex).ToString().Trim() == "授权" && row.GetCell(shouRangRenCellIndex).ToString().Trim() != "" ? count + 1 : count + 0;
                        }
                        else if (this.cbFLZT.Checked && this.cbType.Checked)
                        {
                            count = row.GetCell(lawStatusCellIndex).ToString().Trim() == "授权" && row.GetCell(conditionCellIndex).ToString().Trim() != "非3GPP" ? count + 1 : count + 0;
                        }
                        else if (this.cbSRR.Checked && this.cbType.Checked)
                        {
                            count = row.GetCell(shouRangRenCellIndex).ToString().Trim() != "" && row.GetCell(conditionCellIndex).ToString().Trim() != "非3GPP" ? count + 1 : count + 0;
                        }
                        else if (this.cbFLZT.Checked)
                        {
                            count = row.GetCell(lawStatusCellIndex).ToString().Trim() == "授权" ? count + 1 : count + 0;
                        }
                        else if (this.cbSRR.Checked)
                        {
                            count = row.GetCell(shouRangRenCellIndex).ToString().Trim() != "" ? count + 1 : count + 0;
                        }

                        else if (this.cbType.Checked)
                        {
                            count = row.GetCell(conditionCellIndex).ToString().Trim() != "非3GPP" ? count + 1 : count + 0;
                        }
                    }
                    this.txtShow.AppendText("数据总数：" + count.ToString() + "\r\n");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 处理数据的主逻辑
        /// </summary>
        private void ProcessDataForNewExcel() 
        {
            try
            {
                this.txtShow.AppendText("开始加载Excel ******\r\n");
                ISheet sheet = ExcelUtil.GetExcelSheet(this.chooseXLSPath, this.sheetName);
                Dictionary<string, int> dic = ExcelUtil.LoadColumnData(sheet, this.lineNo, this.lineConditions, this.parseName);
                int parseCellIndex = dic.Keys.Contains(this.parseName) ? dic[this.parseName] : 0;//处理数据列位置
                int conditionCellIndex = dic.Keys.Contains(this.lineConditions) ? dic[this.lineConditions] : 0;//条件列位置
                if (conditionCellIndex == 0 || parseCellIndex == 0)
                {
                    this.txtShow.AppendText("选择的列名称在Excel文件中不存在 ******\r\n");
                    return;
                }
                txtShow.AppendText("开始读取相关数据 ******\r\n");
                string targetPath = Sysinfo.NEW_EXCEL_PATH + Path.GetFileName(this.chooseXLSPath);
                IWorkbook workBook = ExcelUtil.CreateExcelWorkBook(this.sheetName, Path.GetExtension(targetPath));
                LoadDetailDictionaryData(sheet, conditionCellIndex, parseCellIndex);
                txtShow.AppendText("读取数据成功 ******\r\n");
                //设置文字居中显示
                ICellStyle cellStyle = workBook.CreateCellStyle();
                cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;//设置单元格居中显示
                cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER_SELECTION;
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                ISheet newSheet = workBook.GetSheetAt(0);
                newSheet.DefaultRowHeight = 20 * 20;//设置默认行高度
                txtShow.AppendText("开始处理数据并生成新Excel文件,请稍等 ******\r\n");
                MadeNewSheet(sheet, ref newSheet, cellStyle, conditionCellIndex, parseCellIndex);
                ExcelUtil.CreateExcelFile(workBook, targetPath);//生成Excel文件
                txtShow.AppendText(this.parseName + "唯一总数：" + cn_dictionary.Count.ToString() + "\r\n");
                txtShow.AppendText("新Excel文件位置：" + targetPath + "\r\n");
                //激活计数按钮
                this.btnCount.Enabled = true;
            }
            catch (Exception ex)
            {
                txtShow.AppendText("\r\n**************** 操作Excel文件异常 **************** \r\n");
                txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
            finally 
            {
                if (Thread.CurrentThread.IsAlive)
                    Thread.CurrentThread.Abort();
            }
        }
        /// <summary>
        /// 加载原始表中处理的行数据进入字典表
        /// </summary>
        /// <param name="baseSheet">原始表对象</param>
        /// <param name="conditionCellIndex">条件列位置</param>
        /// <param name="parseCellIndex">处理列位置</param>
        private void LoadDetailDictionaryData(ISheet baseSheet, int conditionCellIndex, int parseCellIndex)
        {
            try
            {
                int rowCount = baseSheet.PhysicalNumberOfRows;
                int columnCount = baseSheet.GetRow(this.lineNo - 1).PhysicalNumberOfCells;
                for (int rowIndex = this.lineNo; rowIndex < rowCount;rowIndex++ ) 
                {
                    IRow oldRow = baseSheet.GetRow(rowIndex);
                    if (oldRow != null) 
                    {
                        string condition = oldRow.GetCell(conditionCellIndex) != null ? oldRow.GetCell(conditionCellIndex).ToString().Trim().ToUpper() : "";
                        string parse = oldRow.GetCell(parseCellIndex) != null ? oldRow.GetCell(parseCellIndex).ToString().Trim().ToUpper() : "";
                        if (condition != "" && parse != "")
                        {
                            txtShow.AppendText("No." + rowIndex + " " + condition + ": " + parse + "\r\n");
                            ExcelRowBean excelBean = new ExcelRowBean();
                            excelBean.InformationType = condition;
                            excelBean.CNFilingn = parse;
                            //将分类与CN同族申请号唯一的组合存入字典当中，保证字典中的数据都是唯一组合
                            if (Array.IndexOf(cn_dictionary.Keys.ToArray(), excelBean) == -1)
                                cn_dictionary.Add(excelBean, false);
                            //将CN同组申请号存入字典
                            if (!parseDic.Keys.Contains(parse))
                                parseDic.Add(parse, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生成新Excel表单
        /// </summary>
        /// <param name="oldSheet">原始表单对象</param>
        /// <param name="newSheet">新表单对象</param>
        /// <param name="cellStyle">表格的样式</param>
        /// <param name="conditionCellIndex">条件列位置</param>
        /// <param name="parseCellIndex">处理数据列位置</param>
        private void MadeNewSheet(ISheet oldSheet,ref ISheet newSheet,ICellStyle cellStyle,int conditionCellIndex,int parseCellIndex)
        {
            int rowIndex = 0;
            try
            {
                int rowCount = oldSheet.PhysicalNumberOfRows;
                int oldColumnCount = oldSheet.GetRow(this.lineNo-1).PhysicalNumberOfCells;//原始表的列总数，默认加载第一行的有效列
                int newColumnCount = oldColumnCount + 9;
                for (rowIndex = 0; rowIndex < rowCount;rowIndex++ ) 
                {
                    IRow oldRow = oldSheet.GetRow(rowIndex);
                    if (oldRow != null) 
                    {
                        IRow newRow = newSheet.CreateRow(rowIndex);
                        string condition = oldRow.GetCell(conditionCellIndex) == null ? "" : oldRow.GetCell(conditionCellIndex).ToString().Trim().ToUpper();//获得行中联网分类值
                        string parse = oldRow.GetCell(parseCellIndex) == null ? "" : oldRow.GetCell(parseCellIndex).ToString().Trim().ToUpper();//获得行中CN同族申请号
                        ExcelRowBean original3GTo4G = null;//3G数据转化成4G用以判定DG列数据的输出
                        ExcelRowBean original = new ExcelRowBean();//读取行中需要处理的数据对象
                        original.InformationType = condition;
                        original.CNFilingn = parse;
                        string copy_cn_filingn = "";//在行中是否已出现CNFilingn
                        for (int columnIndex = 0; columnIndex < newColumnCount; columnIndex++) 
                        {
                            ICell newCell = newRow.CreateCell(columnIndex);
                            newCell.SetCellValue("\t");//赋默认值，方便出现边框
                            newCell.CellStyle = cellStyle;
                            newSheet.SetColumnWidth(columnIndex, 20 * 256);
                            int rowspan = 0, colspan = 0;//合并的行数 列数
                            #region 标题行处理
                            if (rowIndex == 0)
                            {
                                //标题行
                                if (columnIndex == parseCellIndex + 1)
                                    newCell.SetCellValue("4G " + this.parseName);
                                else if (columnIndex == parseCellIndex + 2)
                                    newCell.SetCellValue("3G " + this.parseName);
                                else if (columnIndex == parseCellIndex + 3)
                                    newCell.SetCellValue("2G " + this.parseName);
                                else if (columnIndex == parseCellIndex + 4)
                                    newCell.SetCellValue("4G/3G(DG) " + this.parseName);
                                else if (columnIndex == parseCellIndex + 5)
                                    newCell.SetCellValue("4G/3G/2G(TG) " + this.parseName);
                                else if (columnIndex == parseCellIndex + 6)
                                    newCell.SetCellValue("OT " + this.parseName);
                                else if (columnIndex == parseCellIndex + 7)
                                    newCell.SetCellValue("UK " + this.parseName);
                                else if (columnIndex == parseCellIndex + 8)
                                    newCell.SetCellValue("重复 " + this.parseName);
                                else if (columnIndex == parseCellIndex + 9)
                                    newCell.SetCellValue("仅" + this.parseName + "去重");
                                else
                                {
                                    //除新添加的列以外的表的原始单元格信息复制
                                    ICell oldCell = oldRow.GetCell(columnIndex <= parseCellIndex ? columnIndex : columnIndex - 9);
                                    if (oldCell != null)
                                    {
                                        if (columnIndex < parseCellIndex + 1)
                                            ExcelUtil.GetTdMergedInfo(oldSheet, rowIndex, columnIndex, out rowspan, out colspan);
                                        else if (columnIndex > parseCellIndex + 9)
                                            ExcelUtil.GetTdMergedInfo(oldSheet, rowIndex, columnIndex - 9, out rowspan, out colspan);
                                        newCell.SetCellValue(oldCell.ToString().Trim());
                                        if (rowspan > 1 || colspan > 1)
                                        {
                                            CellRangeAddress a = new CellRangeAddress(rowIndex, rowIndex + (rowspan - 1), columnIndex, columnIndex + (colspan - 1));
                                            newSheet.AddMergedRegion(a);
                                        }
                                    }
                                    else
                                        newCell.SetCellValue("\t");
                                }
                            }
                            #endregion
                            #region 处理数据主体
                            else 
                            {
                                //开始读取行数据并处理
                                if (columnIndex == parseCellIndex + 1)//4G 列
                                {
                                    if (original.CNFilingn != "" && cn_dictionary.Keys.Contains(original) && cn_dictionary[original] == false && (original.InformationType == "4G" || original.InformationType == "DG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") == -1) || original.InformationType == "TG") || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1))//表示第一次出现
                                    {
                                        if (original.InformationType == "4G")
                                        {
                                            newCell.SetCellValue(original.CNFilingn);
                                            cn_dictionary[original] = true;//表示已出现过
                                            copy_cn_filingn = original.CNFilingn;
                                        }
                                        else if (original.InformationType == "DG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") == -1))
                                        {
                                            //判定是否在4G 3G里出现过,出现过将数据转移到重复列
                                            ExcelRowBean de_4G_bean = new ExcelRowBean();
                                            de_4G_bean.InformationType = "4G";
                                            de_4G_bean.CNFilingn = original.CNFilingn;
                                            if (!cn_dictionary.Keys.Contains(de_4G_bean))
                                                newCell.SetCellValue(original.CNFilingn);
                                        }
                                        else if (original.InformationType == "TG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1))
                                        {
                                            //是否在4G中包含，不包含则显示出来
                                            ExcelRowBean tg_4G_bean = new ExcelRowBean();
                                            tg_4G_bean.InformationType = "4G";
                                            tg_4G_bean.CNFilingn = original.CNFilingn;
                                            if (!cn_dictionary.Keys.Contains(tg_4G_bean))
                                                newCell.SetCellValue(original.CNFilingn);
                                        }
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 2)//3G 列
                                {
                                    if (original.CNFilingn != "" && cn_dictionary.Keys.Contains(original) && cn_dictionary[original] == false && (original.InformationType == "3G" || original.InformationType == "DG" || original.InformationType == "TG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") == -1) || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1)))//表示第一次出现
                                    {
                                        if (original.InformationType == "3G")
                                        {
                                            newCell.SetCellValue(original.CNFilingn);
                                            cn_dictionary[original] = true;//表示已出现过
                                            original3GTo4G = new ExcelRowBean();
                                            original3GTo4G.InformationType = "4G";//3G对应的4G数据
                                            original3GTo4G.CNFilingn = original.CNFilingn;
                                            copy_cn_filingn = original.CNFilingn;
                                        }
                                        else if (original.InformationType == "DG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") == -1))
                                        {
                                            ExcelRowBean de_3G_bean = new ExcelRowBean();
                                            de_3G_bean.InformationType = "3G";
                                            de_3G_bean.CNFilingn = original.CNFilingn;
                                            if (!cn_dictionary.Keys.Contains(de_3G_bean))
                                                newCell.SetCellValue(original.CNFilingn);
                                        }
                                        else if (original.InformationType == "TG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1))
                                        {
                                            //是否在4G中包含，不包含则显示出来
                                            ExcelRowBean tg_3G_bean = new ExcelRowBean();
                                            tg_3G_bean.InformationType = "3G";
                                            tg_3G_bean.CNFilingn = original.CNFilingn;
                                            if (!cn_dictionary.Keys.Contains(tg_3G_bean))
                                                newCell.SetCellValue(original.CNFilingn);
                                        }
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 3)//2G 列
                                {
                                    if (original.CNFilingn != "" && cn_dictionary.Keys.Contains(original) && cn_dictionary[original] == false && (original.InformationType == "2G" || original.InformationType == "TG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1)))//表示第一次出现
                                    {
                                        if (original.InformationType == "2G")
                                        {
                                            newCell.SetCellValue(original.CNFilingn);
                                            cn_dictionary[original] = true;//表示已出现过
                                            copy_cn_filingn = original.CNFilingn;
                                        }
                                        else if (original.InformationType == "TG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1))
                                        {
                                            //是否在4G中包含，不包含则显示出来
                                            ExcelRowBean tg_2G_bean = new ExcelRowBean();
                                            tg_2G_bean.InformationType = "2G";
                                            tg_2G_bean.CNFilingn = original.CNFilingn;
                                            if (!cn_dictionary.Keys.Contains(tg_2G_bean))
                                                newCell.SetCellValue(original.CNFilingn);
                                        }
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 4)//DG 列
                                {
                                    if ((original.InformationType == "DG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") == -1)) && cn_dictionary.Keys.Contains(original) && original.CNFilingn != "" && cn_dictionary[original] == false)//表示第一次出现
                                    {
                                        ExcelRowBean de_4G_bean = new ExcelRowBean();
                                        de_4G_bean.InformationType = "4G";
                                        de_4G_bean.CNFilingn = original.CNFilingn;
                                        ExcelRowBean de_3G_bean = new ExcelRowBean();
                                        de_3G_bean.InformationType = "3G";
                                        de_3G_bean.CNFilingn = original.CNFilingn;
                                        if (cn_dictionary.Keys.Contains(de_3G_bean) || cn_dictionary.Keys.Contains(de_4G_bean))
                                        {
                                            newCell.SetCellValue("DG1");
                                            cn_dictionary[original] = true;//表示已出现过
                                        }
                                        else
                                        {
                                            newCell.SetCellValue("DG1");
                                            cn_dictionary[original] = true;//表示已出现过
                                            copy_cn_filingn = original.CNFilingn;
                                        }
                                    }
                                    if (original3GTo4G != null && Array.IndexOf(cn_dictionary.Keys.ToArray(), original3GTo4G) != -1)//3G数据转到4G的数据从字典中检索到
                                        newCell.SetCellValue("DG2");
                                }
                                else if (columnIndex == parseCellIndex + 5)//TG 列
                                {
                                    if (original.CNFilingn != "" && cn_dictionary.Keys.Contains(original) && cn_dictionary[original] == false && (original.InformationType == "TG" || (original.InformationType.IndexOf("4G") > -1 && original.InformationType.IndexOf("3G") > -1 && original.InformationType.IndexOf("2G") > -1)))//表示第一次出现OT
                                    {
                                        newCell.SetCellValue(original.CNFilingn);
                                        cn_dictionary[original] = true;//表示已出现过
                                        copy_cn_filingn = original.CNFilingn;
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 6)//OT 列
                                {
                                    if (original.CNFilingn != "" && cn_dictionary.Keys.Contains(original) && cn_dictionary[original] == false && original.InformationType == "OT")//表示第一次出现
                                    {
                                        newCell.SetCellValue(original.CNFilingn);
                                        cn_dictionary[original] = true;//表示已出现过
                                        copy_cn_filingn = original.CNFilingn;
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 7)//UK 列
                                {
                                    if (original.CNFilingn != "" && cn_dictionary.Keys.Contains(original) && cn_dictionary[original] == false && original.InformationType == "UK")//表示第一次出现UK
                                    {
                                        newCell.SetCellValue(original.CNFilingn);
                                        cn_dictionary[original] = true;//表示已出现过
                                        copy_cn_filingn = original.CNFilingn;
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 8)//重复 列
                                {
                                    if (copy_cn_filingn == "")
                                    {//如果检索到对象对应的值true代表该值已出现过
                                        if (original.InformationType == "")
                                            newCell.SetCellValue(this.lineConditions + "为空！");
                                        else if (original.CNFilingn == "")
                                            newCell.SetCellValue(this.parseName + "为空!");
                                        else
                                            newCell.SetCellValue(original.CNFilingn);
                                    }
                                }
                                else if (columnIndex == parseCellIndex + 9)//仅处理目标数去重
                                {
                                    if (parseDic.Keys.Contains(parse) && parseDic[parse] == false)
                                    {
                                        newCell.SetCellValue(parse);
                                        parseDic[parse] = true;
                                    }
                                }
                                else
                                {
                                    ICell oldCell = oldRow.GetCell(columnIndex <= parseCellIndex ? columnIndex : columnIndex - 9);
                                    if (oldCell != null)
                                    {
                                        if (columnIndex < parseCellIndex + 1)
                                            ExcelUtil.GetTdMergedInfo(oldSheet, rowIndex, columnIndex, out rowspan, out colspan);
                                        else if (columnIndex > parseCellIndex + 9)
                                            ExcelUtil.GetTdMergedInfo(oldSheet, rowIndex, columnIndex - 9, out rowspan, out colspan);
                                        newCell.SetCellValue(oldCell.ToString().Trim());
                                        if (rowspan > 1 || colspan > 1)
                                        {
                                            CellRangeAddress a = new CellRangeAddress(rowIndex, rowIndex + (rowspan - 1), columnIndex, columnIndex + (colspan - 1));
                                            newSheet.AddMergedRegion(a);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                newSheet.CreateFreezePane(0, this.lineNo, 0, this.lineNo + 1);//将标题行冻结
            }
            catch (Exception ex)
            {
                throw new Exception("read " + rowIndex + " error:" + ex.Message, ex);
            }
        }
    }
}
