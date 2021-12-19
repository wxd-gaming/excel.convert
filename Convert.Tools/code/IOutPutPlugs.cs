using Convert.Tools.code;
using Convert.Tools.excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert.Tools
{
    public interface IOutPutPlugs
    {

        PlugEnum plugEnum();

        string PlugsName();

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void OutPut(object data);

    }
}
