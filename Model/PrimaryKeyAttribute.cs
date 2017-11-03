using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property,Inherited =true)]
   public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute()
        {

        }
        public PrimaryKeyAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; set;}
    }
}
