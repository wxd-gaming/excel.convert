using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert.Tools.excel
{
    public class ExcelRead
    {

        /// <summary>
        /// 所有的表
        /// </summary>
        public Dictionary<string, DataTable> Tables = new Dictionary<string, DataTable>();

        public void ReadExcel(string excelPath)
        {
            string fileName = Path.GetFileName(excelPath);
            try
            {
                string extend = Path.GetExtension(excelPath);

                using (FileStream fs = File.OpenRead(excelPath))   //打开myxls.xls文件
                {
                    IWorkbook wk;
                    if (".xlsx".Equals(extend, StringComparison.OrdinalIgnoreCase))
                    {
                        //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                        wk = new XSSFWorkbook(fs);
                    }
                    else if (".xls".Equals(extend, StringComparison.OrdinalIgnoreCase))
                    {
                        wk = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        wk = null;
                    }
                    if (wk.NumberOfSheets > 0)
                    {
                        for (int i = 0; i < wk.NumberOfSheets; i++)
                        {
                            ISheet sheet = wk.GetSheetAt(i);
                            ActionColumn(excelPath, sheet);
                            ActionData(excelPath, sheet);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                FormMain.ShowLog.Invoke("excel文件并且处于关闭状态：" + fileName);
                Console.WriteLine(e);
            }
        }

        private void ActionColumn(string excelPath, ISheet sheet)
        {
            //这里是记录行
            int lastRowNum = sheet.LastRowNum;

            if (lastRowNum < 2)
            {
                return;
            }

            IRow columnRow = sheet.GetRow(0);//字段名字行
            IRow typeRow = sheet.GetRow(0);//字段类型行
            IRow belongRow = sheet.GetRow(0);//字段归属
            IRow commentRow = sheet.GetRow(1);//读取配置说明

            if (columnRow == null) return;

            short lastCellNum = columnRow.LastCellNum;
            if (lastCellNum < 0)
            {
                return;
            }

            string sheetName = Convert(sheet.SheetName);

            if (sheetName.StartsWith("sheet", StringComparison.OrdinalIgnoreCase))
            {
                FormMain.ShowLog(excelPath + " 忽律 " + sheetName);
                return;
            }

            if (!Tables.ContainsKey(sheetName))
            {
                Tables[sheetName] = new DataTable();
            }

            DataTable dataTable = Tables[sheetName];
            dataTable.Name = sheetName;
            dataTable.CodeName = sheetName.CodeString().FirstUpper();

            HashSet<string> columnNames = new HashSet<string>();
            for (int k = 0; k < lastCellNum; k++)
            {
                ICell columnCell = columnRow.GetCell(k);
                ICell typeCell = typeRow.GetCell(k);
                ICell belongCell = belongRow.GetCell(k);
                ICell commontCell = commentRow.GetCell(k);
                ActionColumn(excelPath, sheetName, dataTable, columnNames, columnCell, columnCell, columnCell, commontCell);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="dataTable"></param>
        /// <param name="columnNames"></param>
        /// <param name="columnCell">列名</param>
        /// <param name="typeCell">类型 int float </param>
        /// <param name="belongCell">归属-client,server ac as all</param>
        /// <param name="commontCell">注解</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private DataColumn ActionColumn(string filePath, string sheetName,
            DataTable dataTable,
            HashSet<string> columnNames,
            ICell columnCell, ICell typeCell, ICell belongCell, ICell commontCell)
        {
            string belong = CellValue(belongCell);
            string belongCase = null;
            if (!string.IsNullOrWhiteSpace(belong))
            {
                belongCase = belong.ToString().ToLower();
                if ("no".Equals(belongCase) || belongCase.IndexOf("=hl") >= 0)
                {
                    Console.WriteLine("忽律字段");
                    return null;
                }
            }
            string v = CellValue(columnCell);
            if (v == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(v))
            {
                return null;
            }

            string columnName = v.Split('=')[0];

            if (columnNames != null && !columnNames.Add(columnName))
            {
                throw new Exception("文件：" + filePath + " 配置sheet " + sheetName + " 存在相同的字段 " + columnName);
            }

            if (!dataTable.Columns.ContainsKey(columnName))
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.Name = columnName;
                dataColumn.CodeName = columnName.CodeString();

                dataColumn.BeLong = "all";

                if (!string.IsNullOrWhiteSpace(belongCase))
                {

                    if ("client".Equals(belongCase) || belongCase.IndexOf("=ac") >= 0)
                    {
                        dataColumn.BeLong = "client";
                    }
                    else if ("server".Equals(belongCase) || belongCase.IndexOf("=as") >= 0)
                    {
                        dataColumn.BeLong = "server";
                    }
                }

                string vtype = typeCell.ToString().ToLower();
                if ("bool".Equals(vtype) || vtype.IndexOf("=bl") >= 0)
                {
                    dataColumn.ValueType = "bool";
                    dataColumn.SqlType = "tinyint(1)";
                }
                else if ("byte".Equals(vtype) || vtype.IndexOf("=b") >= 0)
                {
                    dataColumn.ValueType = "byte";
                    dataColumn.SqlType = "tinyint(1)";
                }
                else if ("short".Equals(vtype) || vtype.IndexOf("=st") >= 0)
                {
                    dataColumn.ValueType = "short";
                    dataColumn.SqlType = "tinyint(2)";
                }
                else if ("int".Equals(vtype) || vtype.IndexOf("=i") >= 0)
                {
                    dataColumn.ValueType = "int";
                    dataColumn.SqlType = "int";
                }
                else if ("long".Equals(vtype) || vtype.IndexOf("=l") >= 0)
                {
                    dataColumn.ValueType = "long";
                    dataColumn.SqlType = "bigint";
                }
                else if ("float".Equals(vtype) || vtype.IndexOf("=f") >= 0)
                {
                    dataColumn.ValueType = "float";
                    dataColumn.SqlType = "float";
                }
                else if ("double".Equals(vtype) || vtype.IndexOf("=d") >= 0)
                {
                    dataColumn.ValueType = "double";
                    dataColumn.SqlType = "double";
                }
                else if ("int[]".Equals(vtype) || vtype.IndexOf("array") >= 0)
                {
                    dataColumn.ValueType = "int[]";
                    dataColumn.SqlType = "text";
                }
                else if ("int[][]".Equals(vtype) || vtype.IndexOf("list") >= 0)
                {
                    dataColumn.ValueType = "int[][]";
                    dataColumn.SqlType = "text";
                }
                else if ("long[]".Equals(vtype))
                {
                    dataColumn.ValueType = "long[]";
                    dataColumn.SqlType = "text";
                }
                else if ("long[][]".Equals(vtype))
                {
                    dataColumn.ValueType = "long[][]";
                    dataColumn.SqlType = "text";
                }
                else if ("string".Equals(vtype) || vtype.IndexOf("=s") >= 0)
                {
                    dataColumn.ValueType = "string";
                    int len = 100;
                    string[] vs = vtype.Split('=');
                    foreach (var item in vs)
                    {
                        if (item.IndexOf("s_") >= 0 || item.IndexOf("string_") >= 0)
                        {
                            string[] vs1 = item.Split('_');
                            try
                            {
                                len = int.Parse(vs1[1]);
                            }
                            catch (Exception e) { Console.WriteLine(e); }
                        }
                    }

                    if (len < 1000)
                    {
                        dataColumn.SqlType = "varchar(" + len + ")";
                    }
                    else if (len < 50000)
                    {
                        dataColumn.SqlType = "text";
                    }
                    else
                    {
                        dataColumn.SqlType = "longtext";
                    }
                }

                object noiteValue = CellValue(commontCell);
                dataColumn.Comment = noiteValue == null ? "" : noiteValue.ToString();
                if (string.IsNullOrWhiteSpace(dataColumn.Comment))
                {
                    dataColumn.Comment = "";
                }
                dataTable.Columns[columnName] = dataColumn;
            }
            return dataTable.Columns[columnName];
        }

        /// <summary>
        /// 读取数据行
        /// </summary>
        /// <param name="sheet"></param>
        private void ActionData(string excelPath, ISheet sheet)
        {
            string sheetName = Convert(sheet.SheetName);
            if (!Tables.ContainsKey(sheetName))
            {
                return;

            }
            DataTable dataTable = Tables[sheetName];
            IRow columnRow = sheet.GetRow(0);

            if (columnRow == null)
            {
                return;
            }

            IRow typeRow = sheet.GetRow(0);
            IRow belongRow = sheet.GetRow(0);
            IRow commentRow = sheet.GetRow(1);

            if (sheet.LastRowNum > 2)
            {
                for (int i = 2; i <= sheet.LastRowNum; i++)
                {
                    IRow rowData = sheet.GetRow(i); //读取当前行数据
                    if (rowData == null)
                    {
                        return;
                    }
                    // 处理如果该行为空值那么忽略
                    for (int k = 0; k < columnRow.LastCellNum; k++)
                    {
                        ICell cell2 = rowData.GetCell(k);  //当前表格
                        if (cell2 != null && !string.IsNullOrWhiteSpace(cell2.ToString()))
                        {
                            goto lab_001;
                        }
                    }
                    /*文件末尾行*/
                    return;
                lab_001:
                    Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                    for (int k = 0; k < columnRow.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                    {
                        ICell columnCell = columnRow.GetCell(k);
                        ICell typeCell = typeRow.GetCell(k);
                        ICell belongCell = belongRow.GetCell(k);
                        ICell commontCell = commentRow.GetCell(k);
                        ICell dataCell = rowData.GetCell(k);

                        if (columnCell != null)
                        {
                            DataColumn dataColumn = ActionColumn(excelPath, sheetName, dataTable, null, columnCell, typeCell, belongCell, commontCell);
                            if (dataColumn != null)
                            {
                                keyValuePairs[dataColumn.Name] = CellValue(dataColumn.ValueType, dataCell);
                            }
                        }
                    }
                    dataTable.Rows.Add(keyValuePairs);
                }
            }
        }

        private string CellValue(ICell item)
        {
            if (item == null)
            {
                return string.Empty;
            }
            switch (item.CellType)
            {
                case CellType.Boolean:
                    return item.BooleanCellValue.ToString();

                case CellType.Error:
                    return ErrorEval.GetText(item.ErrorCellValue);

                case CellType.Formula:
                    switch (item.CachedFormulaResultType)
                    {
                        case CellType.Boolean:
                            return item.BooleanCellValue.ToString();

                        case CellType.Error:
                            return ErrorEval.GetText(item.ErrorCellValue);

                        case CellType.Numeric:
                            if (DateUtil.IsCellDateFormatted(item))
                            {
                                return item.DateCellValue.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                return item.NumericCellValue.ToString();
                            }
                        case CellType.String:
                            string str = item.StringCellValue;
                            if (!string.IsNullOrEmpty(str))
                            {
                                return str.ToString();
                            }
                            else
                            {
                                return string.Empty;
                            }
                        case CellType.Unknown:
                        case CellType.Blank:
                        default:
                            return string.Empty;
                    }
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(item))
                    {
                        return item.DateCellValue.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        return item.NumericCellValue.ToString();
                    }
                case CellType.String:
                    string strValue = item.StringCellValue;
                    return strValue.ToString().Trim();

                case CellType.Unknown:
                case CellType.Blank:
                default:
                    return string.Empty;
            }
        }

        private object CellValue(string type, ICell cell)
        {
            string v = CellValue(cell);
            object revert = null;
            try
            {
                switch (type)
                {
                    case "bool":
                        if (IsNullOrWhiteSpace(v))
                            revert = false;
                        else
                            revert = bool.Parse(v.ToString());
                        break;
                    case "byte":
                        if (IsNullOrWhiteSpace(v))
                            revert = 0;
                        else
                            revert = ((byte)double.Parse(v.ToString()));
                        break;
                    case "short":
                        if (IsNullOrWhiteSpace(v))
                            revert = 0;
                        else
                            revert = ((short)double.Parse(v.ToString()));
                        break;
                    case "int":
                        if (IsNullOrWhiteSpace(v))
                            revert = 0;
                        else
                            revert = ((int)double.Parse(v.ToString()));
                        break;
                    case "long":
                        if (IsNullOrWhiteSpace(v))
                            revert = 0;
                        else
                            revert = ((long)double.Parse(v.ToString()));
                        break;
                    case "float":
                        if (IsNullOrWhiteSpace(v))
                            revert = 0;
                        else
                            revert = ((float)double.Parse(v.ToString()));
                        break;
                    default:
                        if (IsNullOrWhiteSpace(v))
                            revert = "";
                        else
                            revert = v.ToString();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(v);
                Console.WriteLine(e);
            }
            return revert;
        }

        private string Convert(string obj)
        {
            if (obj == null)
            {
                return null;
            }
            string v = obj.ToString();
            if ("class".Equals(v, StringComparison.OrdinalIgnoreCase))
            {
                v = "clazz";
            }
            v = v.Replace("-", "_");
            return v;
        }

        bool IsNullOrWhiteSpace(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return string.IsNullOrWhiteSpace(obj.ToString());
        }

    }
}
