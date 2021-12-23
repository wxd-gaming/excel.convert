using Convert.Tools;
using Convert.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Text;


public static class SqlExtend
{

    public static string AsDdl(this ExcelDataTable dataTable)
    {
        StringBuilder builder = new StringBuilder();
        return builder.ToString();
    }

    public static void AsDdl(this ExcelDataTable dataTable, StringBuilder builder)
    {
        builder.AppendLine("drop table if exists " + dataTable.Name + ";");
        builder.AppendLine("CREATE TABLE " + dataTable.Name + "(");
        foreach (var column in dataTable.Columns.Values)
        {
            builder.Append(column.Name).Append(" ");
            builder.Append(column.SqlType);
            if (column.Key)
            {
                builder.Append(" NOT NULL");
            }
            builder.Append(" COMMENT '").Append(column.Comment).Append("'");
            builder.Append(",").AppendLine();
        }
        builder.Append("  PRIMARY KEY (`" + dataTable.KeyColumn.Name + "`)").AppendLine();
        builder.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT COMMENT ='").Append(dataTable.Comment).Append("';");
    }

    public static string AsInsertSql(this ExcelDataTable dataTable)
    {
        StringBuilder builder = new StringBuilder();
        AsInsertSql(dataTable, builder);
        return builder.ToString();
    }

    public static void AsInsertSql(this ExcelDataTable dataTable, StringBuilder sql)
    {
        sql.Append("Replace Into `" + dataTable.Name + "` (");
        bool appendDH = false;
        foreach (var column in dataTable.Columns.Values)
        {
            if (appendDH) sql.Append(", ");
            sql.Append("`").Append(column.Name).Append("`");
            appendDH = true;
        }

        sql.AppendLine(")");
        sql.AppendLine("values");
        bool append1 = false;
        foreach (var row in dataTable.Rows)
        {
            if (append1) sql.AppendLine(", ");
            sql.Append("(");
            appendDH = false;
            foreach (var column in dataTable.Columns.Values)
            {
                if (appendDH) sql.Append(", ");
                if ("string".Equals(column.ValueType))
                {
                    sql.Append("'").Append(row[column.Name]).Append("'");
                }
                else
                {
                    sql.Append(row[column.Name]);
                }
                appendDH = true;
            }
            sql.Append(")");
            append1 = true;
        }
        sql.Append(";");
    }

}