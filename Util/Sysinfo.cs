using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ls.Lsdx.Util
{
    /// <summary>
    /// 基础类库
    /// </summary>
    public class Sysinfo
    {
        public const string NEW_EXCEL_PATH = @"D:\";
        /// <summary>
        /// 关键字信息字典集合
        /// </summary>
        public static Dictionary<string, BaseInfo> XLS_TYPE_DICTIONARA = new Dictionary<string, BaseInfo>();
        /// <summary>
        /// 关于加载基本数据字典文件名称
        /// </summary>
        public const string BASE_DICTIONARY_XML_PATH = @"base.xml";
    }
    /// <summary>
    /// 关于网络信号对应的标准
    /// </summary>
    public class BaseInfo
    {
        private List<string> standards = new List<string>();
        private List<string> projects = new List<string>();
        /// <summary>
        /// 空心/核心网涉及的标准
        /// </summary>
        public List<string> Standards { get { return standards; } }
        /// <summary>
        /// 主题
        /// </summary>
        public List<string> Projects { get { return projects; } }
    }
}
