using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model;
using Inpinke.Model.CustomClass;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Inpinke.Helper;

namespace Inpinke.BLL.PDFProcess
{
    public class InTimePDFBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(InTimePDFBLL));
        /// <summary>
        /// Intime 输出pdf的页面大小
        /// </summary>

        public static float PageWidth = 396.8f;
        public static float PageHeight = 396.8f;

        /// <summary>
        /// 输出缩放比例 编辑页面大小/pdf页面大小,不用换算
        /// </summary>
        public static float PaintScale = 1.06f;
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
        public static string OutPath = AppDomain.CurrentDomain.BaseDirectory + "OutPDF/Intime/";
        /// <summary>
        /// 过滤文件名中的特殊字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string FilterSpecial(string strHtml)
        {
            if (string.Empty == strHtml)
            {
                return strHtml;
            }
            string[] aryReg = { "'", "'delete", "?", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "_", ";", "||", "[", "]", "&", "/", "-", "|", " ", "''" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                strHtml = strHtml.Replace(aryReg[i], string.Empty);
            }
            return strHtml;
        }

        /// <summary>
        /// 生成书本pdf
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static bool CreateBookPDF(int bookid)
        {
            Inpinke_Book model = DBBookBLL.GetBookByID(bookid);
            string pdfname = FilterSpecial(model.BookName);
            pdfname = OutPath + pdfname + "-" + model.ID + "-intime.pdf";
            PDFProcessBLL pdfProcess = new PDFProcessBLL(PageWidth + 2 * TrimLineLength, PageHeight + 2 * TrimLineLength, pdfname);
            try
            {
                IList<Inpinke_Book_Page> pages = DBBookBLL.GetBookPage(bookid);
                pdfProcess.PaintScale = PaintScale;
                if (pages != null)
                {
                    pages = pages.OrderBy(e => e.PageNum).ToList();


                    pdfProcess.doc.Open();
                    float boneWidth = backboneWidth * model.PageCount;
                    pdfProcess.FlodPageWidth = flodPageWidth;
                    pdfProcess.BackBoneWidth = boneWidth;
                    pdfProcess.SinglePageWidth = PageWidth;
                    pdfProcess.SinglePageHeight = PageHeight;
                    for (int i = 0; i <= model.PageCount; i++)
                    {
                        Inpinke_Book_Page p = pages.Where(e => e.PageNum == i).SingleOrDefault();
                        if (p == null)
                        {
                            pdfProcess.PageWidth = PageWidth + 2 * TrimLineLength;
                            pdfProcess.PageHeight = PageHeight + 2 * TrimLineLength;
                            pdfProcess.doc.SetPageSize(new iTextSharp.text.Rectangle(pdfProcess.PageWidth, pdfProcess.PageHeight));
                            pdfProcess.doc.NewPage();
                            pdfProcess.PaintTirmLine();
                        }
                        else
                        {
                            float pWidth = PageWidth;
                            if (p.IsSkip)
                            {
                                pWidth = PageWidth * 2;
                                if (i != 0)
                                {
                                    i++;
                                }
                            }
                            else if (i != 1 && i != model.PageCount)
                            {
                                pWidth = PageWidth * 2;
                            }
                            if (i == 0)
                            {
                                //封面和封底和折页书脊总宽度
                                pWidth = PageWidth * 2 + flodPageWidth * 2 + boneWidth;
                            }

                            pdfProcess.PageWidth = pWidth + 2 * TrimLineLength;
                            pdfProcess.PageHeight = PageHeight + 2 * TrimLineLength;
                            pdfProcess.doc.SetPageSize(new iTextSharp.text.Rectangle(pdfProcess.PageWidth, pdfProcess.PageHeight));
                            pdfProcess.doc.NewPage();
                            pdfProcess.PaintTirmLine();
                            PageDataObj pageObj = (PageDataObj)SerializeXmlHelper.DeserializeFromXml(p.PageData, typeof(PageDataObj));
                            pdfProcess.PaintPage(pageObj, 0);
                            if (!p.IsSkip && i != 1 && i != model.PageCount)
                            {
                                p = pages.Where(e => e.PageNum == i + 1).SingleOrDefault();
                                if (p != null)
                                {
                                    pageObj = (PageDataObj)SerializeXmlHelper.DeserializeFromXml(p.PageData, typeof(PageDataObj));
                                    pdfProcess.PaintPage(pageObj, PageWidth);
                                    i++;
                                }
                            }
                        }
                    }
                    pdfProcess.doc.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                pdfProcess.doc.Close();
                Logger.Error(string.Format("CreateBookPDF BookID:{0},Error:{1}", bookid, ex.ToString()));
                return false;
            }

        }

        public static string FormatJsonObjectString(string jsonString)
        {
            jsonString = jsonString.TrimStart('{');
            jsonString = jsonString.Replace("\"layout\":", "");
            jsonString = Regex.Replace(jsonString, "(?<!:)(\"@)(?!.\":\\s )", "\"", RegexOptions.IgnoreCase);
            jsonString = jsonString.TrimEnd('}');
            return jsonString;
        }


    }
}
