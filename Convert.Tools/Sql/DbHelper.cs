using Excel.Convert;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;

public static class DbHelper
{
    public static R Func<R>(
        string dbIp,
        int dbPort,
        string dbName,
        string dbUser,
        string dbPwd,
        Func<DbConnection, R> call)
    {

        using (DbConnection connection = GetMsqlConnection(dbIp, dbPort, dbName, dbUser, dbPwd))
        {
            connection.Open();
            DbCommand sqlCommand = connection.CreateCommand();
            CommandType text = CommandType.Text;
            DbParameter sqlParameter = sqlCommand.CreateParameter();
            return call(connection);
        }
    }

    public static DbConnection GetMsqlConnection(
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


    public static int ExecuteQuery(this String sql, string tableName,
        string dbIp,
        int dbPort,
        string dbName,
        string dbUser,
        string dbPwd)
    {
        using (DbConnection connection = DbHelper.GetMsqlConnection(dbIp, dbPort, dbName, dbUser, dbPwd))
        {
            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandTimeout = 30 * 60 * 1000;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                int executeUpdate = cmd.ExecuteNonQuery();

                FormMain.ShowLog("数据库：" + dbName + "." + tableName + " 更新数据, 影响行数：" + executeUpdate);
                return executeUpdate;
            }
        }
    }
}