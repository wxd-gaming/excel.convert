using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.excel
{
    public class ExcelRead
    {

        /// <summary>
        /// 所有的表
        /// </summary>
        public Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

        public void ReadExcel(string excelPath)
        {
            try
            {
                using (FileStream fs = File.OpenRead(excelPath))   //打开myxls.xls文件
                {
                    IWorkbook wk;
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    if (excelPath.IndexOf(".xlsx") >= 0)
                    {
                        wk = new XSSFWorkbook(fs);
                    }
                    else if (excelPath.IndexOf(".xls") >= 0)
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
                        }
                    }
                }
            }
            catch (Exception e)
            {
                FormMain.ShowLog.Invoke("请确保是2003或者wps创建的excel文件并且处于关闭状态：" + Path.GetFileName(excelPath));
                Console.WriteLine(e);
            }
        }


        public void ActionColumn(ISheet sheet)
        {
            //这里是记录行
            int lastRowNum = sheet.LastRowNum;

            if (lastRowNum < 2)
            {
                FormMain.ShowLog.Invoke("文件格式是第一行是字段类型，第二行字段命名，第三行是注释，第四行是归属");
                return;
            }

            IRow rowType = sheet.GetRow(0);    //字段名字行和类型型行
            IRow rowNontes = sheet.GetRow(1);  //读取配置说明

            short lastCellNum = rowType.LastCellNum;
            if (lastCellNum < 0)
            {
                FormMain.ShowLog.Invoke("文件格式是第一行是字段类型，第二行字段命名，第三行是注释，第四行是归属");
            }

            string sheetName = Convert(sheet.SheetName);
            if (!tables.ContainsKey(sheetName))
            {
                tables[sheetName] = new DataTable();
            }

            DataTable dataTable = tables[sheetName];
            dataTable.Name = sheetName;
            HashSet<string> columnNames = new HashSet<string>();
            for (int i = 0; i < lastCellNum; i++)
            {
                ICell columnCell = rowType.GetCell(i);
                ICell noiteCell = rowNontes.GetCell(i);
                if (columnCell != null && !string.IsNullOrWhiteSpace(columnCell.StringCellValue))
                {
                    string columnName = columnCell.StringCellValue;
                    if (!columnNames.Add(columnName))
                    {
                        FormMain.ShowLog.Invoke("存在相同的字段");
                        return;
                    }
                    if (!dataTable.Columns.ContainsKey(columnName))
                    {
                        DataColumn dataColumn = new DataColumn();
                        dataColumn.Name = columnName;
                        dataColumn.Comment = noiteCell == null ? "" : noiteCell.StringCellValue;
                        if (string.IsNullOrWhiteSpace(dataColumn.Comment))
                        {
                            dataColumn.Comment = "";
                        }
                        dataTable.Columns[columnName] = dataColumn;
                    }
                }
            }

        }

        /// <summary>
        /// 读取数据行
        /// </summary>
        /// <param name="sheet"></param>
        public void ActionData(ISheet sheet)
        {

        }

        public string Convert(object obj)
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

    }
}
