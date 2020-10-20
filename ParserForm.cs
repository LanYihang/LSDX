using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace LSDX
{
    public partial class ParserForm : Form
    {
        private String url = "https://portal.3gpp.org/desktopmodules/Specifications/SpecificationDetails.aspx?specificationId={0}";
        private int MAX_SPECIFICATIONID = 3495;
        private int NUMBER_REQUESTED = 0;//已请求个数
        private List<MessBean> messList = new List<MessBean>();
        private const String EXCEL_PATH = @"D:\answer.xls";
        private const String SHEET_NAME = "Sheet1";
        private const String BIN = "b.bin";
        private Thread thread = null;//操作请求的子线程
        public ParserForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void ParserForm_Load(object sender, EventArgs e)
        {
            NUMBER_REQUESTED = DeserializeCount();
            this.txtURL.Text = string.Format(url, NUMBER_REQUESTED);
            this.txtStartID.Text = NUMBER_REQUESTED.ToString();
            this.txtEndID.Text = MAX_SPECIFICATIONID.ToString();
        }

        //触发检索事件并开始检索
        private void btnStart_Click(object sender, EventArgs e)
        {
            //校验数字
            this.txtStartID.Text = this.txtStartID.Text.Trim();
            this.txtEndID.Text = this.txtEndID.Text.Trim();
            Regex regex = new Regex(@"^[0-9]*[1-9][0-9]*$");
            if (regex.IsMatch(this.txtStartID.Text) && regex.IsMatch(this.txtEndID.Text))
            {
                if (Convert.ToInt16(this.txtStartID.Text) > 0 && Convert.ToInt16(this.txtStartID.Text) < 3495 && Convert.ToInt16(this.txtEndID.Text) > 0 && Convert.ToInt16(this.txtEndID.Text) <= 3495 && Convert.ToInt16(this.txtStartID.Text) < Convert.ToInt16(this.txtEndID.Text))
                {
                    int tmp_a= Convert.ToInt16(this.txtStartID.Text);
                    MAX_SPECIFICATIONID = Convert.ToInt16(this.txtEndID.Text);
                    if (this.messList.Count > 0)
                        this.messList.Clear();
                    this.txtShow.Text = "";
                    //开始读取已存的数据
                    int tmp_b = DeserializeCount();
                    if (tmp_a > tmp_b)
                        NUMBER_REQUESTED = tmp_a;
                    else
                        NUMBER_REQUESTED = tmp_b;
                    this.btnPause.Enabled = true;
                    thread = new Thread(new ThreadStart(SearchURL));
                    thread.Start();
                }
            }
            else
                this.txtShow.AppendText("Illegal input data!\r\n");
        }
        /// <summary>
        /// 暂停是将已存在的数据生成Excel并将请求的ID数序列化，以便start的时候再继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (messList.Count > 0)
            {
                if (thread != null)
                {
                    thread.Abort();
                    thread = null;
                }
                this.txtShow.AppendText("Start generating excel file...\r\n");
                CreateExcel();
                this.txtShow.AppendText("Generate excel file successfully!Path:" + EXCEL_PATH + "\r\n");
            }
            SerializeCount();
        }
        /// <summary>
        /// 发送检索请求
        /// </summary>
        private void SearchURL()
        {
            for (int i = NUMBER_REQUESTED; i <= MAX_SPECIFICATIONID; i++)
                LoadHTML(string.Format(url, i), i);
            if (messList.Count > 0)
            {
                this.txtShow.AppendText("Start generating excel file...\r\n");
                CreateExcel();
                SerializeCount();
                this.txtShow.AppendText("Generate excel file successfully!Path:" + EXCEL_PATH + "\r\n");
            }
        }
        /// <summary>
        /// 加载页面信息，检索所需内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestID">请求的次数</param>
        private void LoadHTML(String url,int requestID)
        {
            try
            {
                this.txtURL.Text = url;
                this.txtShow.AppendText("request:" + url + "\r\n");
                WebClient client = new WebClient();
                client.Encoding = Encoding.Default;
                this.txtShow.AppendText("start parsing...\r\n");
                string html = client.DownloadString(url);
                Regex regex = new Regex(@"<span id=""lblHeaderText"">Specification #:[\s][\d]{2}.[\w]{2,3}[-]?[\d]*</span>");
                String specificationValue = regex.Match(html).Value;
                regex = new Regex(@"[\d]{2}.[\w]{2,3}[-]?[\d]*");
                String specificationId = regex.Match(specificationValue).Value;
                regex = new Regex(@"<span id=""typeVal"">Technical[\s][\w]{1,}[\s][(][T][RS][)]</span>");
                String typeValue = regex.Match(html).Value;
                regex = new Regex(@"[(][T][RS][)]");
                String type = regex.Match(typeValue).Value.Replace("(", "").Replace(")", "");
                regex = new Regex(@"<span id=""radioTechnologyVals""[\s\S]{1,}</span></span></td>");
                String radioTechnologySpan = regex.Match(html).Value;
                //循环寻找
                String radioTechnologyInput = radioTechnologySpan;//包含checked的Input
                String radioTechnology = "";
                do {
                    regex = new Regex(@"checked=""checked""[\s\S]+value=""[1-4]"" />");
                    radioTechnologyInput = regex.Match(radioTechnologyInput).Value;
                    if (radioTechnologyInput != "")
                    {
                        String radioTechnologyNumber = radioTechnologyInput.Substring(radioTechnologyInput.IndexOf("value"), 9).Substring(7, 1);
                        switch (radioTechnologyNumber)
                        {
                            case "1":
                                radioTechnology += "2G ";
                                radioTechnologyInput = radioTechnologyInput.Substring(18, radioTechnologyInput.Length - 18);
                                break;
                            case "2":
                                radioTechnology += "3G ";
                                radioTechnologyInput = radioTechnologyInput.Substring(18, radioTechnologyInput.Length - 18);
                                break;
                            case "3":
                                radioTechnology += "LTE ";
                                radioTechnologyInput = radioTechnologyInput.Substring(18, radioTechnologyInput.Length - 18);
                                break;
                            case "4":
                                radioTechnology += "5G ";
                                radioTechnologyInput = radioTechnologyInput.Substring(18, radioTechnologyInput.Length - 18);
                                break;
                        }
                    }
                } while (radioTechnologyInput!="");
                MessBean mess_bean = new MessBean();
                mess_bean.RequestID = requestID;
                mess_bean.Specification = specificationId;
                mess_bean.Type = type;
                mess_bean.RadioTechonology = radioTechnology;
                messList.Add(mess_bean);
                this.txtShow.AppendText("successful analysis!\r\n");
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("\r\n**************** Parser Html failed **************** \r\n");
                this.txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
                if (thread != null)//在线程存在的情况下是代码触发的异常，而pause则是强制终止线程
                {
                    //有异常就处理
                    if (messList.Count > 0)
                    {
                        this.txtShow.AppendText("Start generating excel file...\r\n");
                        CreateExcel();
                        this.txtShow.AppendText("Generate excel file successfully!Path:" + EXCEL_PATH + "\r\n");
                    }
                    SerializeCount();
                    //清空数据
                    messList.Clear();
                    //终止线程
                    this.txtShow.AppendText("Thread abort!Please click start\r\n");
                    thread.Abort();
                    thread = null;
                }
            }
        }
        /// <summary>
        /// 生成Excel文件
        /// </summary>
        private void CreateExcel()
        {
            try
            {
                IWorkbook workBook = new HSSFWorkbook();
                using (FileStream stream = new FileStream(EXCEL_PATH, FileMode.Create, FileAccess.Write))
                {
                    ISheet sheet = workBook.CreateSheet(SHEET_NAME);
                    for (int i = 0; i <= messList.Count; i++)
                    {
                        IRow row = sheet.CreateRow(i);
                        for (int j = 0; j < 4; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            cell.SetCellType(CellType.STRING);
                            sheet.SetColumnWidth(j, 20 * 256);
                            switch (j)
                            {
                                case 0:
                                    if (i == 0)
                                        cell.SetCellValue("RequestID");
                                    else
                                        cell.SetCellValue(messList[i-1].RequestID);
                                    break;
                                case 1:
                                    if (i == 0)
                                        cell.SetCellValue("Specification");
                                    else
                                        cell.SetCellValue(messList[i - 1].Specification);
                                    break;
                                case 2:
                                    if (i == 0)
                                        cell.SetCellValue("Type");
                                    else
                                        cell.SetCellValue(messList[i - 1].Type);
                                    break;
                                case 3:
                                    if (i == 0)
                                        cell.SetCellValue("Radio technology");
                                    else
                                        cell.SetCellValue(messList[i - 1].RadioTechonology);
                                    break;
                            }
                        }
                    }
                    MemoryStream memory = new MemoryStream();
                    workBook.Write(memory);
                    byte[] buffer = memory.ToArray();
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("\r\n**************** Generate excel failed **************** \r\n");
                this.txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        /// <summary>
        /// 将已请求数序列化至bin文件中
        /// </summary>
        private void SerializeCount()
        {
            try
            {
                NUMBER_REQUESTED += messList.Count;
                using (FileStream stream = new FileStream(BIN, FileMode.Create,FileAccess.Write))
                {
                    byte[] buffer = Encoding.Default.GetBytes(NUMBER_REQUESTED.ToString());
                    stream.Write(buffer,0,buffer.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("\r\n**************** Serialize data failed **************** \r\n");
                this.txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
            }
        }
        /// <summary>
        /// 获取已存的序列化请求数
        /// </summary>
        /// <returns></returns>
        private int DeserializeCount()
        {
            try
            {
                if (File.Exists(BIN))
                {
                    using (FileStream stream = new FileStream(BIN, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[1];
                        stream.Read(buffer, 0, buffer.Length);
                        return Convert.ToInt16(Encoding.Default.GetString(buffer));
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("\r\n**************** Deserialize data failed **************** \r\n");
                this.txtShow.AppendText(ex.Message + ", with stack trace: \r\n" + ex.StackTrace + "\r\n");
                return 1;
            }
        }
    }

    class MessBean
    {
        private int requestID = 0;
        private String specification = "";
        private String type = "";
        private String radioTechonology = "";

        public int RequestID { get; set; }
        public String Specification { get; set; }
        public String Type { get; set; }
        public String RadioTechonology { get; set; }
    }
}
