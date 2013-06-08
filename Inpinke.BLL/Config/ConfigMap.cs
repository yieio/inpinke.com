using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper;

namespace Inpinke.BLL.Config
{
    public class ConfigMap
    {
        /// <summary>
        /// 静态资源路径
        /// </summary>
        public static string StaticSourceUrl
        {
            get { return ConfigHelper.ReadConfig("Settings", "configuration/StaticSourceUrl"); }
        }
        /// <summary>
        /// 用户图片资源路径
        /// </summary>
        public static string UserFileSourceUrl
        {
            get { return ConfigHelper.ReadConfig("Settings", "configuration/UserFileSourceUrl"); }
        }
        /// <summary>
        /// pdf文件路径
        /// </summary>
        public static string PdfSourceUrl
        {
            get { return ConfigHelper.ReadConfig("Settings", "configuration/PdfSourceUrl"); }
        }
        /// <summary>
        /// 前台网站地址
        /// </summary>
        public static string FrontWebsiteUrl
        {
            get { return ConfigHelper.ReadConfig("Settings", "configuration/FrontWebsiteUrl"); }
        }
        /// <summary>
        /// 后台管理员进入前台的key
        /// </summary>
        public static string AdminKey
        {
            get { return ConfigHelper.ReadConfig("Settings", "configuration/AdminKey"); }
        }

        /***Editor.Config***/
        public static int GetEditorAttr(string prodName, string attrName)
        { 
            string editorAttr = ConfigHelper.ReadConfig("Editor", "configuration/" + prodName + attrName);
            int intE = 0;
            int.TryParse(editorAttr, out intE);
            return intE; 
        }


    }
}
