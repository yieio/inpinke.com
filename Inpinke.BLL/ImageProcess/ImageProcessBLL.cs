using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using log4net;
using Inpinke.Model.CustomClass;
using System.Text.RegularExpressions;

namespace Inpinke.BLL.ImageProcess
{
    public class ImageProcessBLL
    {

        public static readonly ILog Logger = LogManager.GetLogger(typeof(ImageProcessBLL));
        /// <summary>
        /// 定长宽缩放裁剪图片
        /// </summary>
        /// <param name="bitSource"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="strSavePath"></param>
        /// <returns></returns>
        public static bool CreateScaleImage(Bitmap bitSource, int width, int height, string strSavePath, bool isScale)
        {
            try
            {
                Bitmap bitmap = new Bitmap(bitSource);
                int outWidth, outHeight;
                int sx, sy, ex, ey;
                float widthScale = (float)bitmap.Width / ((float)width);
                float heightScale = (float)bitmap.Height / ((float)height);
                if (widthScale > heightScale)
                {
                    outHeight = height;
                    outWidth = Convert.ToInt32(bitmap.Width / heightScale);
                    sx = (outWidth - width) / 2; ;
                    sy = 0;
                    ex = sx + width;
                    ey = outHeight;
                }
                else
                {
                    outWidth = width;
                    outHeight = Convert.ToInt32(bitmap.Height / widthScale);
                    sx = 0;
                    sy = (outHeight - height) / 2;
                    ex = outWidth;
                    ey = sy + height;
                }

                Bitmap output1 = new Bitmap(outWidth, outHeight, PixelFormat.Format24bppRgb);
                Bitmap output = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(output1);
                Rectangle rectangle2 = new Rectangle(0, 0, outWidth, outHeight);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                EncoderParameter p;
                EncoderParameters ps;
                ps = new EncoderParameters(1);
                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                ps.Param[0] = p;

                g.DrawImage(bitmap, rectangle2, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                if (isScale) //只缩放
                {
                    output1.Save(strSavePath, GetCodecInfo("image/jpeg"), ps);
                }
                else //缩放并裁剪
                {
                    g = Graphics.FromImage(output);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(output1, new Rectangle(0, 0, width, height), sx, sy, width, height, GraphicsUnit.Pixel);
                    output.Save(strSavePath, GetCodecInfo("image/jpeg"), ps);
                }
                bitmap.Dispose();
                output1.Dispose();
                output.Dispose();
                g.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("CreateScaleImage IsSCale:{0} Error:{1}", isScale, ex.ToString()));
                return false;
            }
        }
        /// <summary>
        /// 定一边生成图片
        /// </summary>
        /// <param name="bitSource"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="maxVal">最大值</param>
        /// <param name="strSavePath"></param>
        /// <returns></returns>
        public static bool CreateStaticScaleImage(Bitmap bitSource, int width, int height,int maxVal, string strSavePath)
        {
            try
            {
                Bitmap bitmap = new Bitmap(bitSource);
                int outWidth, outHeight;
                int sx, sy, ex, ey;
                float widthScale = (float)bitmap.Width / ((float)width);
                float heightScale = (float)bitmap.Height / ((float)height);
                bool isScale = false;
                if (width < height)
                {
                    outHeight = height;
                    outWidth = Convert.ToInt32(bitmap.Width / heightScale);
                    sx = (outWidth - width) / 2; ;
                    sy = 0;
                    ex = sx + width;
                    ey = outHeight;
                    isScale = outWidth < maxVal;
                    width = maxVal;
                }
                else
                {
                    outWidth = width;
                    outHeight = Convert.ToInt32(bitmap.Height / widthScale);
                    sx = 0;
                    sy = (outHeight - height) / 2;
                    ex = outWidth;
                    ey = sy + height;
                    isScale = outHeight < maxVal;
                    height = maxVal;
                }

                Bitmap output1 = new Bitmap(outWidth, outHeight, PixelFormat.Format24bppRgb);
                Bitmap output = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(output1);
                Rectangle rectangle2 = new Rectangle(0, 0, outWidth, outHeight);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                EncoderParameter p;
                EncoderParameters ps;
                ps = new EncoderParameters(1);
                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                ps.Param[0] = p;

                g.DrawImage(bitmap, rectangle2, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                if (isScale) //只缩放
                {
                    output1.Save(strSavePath, GetCodecInfo("image/jpeg"), ps);
                }
                else //缩放并裁剪
                {
                    g = Graphics.FromImage(output);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(output1, new Rectangle(0, 0, width, height), sx, sy, width, height, GraphicsUnit.Pixel);
                    output.Save(strSavePath, GetCodecInfo("image/jpeg"), ps);
                }
                bitmap.Dispose();
                output1.Dispose();
                output.Dispose();
                g.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("CreateStaticScaleImage Error:{0}",ex.ToString()));
                return false;
            }
        }
        /// <summary>
        /// 获取图片格式编码
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }
        /// <summary>
        /// 生成页面预览
        /// </summary>
        /// <param name="pObj"></param>
        /// <param name="strSavePath"></param>
        /// <returns></returns>
        public static bool CreatePageViewImage(PageDataObj pObj, string strSavePath, int orgWidth, int orgHeight, int outWidth, int outHeight, ref bool istxtOverflow)
        {
            Bitmap bitmapOutput = new Bitmap(orgWidth, orgHeight);
            Graphics g = Graphics.FromImage(bitmapOutput);
            Color c = ColorTranslator.FromHtml(pObj.bgcolor);
            g.Clear(c);


            EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            EncoderParameters ps = new EncoderParameters(1);
            ps.Param[0] = p;
            if (pObj.image != null)
            {
                foreach (PageImage item in pObj.image)
                {
                    float wScale = item.orgwidth / (float)item.conwidth;
                    float hScale = item.orgheight / (float)item.conheight;

                    float floZoom = wScale > hScale ? hScale : wScale;
                    int intZoomWidth = Convert.ToInt32(item.conwidth * floZoom);
                    int intZoomHeight = Convert.ToInt32(item.conheight * floZoom);
                    int intOutputWidth = Convert.ToInt32(item.conwidth);
                    int intOutputHeight = Convert.ToInt32(item.conheight);
                    int startX = Convert.ToInt32(item.x * floZoom) * -1;
                    int startY = Convert.ToInt32(item.y * floZoom) * -1;

                    string strImageUrl = GetEditLocationUrl(item.src);
                    Bitmap bitmapSource = new Bitmap(strImageUrl);
                    Bitmap bitmapZoom = new Bitmap(intZoomWidth, intZoomHeight);
                    Bitmap bitmapCut = new Bitmap(intOutputWidth, intOutputHeight);
                    CutPhoto(ref bitmapZoom, bitmapSource, new System.Drawing.Rectangle(0, 0, intZoomWidth, intZoomHeight), startX, startY, intZoomWidth, intZoomHeight);
                    CutPhoto(ref bitmapCut, bitmapZoom, new System.Drawing.Rectangle(0, 0, intOutputWidth, intOutputHeight), 0, 0, intZoomWidth, intZoomHeight);
                    g.DrawImage(bitmapCut, item.conx, item.cony, intOutputWidth, intOutputHeight);
                    g.Dispose();
                    bitmapSource.Dispose();
                    bitmapCut.Dispose();
                }
            }

            AddText2PageView(bitmapOutput, pObj, ref istxtOverflow);
            Bitmap outImage = new Bitmap(outWidth, outHeight);
            CutPhoto(ref outImage, bitmapOutput, new System.Drawing.Rectangle(0, 0, outWidth, outHeight), 0, 0, orgWidth, orgHeight);
            outImage.Save(strSavePath, GetCodecInfo("image/jpeg"), ps);
          
            outImage.Dispose();
            return true;
        }

