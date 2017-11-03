using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
   public static class AppCommon
    {
        public static string sqlnamespace = ConfigurationManager.AppSettings["sqlnamespace"];
        public static string sqltype = ConfigurationManager.AppSettings["sqlnamespace"];
        public static string sqlconnection = ConfigurationManager.AppSettings["sqlconnection"];
    }
}
