using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using log4net;
using System.IO;

namespace Helper
{
    public class ConfigHelper
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(ConfigHelper));

        public static string ServerPath
        {
            get;
            set;
        }

        /// <summary>
        /// 读取配置值,自动根据配置文件修改情况缓存
        /// </summary>
        /// <param name="configFileName">配置文件名，如：host</param>
        /// <param name="configPath">配置节路径，如：configuration/HostBaseUrl</param>
        /// <returns></returns>
        public static string ReadConfig(string configFileName, string configPath)
        {
            string result = "";
            string key = "Config_" + configFileName + "_" + configPath;
            object cache = CacheHelper.GetCache(key);
            int mode = 0;
            if (cache != null)
            {
                result = Convert.ToString(cache);
            }
            else
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    string configUrl = "";
                    if (System.Web.HttpContext.Current == null)
                    {
                        if (!string.IsNullOrEmpty(ServerPath))
                        {
                            configUrl = ServerPath + "/Config/" + configFileName + ".config";
                            mode = 0;
                        }
                        else
                        {
                            configUrl = AppDomain.CurrentDomain.BaseDirectory + "Config\\" + configFileName + ".config";
                            mode = 1;
                        }
                    }
                    else
                    {
                        configUrl = System.Web.HttpContext.Current.Server.MapPath("/Config/" + configFileName + ".config");
                        mode = 2;
                    }
                    if (File.Exists(configUrl))
                    {
                        xmlDoc.Load(configUrl);
                        if (xmlDoc != null)
                        {
                            XmlNode node = xmlDoc.SelectSingleNode(configPath);
                            if (node != null)
                            {
                                result = node.InnerText;
                            }
                        }
                    }
                    else
                    {
                        Logger.Error("找不到配置文件:mode:" + mode + ",path:" + configUrl + ",ServerPath:" + ServerPath);
                    }

                    CacheHelper.SetCache(key, result, configUrl);
                }
                catch (Exception ex) { Logger.Error(ex); }
            }
            return result;
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="configFileName">配置文件名，如：host</param>
        /// <param name="configPath">配置节路径，如：configuration/HostBaseUrl</param>
        /// <returns></returns>
        public static bool WriteConfig(string configFileName, string configPath, string value)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string path = System.Web.HttpContext.Current.Server.MapPath("/Config/" + configFileName + ".config");
                xmlDoc.Load(path);
                if (xmlDoc != null)
                {
                    XmlNode node = xmlDoc.SelectSingleNode(configPath);
                    if (node != null)
                    {
                        node.InnerText = value;
                    }
                }
                xmlDoc.Save(path);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
