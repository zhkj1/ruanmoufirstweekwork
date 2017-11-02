using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
  public class DBFactory
    {
        public static IDBHelper CreateInstance()
        {
            string strnamespace = "SqlHelper";
            string strtype = "SqlHelper.SqlHelper";
            var assembly = Assembly.Load(strnamespace);
            Type obtype = assembly.GetType(strtype);
            return (IDBHelper)Activator.CreateInstance(obtype);
        }
    }
}
