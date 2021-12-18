using Excel.Convert.excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.Plugs
{
    public class OutPut2JsonFile : IOutPutPlugs
    {

        public string PlugsName()
        {
            return "Out Json File";
        }

        string outPath = "d:\\out\\json";

        public void OutPut(DataTable dataTable)
        {
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
