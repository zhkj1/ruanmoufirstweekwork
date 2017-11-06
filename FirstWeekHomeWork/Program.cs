using DBHelper;
using Model;
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
            Console.WriteLine("原始列表");
            List<Company> oldList = DatabaseFactory.CreateInstance().FindListEntity<Company>();
            foreach (var item in oldList)
            {
                Console.WriteLine(item.Id + "" + item.Name + "" + item.CreatorId + " " + item.LastModifierId + " " + item.LastModifyTime);
            }
            Console.WriteLine("**********************************************************************");
            Company company = new Company();
            company.Name = "18516717582";
            company.CreateTime = DateTime.Now;
            company.CreatorId = new Random().Next(0, 100);
            company.LastModifierId = new Random().Next(0, 100);
            company.LastModifyTime = DateTime.Now.AddDays(1);
            company.status = 1;
            company.Id = 7;
            int newid = 0;
            if (DataValidate.IsValidate(company))
            {
                newid = DatabaseFactory.CreateInstance().Insert<Company>(company);

            }
            else
            {
                Console.WriteLine("参数格式不正确");
                return;
            }
         
            if (newid > 1)
            {
                Console.WriteLine("添加成功");
            }
            Console.WriteLine("**********************************************************************");
            Console.WriteLine("获取刚刚添加的");
            var model = DatabaseFactory.CreateInstance().FindEntity<Company>(newid);
            foreach (var item in model.GetType().GetProperties())
            {
                Console.Write(item.GetValue(model)+"       ");
            }

          //  Console.WriteLine("**********************************************************************");
            Console.WriteLine("新列表");
            List<Company> list = DatabaseFactory.CreateInstance().FindListEntity<Company>();
            foreach (var item in list)
            {
                Console.WriteLine(item.Id+""+item.Name+""+item.CreatorId+" "+item.LastModifierId+" "+ item.LastModifyTime);
            }
            Console.WriteLine("**********************************************************************");

            Console.WriteLine("修改刚刚添加的数据也就是最后一条数据Name=修改之后的");
            model.Name = "修改之后的";
            if (DatabaseFactory.CreateInstance().Update<Company>(model)>0)
            {
                Console.WriteLine("修改成功");

            }
            Console.WriteLine("**********************************************************************");
            Console.WriteLine("修改之后的列表");
            List<Company> updateAfterList = DatabaseFactory.CreateInstance().FindListEntity<Company>();
            foreach (var item in updateAfterList)
            {
                Console.WriteLine(item.Id + "" + item.Name + "" + item.CreatorId + " " + item.LastModifierId + " " + item.LastModifyTime);
            }
            Console.WriteLine("**********************************************************************");

        //    Console.WriteLine("删除刚刚添加的数据");
         //   DatabaseFactory.CreateInstance().Delete<Company>(model.Id);
        //    Console.WriteLine("**********************************************************************");
            Console.WriteLine("修改之后的列表");
            List<Company> delAfterList = DatabaseFactory.CreateInstance().FindListEntity<Company>();
            foreach (var item in delAfterList)
            {
                foreach (var item1 in item.GetType().GetProperties())
                {
                    Console.WriteLine(item1.GetValue(item,null));
                }
                //Console.WriteLine(item.Id + "" + item.Name + "" + item.CreatorId + " " + item.LastModifierId + " " + item.LastModifyTime);
            }



        }
    }
}
