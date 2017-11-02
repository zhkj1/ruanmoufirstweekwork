using DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWeekHomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            DBFactory.CreateInstance().Show();
        }
    }
}
