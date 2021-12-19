using Convert.Tools.code;
using Convert.Tools.excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert.Tools.Plugs
{
    public class OutPut2JsonFile : IOutPutPlugs
    {

        string outPath = "d:\\out\\json";

        public PlugEnum plugEnum()
        {
            return PlugEnum.Excel;
        }

        public string PlugsName()
        {
            return "Out Json File";
        }
        public void OutPut(object data)
        {
            DataTable dataTable = data as DataTable;
            string fileName = outPath + "\\" + dataTable.Name + ".json";
            StringBuilder sb = new StringBuilder();
            List<Dictionary<string, object>> rows = dataTable.Rows;
            foreach (var row in rows)
            {
                string v = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                sb.Append(v).AppendLine();
            }
            sb.ToString().WriterFile(fileName);
        }

   
    }
}
