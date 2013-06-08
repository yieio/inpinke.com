using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Xml.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using Inpinke.Model.CustomClass;

namespace Inpinke.BLL.PDFProcess
{
    public class PDFProcessBLL
    {
        public readonly ILog Logger = LogManager.GetLogger(typeof(PDFProcessBLL));
        /// <summary>
        /// XmlDocument对象(对Xml数据读取的类)
        /// </summary>
        public XDocument xmlDoc { get; set; }
        /// <summary>
        /// 生成PDF的Document
        /// </summary>
        public Document doc { get; set; }
        /// <summary>
        /// 页面宽 单位pt
        /// </summary>
        public float PageWidth { get; set; }
        /// <summary>
        /// 页面高
        /// </summary>
        public float PageHeight { get; set; }
        /// <summary>
        /// 单页宽度,高度
        /// </summary>
        public float SinglePageWidth { get; set; }
        public float SinglePageHeight { get; set; }
        /// <summary>
        /// 折页宽度
        /// </summary>
        public float FlodPageWidth { get; set; }
        /// <summary>
        /// 书脊宽度
        /// </summary>
        public float BackBoneWidth { get; set; }
        /// <summary>
        /// 出血
        /// </summary>
        public float PageMarginEdge = 14.4f;
        /// <summary>
        /// 保存PDF
        /// </summary>
        public PdfWriter pdfWriter { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string[] fontPath =
        { 
            AppDomain.CurrentDomain.BaseDirectory + "content\\fonts\\FZLTHK_0.ttf", //方正兰亭黑_GBK
            AppDomain.CurrentDomain.BaseDirectory + "content\\fonts\\ARIAL.ttf" 
        };
        /// <summary>
        ///  72f / 350f;
        /// </summary>
        public float DpiScale = 0.2f;
        /// <summary>
        /// 输出缩放比例 编辑页面大小/pdf页面大小
        /// </summary>
        public float PaintScale = 1.06f;
        /// <summary>
        /// 边角线在X，Y轴上的位置
        /// </summary>
        public float TrimLineX = 8.5f;
        public float TrimLineY = 8.5f;
        /// <summary>
        /// 边角线长度
        /// </summary>
        public float TrimLineLength = 8.5f;
        public float TrimLineWidth = 14.0f;
        /// <summary>
        /// 封面页码
        /// </summary>
        public int CoverPNum = 0;

        public PDFProcessBLL(float width, float height, string strOutput)
        {
            PageWidth = width;
            PageHeight = height;
            try
            {
                iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(PageWidth, PageHeight);
                doc = new Document(rectangle, PageMarginEdge, PageMarginEdge, PageMarginEdge, PageMarginEdge);
                pdfWriter = PdfWriter.GetInstance(doc, new FileStream(strOutput, FileMode.Create));
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("PDFProcessBLL Error:{0}", ex.ToString()));
            }
        }
        /// <summary>
        /// 输出封面
        /// </summary>
        /// <param name="pObj"></param>
        public void PaintCoverPage(PageDataObj pObj)
        {
            if (!string.IsNullOrEmpty(pObj.bgcolor) && pObj.bgcolor.ToUpper() != "#FFFFFF")
            {
                PaintBGColor(pObj.bgcolor, TrimLineLength + FlodPageWidth, TrimLineLength, PageWidth - 2 * TrimLineLength - 2 * FlodPageWidth, PageHeight - 2 * TrimLineLength);
            }
            //画封面折线
            PaintCoverLine();
            if (pObj.text != null)
            {
                foreach (PageText ptxt in pObj.text)
                {
                    float textX = ptxt.conx / PaintScale + TrimLineLength + BackBoneWidth + FlodPageWidth + (pObj.isskip == "true" ? 0 : SinglePageWidth);
                    float textWidth = ptxt.conwidth / PaintScale;
                    float textHeight = ptxt.conheight / PaintScale;
                    float textY = ptxt.cony / PaintScale + TrimLineLength;
                    float textTop = ptxt.cony / PaintScale + TrimLineLength;

                    FontFactory.Register(fontPath[0]);
                    BaseFont bfChinese = BaseFont.CreateFont(fontPath[0], BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    float fontsize = ptxt.fontsize;
                    int align = ptxt.textalign == "left" ? Element.ALIGN_LEFT : ptxt.textalign == "right" ? Element.ALIGN_RIGHT : ptxt.textalign == "center" ? Element.ALIGN_CENTER : Element.ALIGN_LEFT;
                    if (ptxt.issingle == "true")
                    {
                        textY = PageHeight - ptxt.cony / PaintScale - TrimLineLength - textHeight;
                        fontsize = 8f;
                        if (pObj.pnum == CoverPNum)
                        {
                            fontsize = ptxt.conid == "txt_1" ? 27f : 15f;
                        }
                        PaintContent(ptxt.color, ptxt.content, fontPath[0], fontsize, textHeight / 1.5f, 0, textX, textY, textHeight, textWidth, align);
                    }
                    else
                    {
                        PaintContent(ptxt.color, ptxt.content, fontPath[0], fontsize, 17f, 0, textX, textY, textHeight, textWidth, align);
                    }
                }
            }

            if (pObj.image != null)
            {
                foreach (PageImage pimg in pObj.image)
                {
                    float imageX = pimg.conx / PaintScale + TrimLineLength + FlodPageWidth;
                    float imageWidth = pimg.conwidth / PaintScale;
                    if (pObj.isskip != "true")
                    {
                        imageX += BackBoneWidth + SinglePageWidth;
                    }
                    else
                    {
                        imageWidth += BackBoneWidth;
                    }

                    float imageHeight = pimg.conheight / PaintScale;
                    float imageY = PageHeight - pimg.cony / PaintScale - TrimLineLength - imageHeight;

                    float floZoom = pimg.orgheight / pimg.height;
                    int intZoomWidth = Convert.ToInt32(pimg.conwidth * floZoom * 4);
                    int intZoomHeight = Convert.ToInt32(pimg.conheight * floZoom * 4);
                    int intOutputWidth = (int)((imageWidth) / DpiScale);
                    int intOutputHeight = (int)((imageHeight) / DpiScale);
                    int startX = Convert.ToInt32(pimg.x * floZoom * 4) * -1;
                    int startY = Convert.ToInt32(pimg.y * floZoom * 4) * -1;

                    string strImageUrl = GetLocationUrl(pimg.src);
                    Bitmap bitmapSource = new Bitmap(strImageUrl);
                    Bitmap bitmapCut = new Bitmap(intZoomWidth, intZoomHeight);
                    Bitmap bitmapZoom = new Bitmap(intOutputWidth, intOutputHeight);
                    CutPhoto(ref bitmapCut, bitmapSource, new System.Drawing.Rectangle(0, 0, intZoomWidth, intZoomHeight), startX, startY, intZoomWidth, intZoomHeight);
                    CutPhoto(ref bitmapZoom, bitmapCut, new System.Drawing.Rectangle(0, 0, intOutputWidth, intOutputHeight), 0, 0, intZoomWidth, intZoomHeight);

                    PaintImage(bitmapZoom, imageX, imageY);
                    bitmapSource.Dispose();
                    bitmapCut.Dispose();
                }
            }
            //在封底折页输出logo
            string logoUrl = AppDomain.CurrentDomain.BaseDirectory + "/content/pdf/inpinke_logo_pdf4.jpg";
            Bitmap logoBitMap = new Bitmap(logoUrl);
            PaintImage(logoBitMap, PageWidth - TrimLineLength - FlodPageWidth + 40f, 40f);

        }

        /// <summary>
        /// 输出页面
        /// </summary>
        public void PaintPage(PageDataObj pObj, float pStartX)
        {
            if (pObj.pnum == this.CoverPNum)
            {
                PaintCoverPage(pObj);
                return;
            }
            if (!string.IsNullOrEmpty(pObj.bgcolor) && pObj.bgcolor.ToUpper() != "#FFFFFF")
            {
                PaintBGColor(pObj.bgcolor, TrimLineLength, TrimLineLength, PageWidth - 2 * TrimLineLength, PageHeight - 2 * TrimLineLength);
            }

            if (pObj.text != null)
            {
                foreach (PageText ptxt in pObj.text)
                {
                    float textX = ptxt.conx / PaintScale + TrimLineLength + pStartX;
                    float textWidth = ptxt.conwidth / PaintScale;
                    float textHeight = ptxt.conheight / PaintScale;
                    float textY = ptxt.cony / PaintScale + TrimLineLength;
                    float textTop = ptxt.cony / PaintScale + TrimLineLength;

                    FontFactory.Register(fontPath[0]);
                    BaseFont bfChinese = BaseFont.CreateFont(fontPath[0], BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    float fontsize = ptxt.fontsize;
                    int align = ptxt.textalign == "left" ? Element.ALIGN_LEFT : ptxt.textalign == "right" ? Element.ALIGN_RIGHT : ptxt.textalign == "center" ? Element.ALIGN_CENTER : Element.ALIGN_LEFT;
                    if (ptxt.issingle == "true")
                    {
                        textY = PageHeight - ptxt.cony / PaintScale - TrimLineLength - textHeight;
                        fontsize = 8f;
                        if (pObj.pnum == 0)
                        {
                            fontsize = ptxt.conid == "txt_1" ? 27f : 15f;
                        }
                        PaintContent(ptxt.color, ptxt.content, fontPath[0], fontsize, textHeight / 1.5f, 0, textX, textY, textHeight, textWidth, align);
                    }
                    else
                    {
                        PaintContent(ptxt.color, ptxt.content, fontPath[0], fontsize, 17f, 0, textX, textY, textHeight, textWidth, align);
                    }
                }
            }

            if (pObj.image != null)
            {
                foreach (PageImage pimg in pObj.image)
                {
                    float imageX = pimg.conx / PaintScale + TrimLineLength + pStartX;
                    float imageWidth = pimg.conwidth / PaintScale;
                    float imageHeight = pimg.conheight / PaintScale;
                    float imageY = PageHeight - pimg.cony / PaintScale - TrimLineLength - imageHeight;

                    float floZoom = pimg.orgheight / pimg.height;
                    int intZoomWidth = Convert.ToInt32(pimg.conwidth * floZoom * 4);
                    int intZoomHeight = Convert.ToInt32(pimg.conheight * floZoom * 4);
                    int intOutputWidth = (int)((imageWidth) / DpiScale);
                    int intOutputHeight = (int)((imageHeight) / DpiScale);
                    int startX = Convert.ToInt32(pimg.x * floZoom * 4) * -1;
                    int startY = Convert.ToInt32(pimg.y * floZoom * 4) * -1;

                    string strImageUrl = GetLocationUrl(pimg.src);
                    Bitmap bitmapSource = new Bitmap(strImageUrl);
                    Bitmap bitmapCut = new Bitmap(intZoomWidth, intZoomHeight);
                    Bitmap bitmapZoom = new Bitmap(intOutputWidth, intOutputHeight);
                    CutPhoto(ref bitmapCut, bitmapSource, new System.Drawing.Rectangle(0, 0, intZoomWidth, intZoomHeight), startX, startY, intZoomWidth, intZoomHeight);
                    CutPhoto(ref bitmapZoom, bitmapCut, new System.Drawing.Rectangle(0, 0, intOutputWidth, intOutputHeight), 0, 0, intZoomWidth, intZoomHeight);

                    PaintImage(bitmapZoom, imageX, imageY);
                    bitmapSource.Dispose();
                    bitmapCut.Dispose();
                }
            }
        }
        /// <summary>
        /// 画边角线
        /// </summary>
        public void PaintTirmLine()
        {
            PdfContentByte contentByte = pdfWriter.DirectContent;
            contentByte.SetCMYKColorFill(0, 0, 0, 255);
            contentByte.SetCMYKColorStroke(0, 0, 0, 255);
            contentByte.SetLineWidth(0.25f);

            //左下角
            PaintLine(0, TrimLineY, TrimLineWidth, TrimLineY, contentByte);
            PaintLine(TrimLineX, 0, TrimLineX, TrimLineWidth, contentByte);

            //左上角
            PaintLine(0, PageHeight - TrimLineY, TrimLineWidth, PageHeight - TrimLineY, contentByte);
            PaintLine(TrimLineX, PageHeight, TrimLineX, PageHeight - TrimLineWidth, contentByte);

            //右上角
            PaintLine(PageWidth, PageHeight - TrimLineY, PageWidth - TrimLineWidth, PageHeight - TrimLineY, contentByte);
            PaintLine(PageWidth - TrimLineX, PageHeight, PageWidth - TrimLineX, PageHeight - TrimLineWidth, contentByte);

            //右下角
            PaintLine(PageWidth, TrimLineY, PageWidth - TrimLineWidth, TrimLineY, contentByte);
            PaintLine(PageWidth - TrimLineX, 0, PageWidth - TrimLineX, TrimLineWidth, contentByte);

            contentByte.Stroke();//打完收工
        }
        /// <summary>
        /// 画封面书脊，折页线
        /// </summary>
        public void PaintCoverLine()
        {
            PdfContentByte cb = pdfWriter.DirectContent;
            cb.SetCMYKColorFill(0, 0, 0, 255);
            cb.SetCMYKColorStroke(0, 0, 0, 255);
            cb.SetLineWidth(0.25f);
            //封底折页
            PaintLine(TrimLineLength + FlodPageWidth, 0, TrimLineLength + FlodPageWidth, TrimLineWidth, cb);
            PaintLine(TrimLineLength + FlodPageWidth, PageHeight, TrimLineLength + FlodPageWidth, PageHeight - TrimLineWidth, cb);
            //封面折页
            PaintLine(PageWidth - TrimLineLength - FlodPageWidth, 0, PageWidth - TrimLineLength - FlodPageWidth, TrimLineWidth, cb);
            PaintLine(PageWidth - TrimLineLength - FlodPageWidth, PageHeight, PageWidth - TrimLineLength - FlodPageWidth, PageHeight - TrimLineWidth, cb);
            //书脊
            PaintLine(TrimLineLength + FlodPageWidth + SinglePageWidth, 0, TrimLineLength + FlodPageWidth + SinglePageWidth, TrimLineWidth, cb);
            PaintLine(TrimLineLength + FlodPageWidth + SinglePageWidth, PageHeight, TrimLineLength + FlodPageWidth + SinglePageWidth, PageHeight - TrimLineWidth, cb);

            PaintLine(TrimLineLength + FlodPageWidth + SinglePageWidth + BackBoneWidth, 0, TrimLineLength + FlodPageWidth + SinglePageWidth + BackBoneWidth, TrimLineWidth, cb);
            PaintLine(TrimLineLength + FlodPageWidth + SinglePageWidth + BackBoneWidth, PageHeight, TrimLineLength + FlodPageWidth + SinglePageWidth + BackBoneWidth, PageHeight - TrimLineWidth, cb);
            cb.Stroke();//打完收工

        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="starY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        public void PaintLine(float startX, float startY, float endX, float endY, PdfContentByte cb)
        {
            cb.MoveTo(startX, startY);
            cb.LineTo(endX, endY);
        }

        /// <summary>
        /// 剪切图片
        /// </summary>
        /// <param name="bitmapCut">输出图片</param>
        /// <param name="bitmapSource">源图片</param>
        /// <param name="outRectangle">截取框</param>
        /// <param name="srcX">起截点X坐标</param>
        /// <param name="srcY">起截点Y坐标</param>
        /// <param name="srcWidth">截取宽度</param>
        /// <param name="srcHeight">截取高度</param>
        public void CutPhoto(ref Bitmap bitmapCut, Bitmap bitmapSource, System.Drawing.Rectangle outRectangle, float srcX, float srcY, float srcWidth, float srcHeight)
        {
            Graphics g = Graphics.FromImage(bitmapCut);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            g.Clear(System.Drawing.Color.White);
            g.DrawImage(bitmapSource, outRectangle, srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);
            g.Dispose();
        }

        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <param name="absoluteX"></param>
        /// <param name="absoluteY"></param>
        public void PaintImage(Bitmap bitmapSource, float absoluteX, float absoluteY)
        {
            ImageFormat imageFormat = ImageFormat.Jpeg;
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmapSource, imageFormat);
            image.SetDpi(350, 350);

            float floImageWidth = image.ScaledWidth * DpiScale;
            float floImageHeight = image.ScaledHeight * DpiScale;

            image.ScaleToFit(floImageWidth, floImageHeight);
            image.SetAbsolutePosition(absoluteX, absoluteY);
            doc.Add(image);
        }

        /// <summary>
        /// 向pdf页输出文字
        /// </summary>
        /// <param name="textColor">字体颜色</param>
        /// <param name="fontPath">字体</param>
        /// <param name="fontSize">字体大小 pt</param>
        /// <param name="textLeading">行距</param>
        /// <param name="textSpacing">字间距</param>
        /// <param name="textContent">文字内容</param>
        /// <param name="textX">输出位置的X坐标</param>
        /// <param name="textY">输出位置的Y坐标</param>
        public void PaintText(string textColor, string fontPath, float fontSize, float textLeading, float textSpacing, string textContent, float textX, float textY, float maxWidth)
        {
            CMYK_Color cmyk_titleColor = new CMYK_Color().GetCMYK_Color(textColor);

            FontFactory.Register(fontPath);
            BaseFont bfChinese = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            PdfContentByte contentByte = pdfWriter.DirectContent;
            contentByte.BeginText();
            contentByte.SetCMYKColorFill(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            contentByte.SetCMYKColorStroke(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            contentByte.SetFontAndSize(bfChinese, fontSize);
            contentByte.SetLeading(textLeading);
            contentByte.SetCharacterSpacing(textSpacing);
            contentByte.SetTextMatrix(textX, textY);

            float floLineContentSize = bfChinese.GetWidthPointKerned(textContent, fontSize);
            if (floLineContentSize > maxWidth)
            {
                string strOutputContent = textContent.Substring(0, textContent.Length - 1);
                floLineContentSize = bfChinese.GetWidthPointKerned(strOutputContent, fontSize);
                while (floLineContentSize > maxWidth)
                {
                    strOutputContent = textContent.Substring(0, strOutputContent.Length - 1);
                    floLineContentSize = bfChinese.GetWidthPointKerned(strOutputContent, fontSize);
                }
                contentByte.ShowText(strOutputContent);

                //contentByte.ShowTextAligned((int)ContentAlignment.TopCenter, strOutputContent, textX, textY, 0);
            }
            else
            {
                contentByte.ShowText(textContent);
            }
            contentByte.Stroke();
            contentByte.EndText();
        }

        public void PaintText(string textColor, string fontPath, float fontSize, string text, float textX, float textY, float textWidth)
        {
            CMYK_Color cmyk_titleColor = new CMYK_Color().GetCMYK_Color(textColor);
            FontFactory.Register(fontPath);
            BaseFont bfChinese = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            Phrase Phr_Content = new Phrase(text, new iTextSharp.text.Font(bfChinese, fontSize));
            PdfContentByte ConByte_Up = pdfWriter.DirectContent;
            ColumnText.ShowTextAligned(ConByte_Up, Element.ALIGN_CENTER, Phr_Content, textX, textY, 0);
        }

        public void PaintText(string textColor, string fontPath, float fontSize, float textLeading, float textSpacing, string textContent, float textX, float textY, bool isH)
        {
            CMYK_Color cmyk_titleColor = new CMYK_Color().GetCMYK_Color(textColor);

            FontFactory.Register(fontPath);
            BaseFont bfChinese = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            PdfContentByte contentByte = pdfWriter.DirectContent;
            contentByte.BeginText();
            contentByte.SetCMYKColorFill(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            contentByte.SetCMYKColorStroke(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            contentByte.SetFontAndSize(bfChinese, fontSize);
            contentByte.SetLeading(textLeading);
            contentByte.SetCharacterSpacing(textSpacing);
            //contentByte.SetTextMatrix(textX, textY);
            string p1 = "^[]】｝》>)）』@[{}【｛《<(（『‖︱ ︳♀♂]$";

            for (int i = 0; i < textContent.Length; i++)
            {
                contentByte.SetTextMatrix(textX, textY - (10f * i));
                if (Regex.IsMatch(textContent[i].ToString(), p1))
                {
                    contentByte.ShowTextAligned(Element.ALIGN_CENTER, textContent[i].ToString(), textX + 1f, textY - (10f * i), -90);
                }
                else
                {
                    contentByte.ShowText(textContent[i].ToString());
                }

                contentByte.NewlineText();
            }
            contentByte.Stroke();
            contentByte.EndText();
        }

        public bool IsNatural_Number(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str);
        }

        /// <summary>
        /// 输出多行文字
        /// </summary>
        /// <param name="textColor"></param>
        /// <param name="fontPath"></param>
        /// <param name="fontSize"></param>
        /// <param name="textLeading"></param>
        /// <param name="textSpacing"></param>
        /// <param name="textContent"></param>
        /// <param name="textX"></param>
        /// <param name="textY"></param>
        public void PaintContent(string textColor, string fontPath, float fontSize, float textLeading, float textSpacing, string textContent, float textX, float textY, float maxWidth, int maxLine)
        {
            textContent = textContent.Replace("\n", "&hh;");
            textContent = textContent.Replace("&hh;", "\r\n");

            CMYK_Color cmyk_titleColor = new CMYK_Color().GetCMYK_Color(textColor);

            FontFactory.Register(fontPath);
            BaseFont bfChinese = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            PdfContentByte contentByte = pdfWriter.DirectContent;
            contentByte.BeginText();
            contentByte.SetCMYKColorFill(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            contentByte.SetCMYKColorStroke(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            contentByte.SetFontAndSize(bfChinese, fontSize);
            contentByte.SetLeading(textLeading);
            contentByte.SetCharacterSpacing(textSpacing);
            contentByte.SetTextMatrix(textX, textY);


            string strOutputContent = "";
            int linesCount = 1;
            for (int i = 0; i < textContent.Length; i++)
            {
                if (linesCount > maxLine)
                {
                    break;
                }
                strOutputContent += textContent[i].ToString();
                float floLineContentSize = bfChinese.GetWidthPointKerned(strOutputContent, fontSize);
                if ((strOutputContent.Contains("\r") == true && textContent.Substring(i, 2).Equals("\r\n") == true) || floLineContentSize >= maxWidth)
                {
                    string nextNeedStr = "";
                    if (floLineContentSize >= maxWidth && (i + 1) < textContent.Length && textContent[i] != ' ' && IsNatural_Number(textContent[i].ToString()) && IsNatural_Number(textContent[i].ToString()))
                    {
                        if (textContent[i + 1] != ' ')
                        {
                            int lastWhiteStrIndex = strOutputContent.LastIndexOf(' ');
                            if (lastWhiteStrIndex > 20)
                            {
                                nextNeedStr = strOutputContent.Substring(lastWhiteStrIndex);
                                strOutputContent = strOutputContent.Substring(0, lastWhiteStrIndex + 1);
                                nextNeedStr = nextNeedStr.TrimStart(' ');
                            }
                        }
                        else
                        {
                            i = i + 1;
                        }
                    }
                    string p = @"[].,，。;；》>、!！?？】｝}]";
                    if (floLineContentSize >= maxWidth && (i + 1) < textContent.Length && Regex.IsMatch(textContent[i + 1].ToString(), p))
                    {
                        strOutputContent += textContent[i + 1].ToString();
                        i = i + 1;
                    }
                    strOutputContent = strOutputContent.TrimEnd('r').TrimEnd('\\');// Remove(strOutputContent.Length - 1, 1);
                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, strOutputContent, textX, textY, 0);
                    contentByte.NewlineText();
                    linesCount++;
                    strOutputContent = nextNeedStr;
                    continue;
                }
            }
            if (linesCount <= maxLine)
            {
                contentByte.ShowText(strOutputContent);
            }
            contentByte.Stroke();
            contentByte.EndText();
        }

        public void PaintContent(string textColor, string text, string fontPath, float fontSize, float textLeading, float textSpacing, float x, float y, float height, float width, int align)
        {
            CMYK_Color cmyk_titleColor = new CMYK_Color().GetCMYK_Color(textColor);
            FontFactory.Register(fontPath);
            BaseFont bfChinese = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Paragraph para = new Paragraph(text, new iTextSharp.text.Font(bfChinese, fontSize));

            PdfContentByte cb = pdfWriter.DirectContent;
            cb.SetCMYKColorFill(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            cb.SetCMYKColorStroke(cmyk_titleColor.C, cmyk_titleColor.M, cmyk_titleColor.Y, cmyk_titleColor.K);
            ColumnText column = new ColumnText(cb);
            column.SetSimpleColumn(para, x, y, x + width, y + height, textLeading, align);
            //cb.Rectangle(x, y, width, height);
            //cb.Stroke();
            column.Go();
        }

        /// <summary>
        /// 填充背景色
        /// </summary>
        /// <param name="bgColor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void PaintBGColor(string bgColor, float x, float y, float width, float height)
        {
            CMYK_Color cmyk = new CMYK_Color().GetCMYK_Color(bgColor.ToUpper());
            PdfContentByte contentByte = pdfWriter.DirectContentUnder;
            contentByte.SetCMYKColorFill(cmyk.C, cmyk.M, cmyk.Y, cmyk.K);
            contentByte.SetCMYKColorStroke(cmyk.C, cmyk.M, cmyk.Y, cmyk.K);
            contentByte.Rectangle(x, y, width, height);
            contentByte.Fill();
            contentByte.Stroke();
            //contentByte.FillStroke();
        }

        //获取要打印的图片地址
        public string GetLocationUrl(string strImgUrl)
        {
            string strAsbUrl = AppDomain.CurrentDomain.BaseDirectory + strImgUrl;
            int intEditIndex = strAsbUrl.IndexOf("_edit.jpg");
            string strReturn = strAsbUrl.Substring(0, intEditIndex) + "_print.jpg";
            return strReturn;
        }

        public string GetEditLocationUrl(string strImgUrl)
        {
            string strAsbUrl = AppDomain.CurrentDomain.BaseDirectory + strImgUrl;

            return strAsbUrl;
        }
    }
}
