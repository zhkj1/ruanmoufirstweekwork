using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    public interface IDatabase
    {
        //插入
        int Insert<T>(T entity) where T : BaseModel;
        //修改
        int Update<T>(T entity) where T : BaseModel;
        //删除
        int Delete<T>(object propertyValue) where T : BaseModel;
        //获取对象
        T FindEntity<T>(object propertyValue) where T : BaseModel;
        //获取列表
        List<T> FindListEntity<T>() where T : BaseModel;
    }
}
