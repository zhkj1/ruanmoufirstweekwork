﻿using System;
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
            if (oValue is DBNull || string.IsNullOrEmpty(oValue.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
