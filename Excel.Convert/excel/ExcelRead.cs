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
                        var table = wk.GetSheetAt(0);
                        this.SheetName = ReplaceSideLine(table.SheetName).ToLower().Trim();
                        this.FileName = Path.GetFileName(excelPath);
                        string filename = Path.GetFileName(excelPath);
                        show("开始分析文件：" + filename + " 文件数据");
                        if (table != null)
                        {
                            if (table.LastRowNum < 3)
                            {
                                show("文件格式是第一行是字段类型，第二行字段命名，第三行是注释，第四行是归属");
                                return false;
                            }

                            IRow rowType = table.GetRow(0);  //字段类型行
                            IRow rowTitle = table.GetRow(1);  //字段名字行
                            IRow rowNontes = table.GetRow(2);  //读取配置说明
                            IRow rowGuishus = table.GetRow(3);  //字段归属

                            if (table.LastRowNum > 3)
                            {
                                for (int i = 4; i <= table.LastRowNum; i++)
                                {
                                    IRow rowData = table.GetRow(i); //读取当前行数据
                                    if (rowData != null)
                                    {
                                        // 处理如果该行为空值那么忽略
                                        // todo 考虑怎么添加
                                        for (int k = 0; k < rowTitle.LastCellNum; k++)
                                        {
                                            ICell cell2 = rowData.GetCell(k);  //当前表格
                                            if (cell2 != null && !string.IsNullOrWhiteSpace(cell2.ToString())) { goto lab_xx; }
                                        }
                                        return true;
                                    lab_xx:
                                        ExcelData.ExcelDataRow datarow = new ExcelData.ExcelDataRow();
                                        for (int k = 0; k < rowTitle.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                                        {
                                            try
                                            {
                                                GetExcelPropertyType(filename, rowType.GetCell(k), rowTitle.GetCell(k), rowNontes.GetCell(k), rowGuishus.GetCell(k), rowData.GetCell(k), ref datarow);
                                            }
                                            catch (Exception ex)
                                            {
                                                show("文件：" + Path.GetFileName(excelPath) + " 取值错误 第 " + (i + 1) + " 行 " + " 第 " + (k + 1) + " 列 " + ex.ToString());
                                                return false;
                                            }
                                        }
                                        if (string.IsNullOrWhiteSpace(datarow.AtKey))
                                        {
                                            show("文件：" + Path.GetFileName(excelPath) + " 缺少主键");
                                            return false;
                                        }
                                        if (Rows.ContainsKey(datarow.AtKey))
                                        {
                                            show("文件：" + Path.GetFileName(excelPath) + " 存在重复主键 " + datarow.AtKey + " 第 " + (i + 1) + " 行 ");
                                            return false;
                                        }

                                        if (datarow.Cells.Values.Where(l => l.IsPKey).Count() > 1)
                                        {
                                            show("文件：" + Path.GetFileName(excelPath) + " 不支持双主键设置");
                                            return false;
                                        }
                                        Rows.Add(datarow.AtKey, datarow);
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                show("请确保是2003或者wps创建的excel文件并且处于关闭状态：" + Path.GetFileName(excelPath));
                Console.WriteLine(e);
            }
        }
    }
}
