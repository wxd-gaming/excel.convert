using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugs
{
    public class ExcelToJavaCode : IOutPutPlugs
    {

        string outPath = "out\\code";
        string package = "com.tb.po";

        public PlugEnum plugEnum()
        {
            return PlugEnum.Excel;
        }

        public string PlugsName()
        {
            return "导出 Java Code";
        }

        public void DoAction(List<string> files)
        {
            /*
             files 文件路径
             读取字段归属    all 表示全部 no 表示不需要，其他字符任意自定义
            字段列名所在行号
            字段类型所在行号
            字段归属权所在行号
            字段备注信息所在行号
            字段数据起始行号
            是否读取所有的 sheet 标签页
             */
            List<ExcelDataTable> dataTables = files.AsDataTable("all", 2, 3, 1, 4, 5, false);
            if (dataTables.Count == 0)
            {
                return;
            }
            foreach (ExcelDataTable dataTable in dataTables)
            {
                CreateCode_Bean(dataTable);
                CreateCode_Exent(dataTable);
                CreateCode_Factory(dataTable);
            }

        }

        public void CreateCode_Bean(ExcelDataTable dataTable)
        {
            string fileName = outPath + "\\" + package.Replace(".", "\\") + "\\bean\\" + dataTable.CodeName + "Bean.java";

            StringBuilder append = new StringBuilder();

            append.Append("package ").Append(package).Append(".bean;").AppendLine();
            append.AppendLine();
            append.AppendLine();
            append.Append("import com.tb.batis.struct.DbColumn;").AppendLine();
            append.Append("import com.tb.batis.struct.DbTable;").AppendLine();
            append.Append(" import com.tb.batis.struct.DataBaseModel;").AppendLine();
            append.Append("import lombok.Getter;").AppendLine();
            append.Append("import lombok.Setter;").AppendLine();
            append.Append("import lombok.experimental.Accessors;").AppendLine();
            append.AppendLine();
            append.Append("/**").AppendLine();
            append.Append(" * ").Append(dataTable.Comment).AppendLine();
            append.Append(" */").AppendLine();
            append.Append("@Getter").AppendLine();
            append.Append("@Setter").AppendLine();
            append.Append("@Accessors(chain = true)").AppendLine();
            append.Append("@DbTable(mappedSuperclass = true, name = \"").Append(dataTable.Name).Append("\")").AppendLine();
            append.Append("public abstract class " + dataTable.CodeName + "Bean extends DataBaseModel {").AppendLine();
            append.AppendLine();
            foreach (var item in dataTable.Columns.Values)
            {
                append.Append("    /**").AppendLine();
                append.Append("     *").Append(item.Comment).AppendLine();
                append.Append("     */").AppendLine();
                append.Append("    @DbColumn(name = \"" + item.Name + "\"");
                if (item.Key)
                {
                    append.Append(", key = true");
                }
                append.Append(")").AppendLine();
                append.Append("    privte ").Append(item.ValueType).Append(" ").Append(item.CodeName).Append(";").AppendLine();

            }
            append.AppendLine();
            append.Append("}").AppendLine();
            string code = append.ToString();

            code.WriterFile(fileName);
        }

        public void CreateCode_Exent(ExcelDataTable dataTable)
        {
            string fileName = outPath + "\\" + package.Replace(".", "\\") + "\\extend\\" + dataTable.CodeName + "Extend.java";
            if (fileName.ExistsFile())
            {
                return;
            }

            StringBuilder append = new StringBuilder();

            append.Append("package ").Append(package).Append(".extend;").AppendLine();
            append.AppendLine();
            append.AppendLine();
            append.Append("import com.tb.batis.struct.DbColumn;").AppendLine();
            append.Append("import com.tb.batis.struct.DbTable;").AppendLine();
            append.Append(" import com.tb.batis.struct.DataBaseModel;").AppendLine();
            append.Append("import lombok.Getter;").AppendLine();
            append.Append("import lombok.Setter;").AppendLine();
            append.Append("import lombok.experimental.Accessors;").AppendLine();
            append.AppendLine();
            append.Append("/**").AppendLine();
            append.Append(" * ").Append(dataTable.Comment).AppendLine();
            append.Append(" */").AppendLine();
            append.Append("@Getter").AppendLine();
            append.Append("@Setter").AppendLine();
            append.Append("@Accessors(chain = true)").AppendLine();
            append.Append("@DbTable(name = \"").Append(dataTable.Name).Append("\")").AppendLine();
            append.Append("public class " + dataTable.CodeName + "Extend extends ").Append(dataTable.CodeName).Append("Bean {").AppendLine();
            append.AppendLine();
            append.AppendLine();
            append.Append("}").AppendLine();
            string code = append.ToString();

            code.WriterFile(fileName);

        }

        public void CreateCode_Factory(ExcelDataTable dataTable)
        {
            string fileName = outPath + "\\" + package.Replace(".", "\\") + "\\factory\\" + dataTable.CodeName + "Factory.java";

            if (fileName.ExistsFile())
            {
                return;
            }

            StringBuilder append = new StringBuilder();

            append.Append("package ").Append(package).Append(".factory;").AppendLine();
            append.AppendLine();
            append.AppendLine();
            append.Append("import com.tb.batis.struct.DbColumn;").AppendLine();
            append.Append("import com.tb.batis.struct.DbTable;").AppendLine();
            append.Append(" import com.tb.batis.struct.DataBaseModel;").AppendLine();
            append.Append("import lombok.Getter;").AppendLine();
            append.Append("import lombok.Setter;").AppendLine();
            append.Append("import lombok.experimental.Accessors;").AppendLine();
            append.AppendLine();
            append.Append("/**").AppendLine();
            append.Append(" * ").Append(dataTable.Comment).AppendLine();
            append.Append(" */").AppendLine();
            append.Append("@Getter").AppendLine();
            append.Append("@Setter").AppendLine();
            append.Append("@Accessors(chain = true)").AppendLine();
            append.Append("@DbTable(name = \"").Append(dataTable.Name).Append("\")").AppendLine();
            append.Append("public class " + dataTable.CodeName + "Factory ").Append(" {").AppendLine();
            append.AppendLine();
            append.AppendLine();
            append.Append("}").AppendLine();
            string code = append.ToString();

            code.WriterFile(fileName);
        }

    }
}
