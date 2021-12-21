using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using Convert.Tools.Sql;
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

            string ddlPath = "out\\sql\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "\\";

            List<ExcelDataTable> dataTables = files.AsDataTable();

            CreateTable(ddlPath, dataTables);


        }

        public void CreateTable(string ddlPath, List<ExcelDataTable> dataTables)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ExcelDataTable dataTable in dataTables)
            {
                string v = dataTable.AsDdl();
                string sqlName = ddlPath + "\\ddl\\" + dataTable.Name + ".sql";
                v.WriterFile(sqlName);
                builder.Append(v).AppendLine();
            }
            ddlPath = ddlPath + "\\ddl.sql";
            builder.ToString().WriterFile(ddlPath);
        }

    }
}
