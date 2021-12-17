using Excel.Convert.excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert
{
    public interface ICreateCode
    {

        /// <summary>
        /// 创建 映射 entity
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        void CreateCode(string outPath, string package, DataTable dataTable);

    }
}
