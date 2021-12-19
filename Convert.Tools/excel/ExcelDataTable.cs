using System;
using System.Collections.Generic;

namespace Convert.Tools.Excel
{
    public class ExcelDataTable
    {

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码名称
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExcelDataColumn KeyColumn { get; set; }

        /// <summary>
        /// 所有的字段
        /// </summary>
        public Dictionary<string, ExcelDataColumn> Columns = new Dictionary<string, ExcelDataColumn>();
        /// <summary>
        /// 所有行
        /// </summary>
        public List<Dictionary<string, object>> Rows = new List<Dictionary<string, object>>();

    }
}