        /// <summary>
        /// 添加文字到预览图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="pdata"></param>
        /// <param name="isTxtOverflow">文字是否溢出</param>
        public static void AddText2PageView(Bitmap bitmap, PageDataObj pdata, ref bool isTxtOverflow)
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            foreach (PageText t in pdata.text)
            {
                System.Drawing.Font font = new System.Drawing.Font("方正兰亭黑_GBK", t.fontsize, GraphicsUnit.Pixel);

                float x = t.conx;
                float y = t.cony;
                if (!string.IsNullOrEmpty(t.content))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = t.textalign == "left" ? StringAlignment.Near : t.textalign == "center" ? StringAlignment.Center : StringAlignment.Far;
                    string printT = "";
                    int len = t.content.Length;
                    Brush b = t.color.ToLower() == "#ffffff" ? Brushes.White : Brushes.Black;
                    int line = 0;
                    if (t.issingle == "true")
                    {
                        SizeF fsize2 = g.MeasureString(t.content, font);
                        isTxtOverflow = fsize2.Width > t.conwidth;
                        g.DrawString(t.content, font, b, new RectangleF(x, y, t.conwidth, t.conheight), sf);
                        continue;
                    }
                    string p = @"[].,，。;；》>、!！?？】｝}]";
                    for (int i = 0; i < len; i++)
                    {
                        printT += t.content[i];
                        SizeF fsize = g.MeasureString(printT, font);
                        if (i + 1 < len)
                        {
                            fsize = g.MeasureString(printT + t.content[i + 1], font);
                            if (Regex.IsMatch(t.content[i + 1].ToString(), p))
                            {
                                printT += t.content[i + 1];
                                i++;
                            }
                        }
                        if (fsize.Width > t.conwidth || printT.Contains("\n"))
                        {
                            if (printT.Contains("\n"))
                            {
                                printT = printT.Replace("\n", "");
                                fsize.Height = fsize.Height / 2;
                            }
                            g.DrawString(printT, font, b, new RectangleF(x, y, t.conwidth, fsize.Height), sf);
                            y += fsize.Height + 5;
                            printT = "";
                            line++;
                        }
                        if (y + fsize.Height > t.cony + t.conheight)
                        {
                            isTxtOverflow = true;
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(printT))
                    {
                        SizeF fsize1 = g.MeasureString(printT, font);
                        g.DrawString(printT, font, b, new RectangleF(x, y, t.conwidth, fsize1.Height), sf);
                    }
                    isTxtOverflow = false;
                    //SizeF fsize = g.MeasureString(t.content, font,new SizeF(t.conwidth,t.conheight),sf,);                   
                    //x = t.textalign == "left" ? x : t.textalign == "center" ? x + (t.conwidth - fsize.Width) / 2 : x + (t.conwidth - fsize.Width);
                    //g.DrawString(t.content, font, t.color.ToLower() == "#ffffff" ? Brushes.White : Brushes.Black, new RectangleF(x, y, t.conwidth, t.conheight), sf);
                }
            }
            g.Dispose();
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
        public static void CutPhoto(ref Bitmap bitmapCut, Bitmap bitmapSource, System.Drawing.Rectangle outRectangle, float srcX, float srcY, float srcWidth, float srcHeight)
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

        public static string GetEditLocationUrl(string strImgUrl)
        {
            string strAsbUrl = AppDomain.CurrentDomain.BaseDirectory + strImgUrl;

            return strAsbUrl;
        }
    }
}
