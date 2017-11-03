using Model;
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

    public class DataBaseCommon
    {
        /// <summary>
        ///   拼接删除sql
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static StringBuilder DeleteSql(string strTableName, string keyName)
        {
            return new StringBuilder("DELETE  FROM " + strTableName + " WHERE " + keyName + "=" + DBFactory.GetDbParmChar() + keyName + " ");
        }

        public static StringBuilder InsertSql<T>(T entity)
        {
            StringBuilder strSql = new StringBuilder();
            var type = entity.GetType();
            strSql.Append("INSERT INTO " + type.Name + " (");
            //拼接参数
            StringBuilder sbparp = new StringBuilder();
            //拼接字段
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (var prop in type.GetProperties())
            {
                // !prop.IsDefined(typeof(PrimaryKeyAttribute))
                //判读除去自增字段
                if (prop.GetValue(entity, null) != null && GetKeyField<T>() != prop.Name)
                {
                    if (prop.IsDefined(typeof(PropReplaceAttribute)))
                    {
                        object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                        PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                        sb.Append(PropReplace.Name + ",");
                        sbparp.Append(DBFactory.GetDbParmChar() + PropReplace.Name + ",");
                    }
                    else
                    {
                        sb.Append(prop.Name + ",");
                        sbparp.Append(DBFactory.GetDbParmChar() + prop.Name + ",");
                    }


                }
            }

            strSql.Append(sb.Remove(sb.Length - 1, 1) + ")");
            strSql.Append("VALUES(");
            strSql.Append(sbparp.Remove(sbparp.Length - 1, 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            return strSql;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static StringBuilder UpdateSql<T>(T entity)
        {
            string keyName = GetKeyField<T>().ToString();
            StringBuilder strSql = new StringBuilder();
            var type = entity.GetType();
            string tableName = type.Name;
            strSql.Append(" UPDATE ");
            strSql.Append(tableName);
            strSql.Append(" SET ");
            foreach (var prop in type.GetProperties())
            {
                if (prop.GetValue(entity, null) != null && prop.Name != keyName)
                {
                    if (prop.IsDefined(typeof(PropReplaceAttribute)))
                    {
                        object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                        PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                        strSql.Append(PropReplace.Name + "=" + DBFactory.GetDbParmChar() + PropReplace.Name + ",");
                    }
                    else
                    {
                        strSql.Append(prop.Name + "=" + DBFactory.GetDbParmChar() + prop.Name + ",");

                    }

                }
            }
            strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where " + keyName + "=" + DBFactory.GetDbParmChar() + keyName);
            return strSql;
        }
        /// <summary>
        /// 查询sql拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strTableName"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static StringBuilder SelectSql<T>(string strTableName, string keyName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            Type type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(PropReplaceAttribute)))
                {
                    object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                    PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                    strSql.Append(PropReplace.Name + ",");
                }
                else
                {
                    strSql.Append(prop.Name + ",");

                }


            }
            strSql.Remove(strSql.Length - 1, 1);
            strSql.AppendFormat(" FROM {0} WHERE {1} ={2}", type.Name, keyName, DBFactory.GetDbParmChar() + keyName);
            return strSql;


        }

        public static StringBuilder SelectSql<T>()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            Type type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(PropReplaceAttribute)))
                {
                    object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                    PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                    strSql.Append(PropReplace.Name + ",");
                }
                else
                {
                    strSql.Append(prop.Name + ",");

                }

            }
            strSql.Remove(strSql.Length - 1, 1);
            strSql.AppendFormat(" FROM {0} ", type.Name);
            return strSql;


        }

        public static bool IsNull(object obj)
        {
            if (obj is DBNull || string.IsNullOrEmpty(obj.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T ReaderToModel<T>(IDataReader dr)
        {
            using (dr)
            {
                T model = Activator.CreateInstance<T>();
                if (dr.Read())
                {
                    foreach (var prop in model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                    {

                        if (prop.IsDefined(typeof(PropReplaceAttribute)))
                        {
                            object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                            PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                            if (!IsNull(dr[PropReplace.Name]))
                            {
                                prop.SetValue(model, dr[PropReplace.Name]);
                            }

                        }
                        else
                        {
                            if (!IsNull(dr[prop.Name]))
                            {
                                prop.SetValue(model, dr[prop.Name]);

                            }
                        }



                    }
                }
                dr.Close();
                return model;
            }
        }
        public static List<T> ReaderToListModel<T>(IDataReader dr)
        {
            using (dr)
            {
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    foreach (var prop in model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                    {

                        if (prop.IsDefined(typeof(PropReplaceAttribute)))
                        {
                            object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                            PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                            if (!IsNull(dr[PropReplace.Name]))
                            {
                                prop.SetValue(model, dr[PropReplace.Name]);

                            }

                        }
                        else
                        {
                            if (!IsNull(dr[prop.Name]))
                            {
                                prop.SetValue(model, dr[prop.Name]);
                            }

                        }

                    }
                    list.Add(model);
                }
                dr.Close();
                return list;
            }
        }
        /// <summary>
        /// 获取实体主键字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetKeyField<T>()
        {
            Type type = typeof(T);
            string keyField = "";
            if (type.IsDefined(typeof(PrimaryKeyAttribute), true))
            {
                object item = type.GetCustomAttributes(typeof(PrimaryKeyAttribute), true)[0];
                PrimaryKeyAttribute primaryKey = item as PrimaryKeyAttribute;
                if (primaryKey != null)
                {
                    keyField = primaryKey.Name;
                }
            }
            return keyField;
        }

        public static DbParameter[] GetParameter<T>(T entity)
        {
            DbType dbtype = new DbType();
            IList<DbParameter> parameter = new List<DbParameter>();
            var type = entity.GetType();

            foreach (var prop in type.GetProperties())
            {
                //&& GetKeyField<T>() != prop.Name
                if (prop.GetValue(entity, null) != null)
                {
                    var propType = prop.PropertyType;
                    if (propType == typeof(int) || propType == typeof(int?))
                    {
                        dbtype = DbType.Int32;
                    }
                    else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                    {
                        dbtype = DbType.DateTime;
                    }
                    else
                    {
                        dbtype = DbType.String;
                    }
                    if (prop.IsDefined(typeof(PropReplaceAttribute)))
                    {
                        object item = prop.GetCustomAttributes(typeof(PropReplaceAttribute), true)[0];
                        PropReplaceAttribute PropReplace = item as PropReplaceAttribute;
                        parameter.Add(DBFactory.CreateDbParameter(DBFactory.GetDbParmChar() + PropReplace.Name, prop.GetValue(entity, null), dbtype));

                    }
                    else
                    {
                        parameter.Add(DBFactory.CreateDbParameter(DBFactory.GetDbParmChar() + prop.Name, prop.GetValue(entity, null), dbtype));

                    }

                }

            }
            return parameter.ToArray();
        }

    }
}
