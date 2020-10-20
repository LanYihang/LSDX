using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ls.Lsdx.Bean
{
    /// <summary>
    /// Excel表中的行数据说明
    /// </summary>
    class ExcelOtherRowBean
    {
        private string informationType;
        private string technologyType;
        private string cn_filingn = "";
        private int line_num = 0;
        /// <summary>
        /// 手机通信类别 4G  3G  2G OT
        /// </summary>
        public string InformationType { get { return informationType; } set { informationType = value; } }
        /// <summary>
        /// 技术分类  核心网  空口  OTHERS
        /// </summary>
        public string TechnologyType { get { return technologyType; } set { technologyType = value; } }
        /// <summary>
        /// CN同族申请号
        /// </summary>
        public string CNFilingn { get { return cn_filingn; } set { cn_filingn = value; } }
        /// <summary>
        /// 数据出现的行号
        /// </summary>
        public int LineNum { get { return line_num; } set { line_num = value; } }

        /// <summary>
        /// 重写比较代码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj != null && obj is ExcelOtherRowBean) 
            {
                ExcelOtherRowBean other_bean = obj as ExcelOtherRowBean;
                if (other_bean.InformationType.Equals(informationType) && other_bean.TechnologyType.Equals(technologyType) && other_bean.CNFilingn.Equals(cn_filingn))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 重写哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (informationType + technologyType + cn_filingn).GetHashCode();
        }
    }
}
