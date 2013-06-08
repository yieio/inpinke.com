using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.IO;

namespace Helper
{
    /// <summary>
    /// 验证码模块
    /// </summary>
    public class CreateImage
    {
        public static void DrawImage()
        {
            CreateImage img = new CreateImage();
            HttpContext.Current.Session["CheckCode"] = img.RndNum(4);
            img.CreateImages(HttpContext.Current.Session["CheckCode"].ToString());
        }

        public static void DrawImageByKey(string key)
        {
            CreateImage img = new CreateImage();
            HttpContext.Current.Session["CheckCode" + key] = img.RndNum(4);
            img.CreateImages(HttpContext.Current.Session["CheckCode" + key].ToString());
        }

        public static void DrawStaticImage()
        {
            CreateImage img = new CreateImage();
            HttpContext.Current.Session["CheckCode"] = 1111;
            img.CreateImages(HttpContext.Current.Session["CheckCode"].ToString());
        }

        /// <summary>
        /// 生成指定的验证码
        /// </summary>
        /// <param name="checkCode"></param>
        public static void DrawImage(string checkCode)
        {
            CreateImage img = new CreateImage();
            HttpContext.Current.Session["CheckCode"] = checkCode;
            img.CreateImages(checkCode);

        }

        /// <summary>
        /// 生成验证图片
        /// </summary>
        /// <param name="checkCode">验证字符</param>
        private void CreateImages(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 16);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 27);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体 
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            Random rand = new Random();
            //随机输出噪点
            for (int i = 0; i < 50; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);

                Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * 12), ii);
            }
            //画一个边框
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            //Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

        /// <summary>
        /// 生成随机的字母
        /// </summary>
        /// <param name="VcodeNum">生成字母的个数</param>
        /// <returns>string</returns>
        private string RndNum(int VcodeNum)
        {
            string Vchar = "0,1,2,3,4,5,6,7,8,9";
            string[] VcArray = Vchar.Split(',');
            string VNum = ""; //由于字符串很短，就不用StringBuilder了
            int temp = -1; //记录上次随机数值，尽量避免生产几个一样的随机数

            //采用一个简单的算法以保证生成随机数的不同
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(VcArray.Length);
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);
                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;
        }


        public static  string RndString(int VcodeNum)
        {
            string abcChar = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string Vchar = "0,1,2,3,4,5,6,7,8,9";
            Vchar = Vchar + "," + abcChar + "," + abcChar.ToUpper();
            string[] VcArray = Vchar.Split(',');
            string VNum = ""; //由于字符串很短，就不用StringBuilder了
            int temp = -1; //记录上次随机数值，尽量避免生产几个一样的随机数

            //采用一个简单的算法以保证生成随机数的不同
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(VcArray.Length);
                if (temp != -1 && temp == t)
                {
                    return RndString(VcodeNum);
                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;
        }
        /// <summary>
        /// 生成不重复的纯数字0-9随机码
        /// </summary>
        /// <param name="vCodeNum">生成字母的个数</param>
        /// <returns></returns>
        public static string DiffRandromNum(int vCodeNum)
        {
            string rndNum = "";
            Random random = new Random();
            for (int i = 0; i < vCodeNum; i++)
            {
                rndNum += random.Next(0, 9);
            }
            return rndNum;
        }
    }
}
