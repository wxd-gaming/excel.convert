using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugs
{
    public class OutPutToJsonFile : IOutPutPlugs
    {

        string outPath = "d:\\out\\json";

        public PlugEnum plugEnum()
        {
            return PlugEnum.Excel;
        }

        public string PlugsName()
        {
            return "导出 Json File";
        }
        public void DoAction(List<string> files)
        {
            List<ExcelDataTable> dataTables = files.AsDataTable();
            foreach (ExcelDataTable dataTable in dataTables)
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
}
