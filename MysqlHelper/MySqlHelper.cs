using DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace MysqlHelper
{
    public class MySqlHelpe : IDBHelper
    {
        public static string connectionString = "";
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, parameters);
                    num = int.Parse(cmd.ExecuteScalar().ToString());
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
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
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
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
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
            throw new NotImplementedException();
        }
    }
}
