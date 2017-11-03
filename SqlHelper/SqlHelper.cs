using DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlHelper
{
    public class SqlHelper : IDBHelper
    {
        public static string connectionString = AppCommon.sqlconnection;
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn,cmdType, cmdText, parameters);
                    num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;

            }
            return num;

        }

        public IDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, cmdType, cmdText, parameters);
                IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();

                throw;
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        public void Show()
        {
            Console.WriteLine("我是Sql");
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, parameters);
                   
                    object obj= cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                return  null;

            }
           
        }
    }
}
