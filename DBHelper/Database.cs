using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DBHelper
{
    public class Database : IDatabase
    {
      
        public int Delete<T>(object propertyValue) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public T FindEntity<T>(object propertyValue) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public List<T> FindListEntity<T>() where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T entity) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity) where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}
