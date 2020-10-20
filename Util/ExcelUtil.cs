using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.Util;
using System.Web;

namespace Com.Ls.Lsdx.Util
{
    /// <summary>
    /// 操作excel获得合并单元格信息
    /// </summary>
    public class ExcelUtil
    {
        /// <summary>
        /// 加载Excel文件中的表单对象
        /// </summary>
        /// <param name="excelPath">excel文件的全路径</param>
        /// <param name="sheetName">excel中的表单名称</param>
        /// <returns>获得待处理的表单对象</returns>
        public static ISheet GetExcelSheet(string excelPath,string sheetName)
        {
            try
            {
                using (FileStream stream = new FileStream(excelPath, FileMode.Open))
                {
                    IWorkbook workBook = null;
                    if (Path.GetExtension(excelPath) == ".xls")
                        workBook = new HSSFWorkbook(stream);
                    else if (Path.GetExtension(excelPath) == ".xlsx")
                        workBook = new XSSFWorkbook(stream);
                    return workBook.GetSheet(sheetName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获得处理列在表单中的位置
        /// </summary>
        /// <param name="sheet">excel的表单对象</param>
        /// <param name="headCount">标题列所包含的行数</param>
        /// <param name="columnNames">处理列的所有名称</param>
        /// <returns>返回列名称所对应的位置</returns>
        public static Dictionary<string, int> LoadColumnData(ISheet sheet,int headCount, params string[] columnNames)
        {
            try
            {
                Dictionary<string ,int> paramDictionary=new Dictionary<string,int>();
                int rowsCount = sheet.PhysicalNumberOfRows;
                for (int i = 0; i < headCount; i++) 
                {
                    int colsCount = sheet.GetRow(i).PhysicalNumberOfCells;
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < colsCount;j++ ) 
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null && Array.IndexOf(columnNames.ToArray(), cell.ToString().Trim()) > -1)
                            paramDictionary.Add(cell.ToString().Trim(), j);
                    }
                }
                return paramDictionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建Excel的表单对象
        /// </summary>
        /// <param name="sheetName">表单名称</param>
        /// <param name="fileType">Excel的文件后缀名</param>
        /// <returns>返回创建的表单对象</returns>
        public static IWorkbook CreateExcelWorkBook(string sheetName, string fileType)
        {
            try
            {
                IWorkbook workBook = null;
                if (fileType == ".xls")
                    workBook = new HSSFWorkbook();
                else if (fileType == ".xlsx")
                    workBook = new XSSFWorkbook();
                workBook.CreateSheet(sheetName);
                return workBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <param name="workBook">包含表单对象的Excel对象</param>
        /// <param name="destinationFilePath">Excel目标文件路径</param>
        public static void CreateExcelFile(IWorkbook workBook, string destinationFilePath)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write);
                MemoryStream memory_stream = new MemoryStream();
                workBook.Write(memory_stream);
                byte[] buf = memory_stream.ToArray();
                stream.Write(buf, 0, buf.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        /// <summary>
        /// 检索获得Excel表单中合并单元格信息
        /// </summary>
        /// <param name="sheet">Excel中的表单对象</param>
        /// <param name="rowIndex">单元格行位置</param>
        /// <param name="colIndex">单元格列位置</param>
        /// <param name="rowspan">单元格合并的行数</param>
        /// <param name="colspan">单元格合并的列数</param>
        public static void GetTdMergedInfo(ISheet sheet, int rowIndex, int colIndex, out int rowspan, out int colspan)
        {
            colspan = 1; rowspan = 1;
            CellRangeAddress region;
            try
            {
                int regionsCount = sheet.NumMergedRegions;
                for (int i = 0; i < regionsCount; i++)
                {
                    region = sheet.GetMergedRegion(i);
                    if (region.FirstRow == rowIndex && region.FirstColumn == colIndex)
                    {
                        colspan = region.LastColumn - region.FirstColumn + 1;
                        rowspan = region.LastRow - region.FirstRow + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="font">单元格字体</param>
        /// <param name="fillForegroundColor">图案的颜色</param>
        /// <param name="fillPattern">图案样式</param>
        /// <param name="fillBackgroundColor">单元格背景</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle(HSSFWorkbook hssfworkbook, IFont font, HSSFColor fillForegroundColor, FillPatternType fillPattern, HSSFColor fillBackgroundColor, HorizontalAlignment ha, VerticalAlignment va)
        {
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.FillPattern = fillPattern;
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;
            if (fillForegroundColor != null)
            {
                cellstyle.FillForegroundColor = fillForegroundColor.GetIndex();
            }
            if (fillBackgroundColor != null)
            {
                cellstyle.FillBackgroundColor = fillBackgroundColor.GetIndex();
            }
            if (font != null)
            {
                cellstyle.SetFont(font);
            }
            //有边框
            cellstyle.BorderBottom = BorderStyle.THIN;
            cellstyle.BorderLeft = BorderStyle.THIN;
            cellstyle.BorderRight = BorderStyle.THIN;
            cellstyle.BorderTop = BorderStyle.THIN;
            return cellstyle;
        }

        /// <summary>
        /// 提供下载至客户端
        /// </summary>
        /// <param name="file">下载文件FileInfo对象</param>
        public static void DownloadFile(FileInfo file)
        {
            HttpResponse Response=System.Web.HttpContext.Current.Response;
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            Response.AppendHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(file.FullName);
            Response.Flush(); Response.End();
        }
    }
}
}
