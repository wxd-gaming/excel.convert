using Newtonsoft.Json;
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

namespace Excel.Convert.Excel
{
    public class ExcelRead
    {
        public bool ReadAllSheet = true;
        public int NameRowNumber = 0;
        public int TypeRowNumber = 0;
        public int BelongRowNumber = 0;
        public int CommentRowNumber = 1;
        /// <summary>
        /// 数据读取其实行
        /// </summary>
        public int DataStartRowNumber = 2;
        public string Belong = "all";

        public char ArraySplit_1 = ':';
        public char ArraySplit_2 = ',';

        /// <summary>
        /// 所有的表
        /// </summary>
        public Dictionary<string, ExcelDataTable> Tables = new Dictionary<string, ExcelDataTable>();

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
                        int forCount = wk.NumberOfSheets;
                        if (!ReadAllSheet) forCount = 1;
                        for (int i = 0; i < forCount; i++)
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
                FormMain.ShowLog(Program.GetExceptionMsg(e, excelPath));
                throw new RuntimeException("确保excel文件处于关闭状态：" + fileName + ", " + e.Message);
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

            IRow columnRow = sheet.GetRow(NameRowNumber);//字段名字行
            IRow typeRow = sheet.GetRow(TypeRowNumber);//字段类型行
            IRow belongRow = sheet.GetRow(BelongRowNumber);//字段归属
            IRow commentRow = sheet.GetRow(CommentRowNumber);//读取配置说明

            if (columnRow == null) return;

            short lastCellNum = columnRow.LastCellNum;
            if (lastCellNum < 0)
            {
                return;
            }

            string sheetName = Convert(sheet.SheetName);

            string comment = CellValue(sheet.GetRow(0).GetCell(0));

            if (sheetName.StartsWith("sheet", StringComparison.OrdinalIgnoreCase))
            {
                FormMain.ShowLog(excelPath + " 忽律 " + sheetName);
                return;
            }

            if (!Tables.ContainsKey(sheetName))
            {
                Tables[sheetName] = new ExcelDataTable();
            }

            ExcelDataTable dataTable = Tables[sheetName];
            dataTable.Name = sheetName;
            dataTable.Comment = comment;
            dataTable.CodeName = sheetName.CodeString().FirstUpper();

