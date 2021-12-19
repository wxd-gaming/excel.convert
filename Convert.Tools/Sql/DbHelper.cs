using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Convert.Tools.Sql
{
    public class DbHelper
    {
        public static R Func<R>(
            string dbIp,
            int dbPort,
            string dbName,
            string dbUser,
            string dbPwd,
            Func<MySqlConnection, R> call)
        {
            using (MySqlConnection connection = GetConnection(dbIp, dbPort, dbName, dbUser, dbPwd))
            {
                connection.Open();
                return call(connection);
            }
        }

        public static MySqlConnection GetConnection(
            string dbIp,
            int dbPort,
            string dbName,
            string dbUser,
            string dbPwd)
        {
            MySqlConnection connection = new MySqlConnection(
                "Server=" + dbIp
                + ";port=" + dbPort
                + ";Database=" + dbName
                + ";user id=" + dbUser
                + ";Password=" + dbPwd
                + ";persist security info=False;Charset=utf8"
                );

            connection.Open();
            return connection;
        }
    }
}
