using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Plugs
{
    public class ExcelToMysqlFile : IOutPutPlugs
    {

        public PlugEnum plugEnum()
        {
            return PlugEnum.Excel;
        }

        public string PlugsName()
        {
            return "导出 Mysql 文件";
        }

        public void DoAction(List<string> files)
        {

            string outPath = "out\\sql\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "\\";

            List<ExcelDataTable> dataTables = files.AsDataTable();

            CreateTable(outPath, dataTables);
            InsertSql(outPath, dataTables);

        }

        public void CreateTable(string outPath, List<ExcelDataTable> dataTables)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ExcelDataTable dataTable in dataTables)
            {
                string v = dataTable.AsDdl();
                string sqlName = outPath + "\\ddl\\" + dataTable.Name + ".sql";
                v.WriterFile(sqlName);
                builder.Append(v).AppendLine().AppendLine("go;");
            }
            outPath = outPath + "\\ddl.sql";
            builder.ToString().WriterFile(outPath);
        }

        public void InsertSql(string outPath, List<ExcelDataTable> dataTables)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ExcelDataTable dataTable in dataTables)
            {
                string v = dataTable.AsInsertSql();
                string sqlName = outPath + "\\data\\" + dataTable.Name + ".sql";
                v.WriterFile(sqlName);
                builder.Append(v).AppendLine().AppendLine("go;");
            }
            outPath = outPath + "\\data.sql";
            builder.ToString().WriterFile(outPath);
        }

    }
}
