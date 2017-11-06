using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class DataValidate
    {
    

        public static bool IsValidate<T>(T entity)
        {
            var type = entity.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(EmailAttribute), true))
                {
                   object obj= prop.GetCustomAttributes(typeof(EmailAttribute), true)[0];
                    var attr = (EmailAttribute)obj;
                    if (!attr.Validate(prop.GetValue(entity, null)))
                    {
                        return false;
                    }
                }
                if (prop.IsDefined(typeof(CellphoneAttribute), true))
                {
                    object obj = prop.GetCustomAttributes(typeof(CellphoneAttribute), true)[0];
                    var attr = (CellphoneAttribute)obj;
                    if (!attr.Validate(prop.GetValue(entity, null)))
                    {
                        return false;
                    }
                }
                if (prop.IsDefined(typeof(RequiredAttribute), true))
                {
                    object obj = prop.GetCustomAttributes(typeof(RequiredAttribute), true)[0];
                    var attr = (RequiredAttribute)obj;
                    if (attr.Validate(prop.GetValue(entity, null)))
                    {
                        return false;
                    }
                }

                if (prop.IsDefined(typeof(LengthAttribute), true))
                {
                    object obj = prop.GetCustomAttributes(typeof(LengthAttribute), true)[0];
                    var attr = (LengthAttribute)obj;
                    if (attr.Validate(prop.GetValue(entity, null)))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
