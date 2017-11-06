using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class EmailAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
           // Regex RegexRegex = new Regex("");
            return Regex.IsMatch(oValue.ToString(), "^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

        }
    }
}
