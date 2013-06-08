using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Helper.Attributes
{
    /// <summary>
    /// 用于对Enum进行描述
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Field)]
    public class DescAttribute : Attribute//, ICachable
    {
        //private static bool enableCache = false;
        //private static ICacheEngine cache;
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        public DescAttribute(string desc)
        {
            Desc = desc;
        }


        public static string GetDesc(Type enumType, int eunmValue)
        {
            Dictionary<int, string> dict = ToDict(enumType);
            int intType = eunmValue;
            if (!dict.ContainsKey(eunmValue))
                return "";
            return dict[intType];
        }

        public static string GetDesc<T>(T t)
        {
            Type enumType = t.GetType();
            Dictionary<int, string> dict = ToDict(enumType);
            string enumName = Enum.Format(enumType, t, "G");
            int intType = (int)Enum.Parse(enumType, enumName);
            if (!dict.ContainsKey(intType))
                return enumName;
            return dict[intType];
        }

        /// <summary>
        /// 将一个枚举类型转化了[值:描述]字典
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDict(Type enumType)
        {
            //if (enableCache && cache.Contain(typeof(DescAttribute).Name, enumType.Name))
            //    return cache.Get<Dictionary<int, string>>(typeof(DescAttribute).Name, enumType.Name);

            Dictionary<int, string> result = new Dictionary<int, string>();
            if (!enumType.IsEnum)
                throw new InvalidOperationException("不允许对非枚举类型进行转化操作");

            FieldInfo[] fieldInfos = enumType.GetFields();
            if (fieldInfos.Length == 0)
                return result;

            foreach (FieldInfo info in fieldInfos)
            {
                if (!info.FieldType.IsEnum)
                    continue;
                int intValue = (int)Enum.Parse(enumType, info.Name);
                object[] descs = info.GetCustomAttributes(typeof(DescAttribute), true);
                if (descs.Length != 1)
                    throw new InvalidOperationException("不允许对多于或少于一个DescAttribute枚举进行转化");

                result.Add(intValue, ((DescAttribute)descs[0]).Desc);
            }

            //if (enableCache)
            //    cache.Add(typeof(DescAttribute).Name, enumType.Name, result);

            return result;
        }

        public static IEnumerable<string> GetDescs(Type t, string[] values)
        {
            Dictionary<int, string> descs = ToDict(t);
            List<string> result = new List<string>();
            foreach (string val in values)
            {
                int intVal = 0;
                if (int.TryParse(val, out intVal))
                    result.Add(descs[intVal]);
            }
            return result;
        }

        public static string ToOptionListStr(Type enumType, int selectedValue)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<int, string> oplist = ToDict(enumType);
            foreach (KeyValuePair<int, string> d in oplist)
            {
                if (d.Key == selectedValue)
                {
                    sb.Append("<option value=\"" + d.Key + "\" selected=\"true\">" + d.Value + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + d.Key + "\">" + d.Value + "</option>");
                }
            }
            return sb.ToString();
        }

        #region ICachable 成员

        //public void SetCacheEngine(ICacheEngine engine)
        //{
        //    cache = engine;
        //    enableCache = true;
        //}

        #endregion
    }
}
