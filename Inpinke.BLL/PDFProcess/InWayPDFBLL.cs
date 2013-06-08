using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Inpinke.BLL.PDFProcess
{
   public class InWayPDFBLL
    {
       public static readonly ILog Logger = LogManager.GetLogger(typeof(InWayPDFBLL));
        /// <summary>
        /// Intime 输出pdf的页面大小
        /// </summary>
        public static float PageWidth = 566.9f;
        public static float PageHeight = 425.2f;
        /// <summary>
        /// Intime 折页宽度
        /// </summary>
        public static float flodPageWidth = 270f;
        /// <summary>
        /// 单面书脊宽度
        /// </summary>
        public static float backboneWidth = 0.43f;
        /// <summary>
        /// 边角线长度
        /// </summary>
        public static float TrimLineLength = 8.5f;
        /// <summary>
        /// 输出路径
        /// </summary>
        public static string OutPath = AppDomain.CurrentDomain.BaseDirectory + "OutPDF/Inway/";
         
        /// <summary>
        /// 生成书本pdf
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static bool CreateBookPDF(int bookid)
        {
            return true;
        }
    }
}
