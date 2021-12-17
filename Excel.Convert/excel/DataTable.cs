using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.excel
{
    public class DataTable
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
        public DataColumn KeyColumn { get; set; }

        /// <summary>
        /// 所有的字段
        /// </summary>
        public Dictionary<string, DataColumn> Columns = new Dictionary<string, DataColumn>();
        /// <summary>
        /// 所有行
        /// </summary>
        public List<Dictionary<string, object>> Rows = new List<Dictionary<string, object>>();

    }
}
