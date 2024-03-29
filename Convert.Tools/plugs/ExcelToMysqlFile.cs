﻿using Excel.Convert;
using Excel.Convert.Code;
using Excel.Convert.Excel;
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
            List<ExcelDataTable> dataTables = files.AsDataTable("all", 2, 3, 1, 4, 5, true);
            if (dataTables.Count == 0)
            {
                return;
            }
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
                builder.Append(v).AppendLine().AppendLine();
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
                builder.Append(v).AppendLine().AppendLine();
            }
            outPath = outPath + "\\data.sql";
            builder.ToString().WriterFile(outPath);
        }

    }
}
