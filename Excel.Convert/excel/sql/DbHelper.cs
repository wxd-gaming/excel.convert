using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert.sql
{
    public class DbHelper
    {

        /// <summary>
        /// 数据库连接
        /// </summary>
        private String DbUrl { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        private String DbPort { get; set; }
        /// <summary>
        /// 数据库名字
        /// </summary>
        private String DbName { get; set; }
        /// <summary>
        /// 数据库用户
        /// </summary>
        private String DbUser { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        private String DbPwd { get; set; }
        /// <summary>
        /// 是否显示sql语句
        /// </summary>
        private bool ShowSql { get; set; }

        public R SqlConnection<R>(Func<MySqlConnection, R> call)
        {
            using (
                MySqlConnection connection = new MySqlConnection(
                    "Server=" + this.DbUrl
                    + ";port=" + this.DbPort
                    + ";Database=" + this.DbName
                    + ";user id=" + this.DbUser
                    + ";Password=" + this.DbPwd
                    + ";persist security info=False;Charset=utf8"
                    ))
            {
                connection.Open();
                return call(connection);
            }
        }
        
    }
}
