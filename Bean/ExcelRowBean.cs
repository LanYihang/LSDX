using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ls.Lsdx.Bean
{
    /// <summary>
    /// Excel表中的行数据说明
    /// </summary>
    public class ExcelRowBean
    {
        private string informationType;
        private string cn_filingn = "";
        /// <summary>
        /// 手机通信类别 4G  3G  2G OT
        /// </summary>
        public string InformationType { get { return informationType; }set { informationType = value; } }
        /// <summary>
        /// CN同族申请号
        /// </summary>
        public string CNFilingn { get { return cn_filingn; } set { cn_filingn = value; } }

        /// <summary>
        /// 重写比较代码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj==this)
                return true;
            if (!(obj is ExcelRowBean))
                return false;
            ExcelRowBean tmp = (ExcelRowBean)obj;
            return informationType == tmp.informationType && cn_filingn == tmp.cn_filingn;
        }
        /// <summary>
        /// 重写哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (informationType + cn_filingn).GetHashCode();
        }
    }
}