            HashSet<string> columnNames = new HashSet<string>();
            for (int k = 0; k < lastCellNum; k++)
            {
                ICell columnCell = columnRow.GetCell(k);
                ICell typeCell = typeRow.GetCell(k);
                ICell belongCell = belongRow.GetCell(k);
                ICell commontCell = commentRow.GetCell(k);
                ActionColumn(excelPath, sheetName, dataTable, columnNames, columnCell, typeCell, belongCell, commontCell);
            }
            if (dataTable.KeyColumn == null)
            {
                dataTable.KeyColumn = dataTable.Columns.First().Value;
                dataTable.KeyColumn.Key = true;
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
        private ExcelDataColumn ActionColumn(string filePath, string sheetName,
            ExcelDataTable dataTable,
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
                throw new RuntimeException("文件：" + filePath + " 配置sheet " + sheetName + " 存在相同的字段 " + columnName);
            }
            if (columnNames == null)
            {
                if (dataTable.Columns.ContainsKey(columnName))
                {
                    return dataTable.Columns[columnName];
                }
                else
                {
                    return null;
                }
            }

            if (!dataTable.Columns.ContainsKey(columnName))
            {
                ExcelDataColumn dataColumn = new ExcelDataColumn();
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

                if (dataColumn.NoBeLong(Belong))
                {
                    return null;
                }

                string vtype = typeCell.ToString().ToLower();
                if ("bool".Equals(vtype)|| "boolean".Equals(vtype) || vtype.IndexOf("=bl") >= 0)
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
                else if ("float[]".Equals(vtype))
                {
                    dataColumn.ValueType = "float[]";
                    dataColumn.SqlType = "text";
                }
                else if ("float[][]".Equals(vtype))
                {
                    dataColumn.ValueType = "float[][]";
                    dataColumn.SqlType = "text";
                }
                else if ("double[]".Equals(vtype))
                {
                    dataColumn.ValueType = "double[]";
                    dataColumn.SqlType = "text";
                }
                else if ("double[][]".Equals(vtype))
                {
                    dataColumn.ValueType = "double[][]";
                    dataColumn.SqlType = "text";
                }
                else if ("string".Equals(vtype) || vtype.IndexOf("=s") >= 0)
                {
                    dataColumn.ValueType = "String";
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
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                e.ToString().WriterLog();
                            }
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
                else
                {
                    dataColumn.ValueType = "int";
                    dataColumn.SqlType = "int";
                }

                object noiteValue = CellValue(commontCell);
                dataColumn.Comment = noiteValue == null ? "" : noiteValue.ToString();
                if (string.IsNullOrWhiteSpace(dataColumn.Comment))
                {
                    dataColumn.Comment = "";
                }

                if ("id".Equals(dataColumn.Name, StringComparison.OrdinalIgnoreCase)
                    || vtype.IndexOf("=p") >= 0)
                {
                    dataColumn.Key = true;
                }

                if (dataColumn.Key)
                {
                    if (dataTable.KeyColumn != null)
                    {
                        throw new RuntimeException("存在重复 主键字段 " + dataColumn.Name);
                    }
                    dataTable.KeyColumn = dataColumn;
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
            ExcelDataTable dataTable = Tables[sheetName];
            IRow columnRow = sheet.GetRow(NameRowNumber);

            if (columnRow == null)
            {
                return;
            }

            IRow typeRow = sheet.GetRow(TypeRowNumber);
            IRow belongRow = sheet.GetRow(BelongRowNumber);
            IRow commentRow = sheet.GetRow(CommentRowNumber);

            if (sheet.LastRowNum > 2)
            {
                for (int rowNumber = DataStartRowNumber; rowNumber <= sheet.LastRowNum; rowNumber++)
                {
                    IRow rowData = sheet.GetRow(rowNumber); //读取当前行数据
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
                    for (int columnNumber = 0; columnNumber < columnRow.LastCellNum; columnNumber++)  //LastCellNum 是当前行的总列数
                    {
                        ICell columnCell = columnRow.GetCell(columnNumber);
                        ICell typeCell = typeRow.GetCell(columnNumber);
                        ICell belongCell = belongRow.GetCell(columnNumber);
                        ICell commontCell = commentRow.GetCell(columnNumber);
                        ICell dataCell = rowData.GetCell(columnNumber);

                        if (columnCell != null)
                        {
                            ExcelDataColumn dataColumn = ActionColumn(excelPath, sheetName, dataTable, null, columnCell, typeCell, belongCell, commontCell);
                            if (dataColumn != null)
                            {
                                keyValuePairs[dataColumn.Name] = CellValue(rowNumber + 1, columnNumber + 1, dataColumn.Name, dataColumn.ValueType, dataCell);
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

        private object CellValue(int rowNumber, int columnNumber, string columnName, string type, ICell cell)
        {
            string v = CellValue(cell);
            object revert = null;
            try
            {
                switch (type)
                {
                    case "bool":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = false;
                            else
                                revert = bool.Parse(v.ToString());
                        }
                        break;
                    case "byte":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = 0;
                            else
                                revert = ((byte)double.Parse(v.ToString()));
                        }
                        break;
                    case "short":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = 0;
                            else
                                revert = ((short)double.Parse(v.ToString()));
                        }
                        break;
                    case "int":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = 0;
                            else
                                revert = ((int)double.Parse(v.ToString()));
                        }
                        break;
                    case "int[]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new int[0];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_1);
                                List<int> vs1 = new List<int>();
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        vs1.Add((int)double.Parse(vs[i]));
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "int[][]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new int[0][];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_2);
                                int[][] vs1 = new int[vs.Length][];
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        string[] vs2 = vs[i].Split(ArraySplit_1);
                                        int[] vs3 = new int[vs2.Length];
                                        for (int j = 0; j < vs2.Length; j++)
                                        {
                                            if (!string.IsNullOrWhiteSpace(vs2[j]))
                                            {
                                                vs3[j] = ((int)double.Parse(vs2[j]));
                                            }
                                        }
                                        vs1[i] = vs3;
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "long":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = 0;
                            else
                                revert = ((long)double.Parse(v.ToString()));
                        }
                        break;
                    case "long[]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new long[0];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_1);
                                long[] vs1 = new long[vs.Length];
                                for (long i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        vs1[i] = ((long)double.Parse(vs[i]));
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "long[][]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new long[0][];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_2);
                                long[][] vs1 = new long[vs.Length][];
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        string[] vs2 = vs[i].Split(ArraySplit_1);
                                        long[] vs3 = new long[vs2.Length];
                                        for (int j = 0; j < vs2.Length; j++)
                                        {
                                            if (!string.IsNullOrWhiteSpace(vs2[j]))
                                            {
                                                vs3[j] = ((long)double.Parse(vs2[j]));
                                            }
                                        }
                                        vs1[i] = vs3;
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "float":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = 0f;
                            else
                                revert = (float)double.Parse(v.ToString());
                        }
                        break;
                    case "float[]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new float[0];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_1);
                                List<long> vs1 = new List<long>();
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        vs1.Add((long)double.Parse(vs[i]));
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "float[][]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new float[0][];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_2);
                                float[][] vs1 = new float[vs.Length][];
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        string[] vs2 = vs[i].Split(ArraySplit_1);
                                        float[] vs3 = new float[vs2.Length];
                                        for (int j = 0; j < vs2.Length; j++)
                                        {
                                            if (!string.IsNullOrWhiteSpace(vs2[j]))
                                            {
                                                vs3[j] = (float)double.Parse(vs2[j]);
                                            }
                                        }
                                        vs1[i] = vs3;
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "double":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = 0d;
                            else
                                revert = double.Parse(v.ToString());
                        }
                        break;
                    case "double[]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new double[0];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_1);
                                double[] vs1 = new double[vs.Length];
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        vs1[i] = double.Parse(vs[i]);
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    case "double[][]":
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = new double[0][];
                            else
                            {
                                string[] vs = v.ToString().Split(ArraySplit_2);
                                double[][] vs1 = new double[vs.Length][];
                                for (int i = 0; i < vs.Length; i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(vs[i]))
                                    {
                                        string[] vs2 = vs[i].Split(ArraySplit_1);
                                        double[] vs3 = new double[vs2.Length];
                                        for (int j = 0; j < vs2.Length; j++)
                                        {
                                            if (!string.IsNullOrWhiteSpace(vs2[j]))
                                            {
                                                vs3[j] = double.Parse(vs2[j]);
                                            }
                                        }
                                        vs1[i] = vs3;
                                    }
                                }
                                revert = vs1;
                            }
                        }
                        break;
                    default:
                        {
                            if (IsNullOrWhiteSpace(v))
                                revert = "";
                            else
                                revert = v.ToString();
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                FormMain.ShowLog("类型：" + type + ", 数据：" + v + ", 异常：" + e.Message);
                FormMain.ShowLog("数据行：" + rowNumber + ", 列：" + columnNumber + ", 名：" + columnName);
                throw e;
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
            string v = obj.ToString();
            return string.IsNullOrWhiteSpace(v) || "#null".Equals(v, StringComparison.OrdinalIgnoreCase);
        }

    }
}
