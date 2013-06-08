using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Inpinke.BLL
{
    /// <summary>
    /// 友言单点登录相关方法
    /// </summary>
    public class UYanBLL
    {
        /// <summary>
        /// string Loginsrc = "http://api.uyan.cc?mode=des&uid=10998&uname=zhangsan&email=zhangsan@uyan.cc&uface=http://uyan.cc/img/zhangsan.jpg&ulink=http://uyan.cc&expire=3600&key=ojfejofowoejofe ";
        /// 获取des加密串
        /// </summary>
        /// <param name="Loginsrc"></param>
        /// <returns></returns>
        public static string GetMi(string Loginsrc)
        {
            string strRet = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Loginsrc);
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream resStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.Default;
                StreamReader readStream = new StreamReader(resStream, encode);
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    strRet = strRet + str;
                    count = readStream.Read(read, 0, 256);
                }
                resStream.Close();
            }
            catch (Exception e)
            {
                strRet = "";
            }
            return strRet;
        }
    }
}
