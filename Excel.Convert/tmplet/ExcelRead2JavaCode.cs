using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.excel.entity
{
    public class ExcelRead2JavaCode : ICreateCode
    {

        public void CreateCode(string outPath, string package, DataTable dataTable)
        {
            StringBuilder append = new StringBuilder();

            append.Append("package ").Append(package).Append(";").AppendLine();
            append.AppendLine();
            append.Append("import com.cqd.batis.mapping.DbColumn;").AppendLine();
            append.Append("import com.cqd.batis.mapping.DbTable;").AppendLine();
            append.Append("import lombok.Getter;").AppendLine();
            append.Append("import lombok.Setter;").AppendLine();
            append.Append("import lombok.experimental.Accessors;").AppendLine();
            append.AppendLine();
            append.AppendLine();
            append.Append("").AppendLine();
            string code = append.ToString();

        }

        public void CreateCode_Bean(DataTable dataTable)
        {

        }

        public void CreateCode_Bean_Exent(DataTable dataTable)
        {

        }

        public void CreateCode_Bean_Factory(DataTable dataTable)
        {

        }

    }
}
