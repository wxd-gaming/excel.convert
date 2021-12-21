using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using Convert.Tools.Sql;
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
            List<ExcelDataTable> dataTables = files.AsDataTable();
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
            }
        }

    }
}
