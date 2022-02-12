using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.Excel
{
    public class ExcelDataColumn
    {

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码名称
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 注解
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 归属，all ,server ,client 默认all
        /// </summary>
        public string BeLong { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string ValueType { get; set; }
        /// <summary>
        /// 数据列最大长度
        /// </summary>
        public string SqlType { get; set; }
        /// <summary>
        /// 主键
        /// </summary>        
        public bool Key { get; set; }

        public bool NoBeLong(string belong)
        {
            return !(string.IsNullOrWhiteSpace(belong)
                || string.IsNullOrWhiteSpace(this.BeLong)
                || "all".Equals(belong, StringComparison.OrdinalIgnoreCase)
                || "all".Equals(this.BeLong, StringComparison.OrdinalIgnoreCase)
                || this.BeLong.Equals(belong, StringComparison.OrdinalIgnoreCase));
        }

    }
}
