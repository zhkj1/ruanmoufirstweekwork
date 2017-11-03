using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.Property,Inherited =true)]
   public  class PropReplaceAttribute:Attribute
    {
        public PropReplaceAttribute()
        {

        }
        public PropReplaceAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}
