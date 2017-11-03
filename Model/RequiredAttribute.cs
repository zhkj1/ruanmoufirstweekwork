using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.Property,Inherited=true)]
  public class RequiredAttribute: AbstractValidateAttribute
    {
        public RequiredAttribute()
        {

        }

        public override bool Validate(object oValue)
        {
            throw new NotImplementedException();
        }
    }
}
