using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Common;
using System.Data;

namespace DBHelper
{
    public class Database : IDatabase
    {
      /// <summary>
      ///删除数据
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="propertyValue">主键值</param>
      /// <returns></returns>
        public int Delete<T>(object propertyValue) where T : BaseModel
        {
          
            string  tableName = typeof(T).Name;
            string  keyName = DataBaseCommon.GetKeyField<T>();
            StringBuilder strSql = DataBaseCommon.DeleteSql(tableName, keyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DBFactory.CreateDbParameter(DBFactory.GetDbParmChar() + keyName, propertyValue));
            return DBFactory.CreateInstance().ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray());
        }

        public T FindEntity<T>(object propertyValue) where T : BaseModel
        {
            string tableName = typeof(T).Name;
            string keyName = DataBaseCommon.GetKeyField<T>();
            StringBuilder strSql = DataBaseCommon.SelectSql<T>(tableName, keyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DBFactory.CreateDbParameter(DBFactory.GetDbParmChar() + keyName, propertyValue));
            IDataReader dr = DBFactory.CreateInstance().ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DataBaseCommon.ReaderToModel<T>(dr);
        }

        public List<T> FindListEntity<T>() where T : BaseModel
        {
            string tableName = typeof(T).Name;
            string keyName = DataBaseCommon.GetKeyField<T>();
            StringBuilder strSql = DataBaseCommon.SelectSql<T>();
             IDataReader dr = DBFactory.CreateInstance().ExecuteReader(CommandType.Text, strSql.ToString(), null);
            return DataBaseCommon.ReaderToListModel<T>(dr);
        }

        public int Insert<T>(T entity) where T : BaseModel
        {
            string tableName = typeof(T).Name;
            string keyName = DataBaseCommon.GetKeyField<T>();
            StringBuilder strSql = DataBaseCommon.InsertSql<T>(entity);

            DbParameter[] parameter = DataBaseCommon.GetParameter<T>(entity);
            object  obj= DBFactory.CreateInstance().ExecuteScalar(CommandType.Text, strSql.ToString(), parameter);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return -1;
            }
             
        }

        public int Update<T>(T entity) where T : BaseModel
        {
            string tableName = typeof(T).Name;
            string keyName = DataBaseCommon.GetKeyField<T>();
            StringBuilder strSql = DataBaseCommon.UpdateSql<T>(entity);
            DbParameter[] parameter = DataBaseCommon.GetParameter<T>(entity);
            return   DBFactory.CreateInstance().ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
          
        }
    }
}
