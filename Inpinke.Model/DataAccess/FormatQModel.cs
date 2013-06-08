using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model.CustomClass;

namespace Inpinke.Model.DataAccess
{
    public class FormatQModel
    {
        public static string FormatQueryModel<T>(T query)
        {
            string where = " 1=1 ";
            var t = query.GetType();
            var ps = t.GetProperties();

            foreach (var p in ps)
            {
                CompareSetAttribute iValueObj = null;
                if (p.IsDefined(typeof(CompareSetAttribute), false))
                {
                    object[] attributes = p.GetCustomAttributes(typeof(CompareSetAttribute), false);
                    iValueObj = (CompareSetAttribute)attributes[0];
                }
                var value = p.GetValue(query, null);
                if (value == null || (iValueObj != null && iValueObj.IgnoreValue == value.ToString()))
                {
                    continue;
                }

                if (iValueObj == null)
                {
                    iValueObj = new CompareSetAttribute()
                    {
                        Compare = "=",
                        CompareWith = p.Name
                    };
                }

                if (p.PropertyType == typeof(string))
                {
                    where += string.Format(" and {0}{1}'{2}' ", iValueObj.CompareWith, iValueObj.Compare, value);
                }
                else
                {
                    where += string.Format(" and {0}{1}{2} ", iValueObj.CompareWith, iValueObj.Compare, value);
                }
            }
            return where;
        }
    }
}
