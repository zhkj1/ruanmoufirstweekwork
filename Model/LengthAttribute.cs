using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LengthAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            if (oValue.ToString().Length > MaxLength|| oValue.ToString().Length<MinLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int MaxLength{ get; set; }

        public int MinLength { get; set; }

    }
}
