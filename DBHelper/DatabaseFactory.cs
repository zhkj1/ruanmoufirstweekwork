using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
   public  class DatabaseFactory
    {
        public static IDatabase CreateInstance()
        {
            return new Database();
        }
    }
}
