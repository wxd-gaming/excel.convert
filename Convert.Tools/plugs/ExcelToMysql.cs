using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Plugs
{
    public class ExcelToMysql : IOutPutPlugs
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private String DbIp = "192.168.0.122";
        /// <summary>
        /// 数据库连接
        /// </summary>
        private int DbPort = 3999;
        /// <summary>
        /// 数据库名字
        /// </summary>
        private String DbName = "test3";
        /// <summary>
        /// 数据库用户
        /// </summary>
        private String DbUser = "root";
        /// <summary>
        /// 数据库密码
        /// </summary>
        private String DbPwd = "test";
        /// <summary>
        /// 是否显示sql语句
        /// </summary>
        private bool ShowSql = false;

        public PlugEnum plugEnum()
        {
            return PlugEnum.Excel;
        }

        public string PlugsName()
        {
            return "导入 Mysql 数据库";
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
                string ddl = dataTable.AsDdl();
                try
                {
                    ddl.ExecuteQuery(dataTable.Name, DbIp, DbPort, DbName, DbUser, DbPwd);
                }
                catch (Exception e)
                {
                    throw new RuntimeException(dataTable.Name + ", " + e.Message);
                }
                string data = dataTable.AsInsertSql();
                try
                {
                    data.ExecuteQuery(dataTable.Name, DbIp, DbPort, DbName, DbUser, DbPwd);
                }
                catch (Exception e)
                {
                    throw new RuntimeException(dataTable.Name + ", " + e.Message);
                }
            }
        }

    }
}
