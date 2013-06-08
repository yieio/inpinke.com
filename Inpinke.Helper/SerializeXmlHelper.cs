using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace Inpinke.Helper
{
    public static class SerializeXmlHelper
    {
        /// <summary>
        /// 序列化一个对象（非列表为XML）
        /// </summary>
        /// <span name="obj"> </span>
        /// <span name="rootName"> </span>
        /// <returns></returns>
        public static XElement SerializeToXml(this object obj, string rootName = null)
        {
            var t = obj.GetType();
            XElement root;
            if (t.IsGenericType)
            {
                root = obj.SerializeListToXml(rootName);
            }
            else if (t.IsArray)
            {
                root = obj.SerializeArrayToXml(rootName);
            }
            else
            {
                root = new XElement(t.Name);
                var ps = t.GetProperties();
                if (ps.Length == 0)
                {
                    root.Value = obj.ToString();
                }
                else
                {
                    foreach (var p in ps)
                    {
                        if (p.PropertyType.BaseType == typeof(object))
                        {
                            var node = new XElement(p.Name, p.GetValue(obj, null));
                            root.Add(node);
                        }
                        else
                            root.Add(p.GetValue(obj, null).SerializeToXml(p.Name));
                    }
                }
            }

            return root;
        }
        /// <summary>
        /// 序列化一个List为xml
        /// </summary>
        /// <span name="obj"> </span>
        /// <span name="rootName"> </span>
        /// <returns></returns>
        public static XElement SerializeListToXml(this object obj, string rootName)
        {
            var root = new XElement(rootName);
            var list = (IEnumerable)obj;
            foreach (var x in list)
            {
                root.Add(x.SerializeToXml(rootName));
            }
            return root;
        }

        /// <summary>
        /// 序列化一个数组为xml
        /// </summary>
        /// <span name="obj"> </span>
        /// <span name="rootName"> </span>
        /// <returns></returns>
        public static XElement SerializeArrayToXml(this object obj, string rootName)
        {
            var root = new XElement(rootName);
            foreach (var o in (Array)obj)
            {
                root.Add(o.SerializeToXml(rootName));
            }
            return root;
        }

        /// <summary>
        /// 反序列xml为一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <span name="element"> </span>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(this XElement element)
        {
            return (T)DeserializeFromXml(element, typeof(T));
        }

        public static object DeserializeFromXml(this XElement element, Type t)
        {           
            var obj = Activator.CreateInstance(t);
            var ps = t.GetProperties();
            foreach (var p in ps)
            {
                if (p.PropertyType.IsGenericType)
                {
                    p.SetValue(obj, DeserializeListFromXml(element, p.Name, p.PropertyType), null);
                }
                else
                {
                    var a = element.Attribute(p.Name);
                  
                    if (a != null)
                    {
                        if (p.PropertyType == typeof(int))
                        {
                            p.SetValue(obj, int.Parse(a.Value), null);
                        }
                        else if (p.PropertyType == typeof(float))
                        {
                            p.SetValue(obj, float.Parse(a.Value), null);
                        }
                        else
                        {
                            p.SetValue(obj, a.Value, null);
                        }
                       
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 反序列化xml为一个数组
        /// </summary>
        /// <span name="element"> </span>
        /// <returns></returns>
        public static T[] DeserializeArrayFromXml<T>(this XContainer element)
        {
            return (T[])DeserializeArrayFromXml(element, typeof(T));
        }

        public static Array DeserializeArrayFromXml(this XContainer element, Type t)
        {
            var elements = element.Elements();
            //创建数组
            var atype = Type.GetType(t.FullName.Replace("[]", ""));
            var array = Array.CreateInstance(atype, elements.Count());

            var i = 0;
            foreach (var e in elements)
            {
                if (e.HasElements)
                {
                    array.SetValue(DeserializeFromXml(e, atype), i);
                }
                else
                {
                    array.SetValue(Convert.ChangeType(e.Value, atype), i);
                }
                i++;
            }
            return array;
        }
        /// <summary>
        /// 反序列化xml为一个泛型ListT
        /// </summary>
        /// <span name="element"> </span>
        /// <returns></returns>
        public static T DeserializeListFromXml<T>(this XContainer element, string name)
        {
            return (T)DeserializeListFromXml(element, name, typeof(T));
        }
        /// <summary>
        /// 反序列化xml为一个泛型ListT
        /// </summary>
        /// <span name="element"> </span>
        /// <span name="t"> </span>泛型List的类型
        /// <returns></returns>
        public static IEnumerable DeserializeListFromXml(this XContainer element, string name, Type t)
        {
            var elements = element.Elements(name);

            t = Type.GetType(t.FullName.Replace("IEnumerable", "List"));

            var list = (IEnumerable)Activator.CreateInstance(t);
            var argt = t.GetGenericArguments()[0];
            var add = t.GetMethod("Add", new[] { argt });
            foreach (var e in elements)
            {
                add.Invoke(list, new[] { DeserializeFromXml(e, argt) });
            }
            return list;

        }
    }
}
