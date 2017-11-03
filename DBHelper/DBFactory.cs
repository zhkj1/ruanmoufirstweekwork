using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
  public class DBFactory
    {
        private static string strnamespace = AppCommon.sqlnamespace;
        private static string strtype = AppCommon.sqltype;

        public static IDBHelper CreateInstance()
        {
            strnamespace = "SqlHelper";
            strtype = "SqlHelper.SqlHelper";
            var assembly = Assembly.Load(strnamespace);
            Type obtype = assembly.GetType(strtype);
            return (IDBHelper)Activator.CreateInstance(obtype);
        }

        //获取参数化的符号
        public static string GetDbParmChar()
        {
            if (strnamespace == "SqlHelper")
            {
                return "@";
            }
            else
            {
                return "?";
            }
        }
        //创建参数对象
        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            if (strnamespace == "SqlHelper")
            {
                DbParameter param = new SqlParameter();
                param.ParameterName = paramName;
                param.Value = value;
                return param;
            }
            else
            {
                DbParameter param = new MySqlParameter();
                param.ParameterName = paramName;
                param.Value = value;
                return param;
            }

        }
        public static DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            if (strnamespace == "SqlHelper")
            {
                DbParameter param = new SqlParameter();
                param.ParameterName = paramName;
                param.Value = value;
                param.DbType = dbType;
                return param;
            }
            else
            {
                DbParameter param = new MySqlParameter();
                param.ParameterName = paramName;
                param.Value = value;
                param.DbType = dbType;
                return param;
            }

        }

    }
}
