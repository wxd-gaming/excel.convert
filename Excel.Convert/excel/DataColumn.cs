using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.excel
{
    public class DataColumn
    {

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 注解
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string ValueType { get; set; }
        /// <summary>
        /// 数据列最大长度
        /// </summary>
        public int MaxLen { get; set; }
        /// <summary>
        /// 主键
        /// </summary>        
        public bool Key { get; set; }


    }
}
