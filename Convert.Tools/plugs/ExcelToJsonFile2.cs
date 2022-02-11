﻿using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugs
{
    public class ExcelToJsonFile2 : IOutPutPlugs
    {

        string outPath = "out\\json";

        public PlugEnum plugEnum()
        {
            return PlugEnum.Excel;
        }

        public string PlugsName()
        {
            return "导出 Json File 老版本";
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
            List<ExcelDataTable> dataTables = files.AsDataTable("all", 0, 0, 0, 1, 2, false);
            if (dataTables.Count == 0)
            {
                return;
            }
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