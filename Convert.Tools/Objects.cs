﻿using Excel.Convert.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excel.Convert
{
    public static class Objects
    {

        public static List<ExcelDataTable> AsDataTable(this List<string> files)
        {
            return AsDataTable(files, "all");
        }

        public static List<ExcelDataTable> AsDataTable(this List<string> files, string checkBelong)
        {
            return AsDataTable(files, checkBelong, 0, 0, 0, 1, 2, false);
        }

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
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
        public static List<ExcelDataTable> AsDataTable(this List<string> files, string checkBelong,
            int nameRowNumber, int typeRowNumber,
            int belongRowNumber, int commentRowNumber,
            int dataStartComment, bool readAllSheet
            )
        {
            ExcelRead excelRead = new ExcelRead();
            excelRead.NameRowNumber = nameRowNumber;
            excelRead.TypeRowNumber = typeRowNumber;
            excelRead.BelongRowNumber = belongRowNumber;
            excelRead.CommentRowNumber = commentRowNumber;
            excelRead.DataStartRowNumber = dataStartComment;
            excelRead.ReadAllSheet = readAllSheet;
            excelRead.Belong = checkBelong;
            foreach (string file in files)
            {
                FormMain.ShowLog("开始解析文件：" + file);
                excelRead.ReadExcel(file);
            }
            List<ExcelDataTable> list = new List<ExcelDataTable>();
            foreach (var item in excelRead.Tables.Values)
            {
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String CodeString(this String str)
        {
            string code = "";
            string[] vs = str.Replace("=", "_").Replace("-", "_").Split('_');
            foreach (string v in vs)
            {
                if (code.Length < 1)
                    code += FirstLower(v);
                else
                    code += FirstUpper(v);
            }
            return code;
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String FirstLower(this String str)
        {
            if (str == null || str.Length < 1)
            {
                return str;
            }
            char[] chars = str.ToCharArray();
            chars[0] = Char.ToLower(chars[0]);
            str = new String(chars);
            return str;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String FirstUpper(this String str)
        {
            if (str == null || str.Length < 1)
            {
                return str;
            }
            char[] chars = str.ToCharArray();
            chars[0] = Char.ToUpper(chars[0]);
            str = new String(chars);
            return str;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool ExistsFile(this string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        /// <summary>
        /// 创建 文件
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        public static void WriterFile(this string condent, string filePath)
        {
            string dirPath = System.IO.Path.GetDirectoryName(filePath);
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(condent);
                FormMain.ShowLog("生成：" + Path.GetFullPath(filePath));
            }
        }

        static string LogFileName = "out\\out.log";
        public static void WriterLog(this string condent)
        {
            string dirPath = System.IO.Path.GetDirectoryName(LogFileName);
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogFileName, true, new UTF8Encoding(false)))
            {
                sw.WriteLine(condent);
            }
        }

    }
}
