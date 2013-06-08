using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Helper.IO
{
    public class FileHelper
    {
        public static void Write(string path, string text)
        {
            StreamWriter sw = null;
            try
            {
                //实例化一个StreamWriter 
                sw = new StreamWriter(path, false, Encoding.UTF8);
                //开始写入  
                sw.Write(text);
                //清空缓冲区  
                sw.Flush();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (sw != null)
                {
                    //关闭流  
                    sw.Close();
                }
            }


        }
    }
}
