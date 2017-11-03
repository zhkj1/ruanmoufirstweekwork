using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
  public interface  IDBHelper
    {
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型SQL或存储过程</param>
        /// <param name="cmdText">SQL或存储过程</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteNonQuery(CommandType cmdType,string cmdText, params DbParameter[] parameters);
        /// <summary>
        ///  执行 SQL 语句,返回首行狩猎
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameters);


        //执行有结果集返回的数据库操作命令、并返回DataReader对象
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] parameters);

        void Show();
     
    }
}
