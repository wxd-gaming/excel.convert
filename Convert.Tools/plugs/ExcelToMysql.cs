using Convert.Tools;
using Convert.Tools.Code;
using Convert.Tools.Excel;
using Convert.Tools.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugs
{
    public class OutPutToMysql : IOutPutPlugs
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
            return "导入 Mysql";
        }

        public void DoAction(List<string> files)
        {
            List<ExcelDataTable> dataTables = files.AsDataTable();
            foreach (ExcelDataTable dataTable in dataTables)
            {

                using (MySqlConnection connection = DbHelper.GetConnection(DbIp, DbPort, DbName, DbUser, DbPwd))
                {

                }

            }
        }

    }
}
