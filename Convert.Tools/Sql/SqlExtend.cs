using Convert.Tools;
using Convert.Tools.Excel;
using Convert.Tools.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SqlExtend
{

    public static List<ExcelDataTable> AsDataTable(this List<string> files)
    {
        return AsDataTable(files, 0, 0, 0, 1, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="files"></param>
    /// <param name="nameRowNumber"></param>
    /// <param name="typeRowNumber"></param>
    /// <param name="belongRowNumber">归属行号</param>
    /// <param name="commentRowNumber">备注行号</param>
    /// <param name="dataStartComment">数据起始行号</param>
    /// <returns></returns>
    public static List<ExcelDataTable> AsDataTable(this List<string> files, int nameRowNumber, int typeRowNumber,
        int belongRowNumber, int commentRowNumber, int dataStartComment
        )
    {
        ExcelRead excelRead = new ExcelRead();
        excelRead.NameRowNumber = nameRowNumber;
        excelRead.TypeRowNumber = typeRowNumber;
        excelRead.BelongRowNumber = belongRowNumber;
        excelRead.CommentRowNumber = commentRowNumber;
        excelRead.DataStartRowNumber = dataStartComment;
        foreach (string file in files)
        {
            excelRead.ReadExcel(file);
        }
        List<ExcelDataTable> list = new List<ExcelDataTable>();
        foreach (var item in excelRead.Tables.Values)
        {
            list.Add(item);
        }
        return list;
    }

    public static string AsDdl(this ExcelDataTable dataTable)
    {
        StringBuilder builder = new StringBuilder();
        AsDdl(dataTable, builder);
        return builder.ToString();
    }

    public static void AsDdl(this ExcelDataTable dataTable, StringBuilder builder)
    {
        builder.AppendLine("drop table if exists " + dataTable.Name + ";");
        builder.AppendLine("CREATE TABLE " + dataTable.Name + "(");
        foreach (var item in dataTable.Columns)
        {
            ExcelDataColumn column = item.Value;
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

    public static void InsertMysql(this ExcelDataTable dataTable)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("Replace Into `" + dataTable.Name + "` (");
        bool appendDH = false;
        foreach (var item in dataTable.Columns)
        {
            if (appendDH) sql.Append(", ");
            ExcelDataColumn column = item.Value;
            sql.Append("`").Append(column.Name).Append("`");
        }

        sql.AppendLine(")");
        sql.AppendLine("values");
        foreach (var row in dataTable.Rows)
        {
            sql.Append("(");
            appendDH = false;
            foreach (var column in dataTable.Columns)
            {
                if (appendDH) sql.Append(", ");
                sql.Append("`").Append(row[column.Value.Name]).Append("`");
            }
            sql.AppendLine(")");
        }

    }

}