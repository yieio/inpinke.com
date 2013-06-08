using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace Helper
{
    public static class Extentions
    {
        /// <summary>
        /// 对象类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        public static Ttarget Cast<Ttarget, TSource>(this TSource obj, Ttarget sample)
        {
            Ttarget t = (Ttarget)System.Activator.CreateInstance(sample.GetType());
            PropertyInfo[] pis = sample.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                PropertyInfo info = obj.GetType().GetProperty(pi.Name);
                if( info != null )
                    pi.SetValue(t, info.GetValue( obj, null), null);
            }
            return t;
        }

        public static List<Ttarget> Cast<Ttarget, Tsource>(this List<Tsource> arr, Ttarget sample)
        {
            List<Ttarget> ts = new List<Ttarget>();
            foreach (Tsource obj in arr)
            {
                ts.Add(obj.Cast(sample));
            }
            return ts.ToList();
        }

        /// <summary>
        /// 将对象的各个Property转化为Dictionary
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IDictionary<string, TValue> ToDictionary<TValue>(this object values)
        {
            IDictionary<string, TValue> dict = new Dictionary<string, TValue>();
            if (values != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    object value = descriptor.GetValue(values);
                    if (value == null)
                        continue;
                    Type type = typeof(TValue);
                    dict.Add(descriptor.Name, (TValue)Convert.ChangeType( value, type ));
                }
            }
            return dict;
        }

        public static string ToHtml(this string source )
        {
            if (string.IsNullOrEmpty(source))
                return "";
            return source.Replace(" ", "&nbsp;").Replace("\r\n", "<br/>").Replace("\n","<br/>");
        }

        public static string FromHtml(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return "";
            return source.Replace("&nbsp;", " ").Replace("<br/>", "\r\n");
        }

        public static string Join<T>(this IEnumerable<T> array, char token)
        {
            if (array.Count() == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach( T t in array )
            {
                sb.AppendFormat("{0}" + token, t.ToString());
            }
            return sb.ToString().TrimEnd(token);
        }

        public static string Format<T>(this T obj, string format)
        {
            format = System.Web.HttpUtility.UrlDecode(format);
            Regex r = new Regex( @"\{(?<prop>[_\w\d]+)\}" );
            MatchCollection mc = r.Matches(format);
            foreach (Match m in mc)
            {
                string prop = m.Groups["prop"].Value;

                format = format.Replace(m.Value, obj.GetPropertyValue(prop,""));
            }
            return format;
        }

        public static string Format<T>(this IEnumerable<T> array, string format, char token ) 
        {
            List<string> list = new List<string>();
            foreach (T t in array)
            {
                list.Add(t.Format( format));
            }
            return list.Join( token );
        }


        public static TValue GetPropertyValue<TObject, TValue>(this TObject obj, string propertyName, TValue defaultValue)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            if (pi == null)
                throw new InvalidOperationException("该属性不存在："+defaultValue.ToString());

            TValue val = pi.DeclaringType == defaultValue.GetType() ? (TValue)pi.GetValue(obj, null) : (TValue)Convert.ChangeType(pi.GetValue(obj, null), defaultValue.GetType());
            if (val == null)
                val = defaultValue;

            return val;
        }

        public static TValue GetPropertyValue<TObject, TValue>(this TObject obj, string propertyName)
        {
            return obj.GetPropertyValue<TObject, TValue>(propertyName);
        }

        public static string ToHtml(this string src, int maxLength)
        {
            return src.Length > maxLength ? src.Substring(0, maxLength).ToHtml() : src.ToHtml();
        }

        public static string ClearHtml(this string text)
        {
            //Regex r = new Regex(@"<(?!(?:br|/br|br\s*/)>)[^>]*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex r = new Regex(@"\<.*?\>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            text = r.Replace(text, "");
            //text = text.Replace(@"\<\/.*?\>", "");
            //text = text.Replace(@"\<.*?\/\>", "");
            text = text.Replace(@" ", "");
            text = text.Replace(@"&nbsp;", " ");
            return text;
        }


    }

}
