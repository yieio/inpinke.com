using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;

namespace Helper
{
    public class ZipHelper
    {
        #region 加压解压方法
        /// <summary>
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹夹路径</param>
        /// <param name="zipFilePath">生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名+.zip</param>
        /// <param name="err">出错信息</param>
        /// <returns>是否压缩成功</returns>

        public static bool ZipFile(MemoryStream stream, string strName)
        {
            HttpResponse contextResponse = null;
            ZipOutputStream s = null;
            try
            {
                FileStream streamOUT = new FileStream(strName, FileMode.OpenOrCreate);
                using (s = new ZipOutputStream(streamOUT))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];

                    ZipEntry entry = new ZipEntry(strName);
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    int sourceBytes;
                    do
                    {
                        sourceBytes = stream.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);


                }
                contextResponse = HttpContext.Current.Response;
                contextResponse.Clear();
                contextResponse.Buffer = true;
                contextResponse.Charset = "GB2312"; //设置了类型为中文防止乱码的出现 
                contextResponse.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", strName)); //定义输出文件和文件名 
                contextResponse.AppendHeader("Content-Length", streamOUT.Length.ToString());
                contextResponse.ContentEncoding = Encoding.Default;
                contextResponse.ContentType = "application/x-zip-compressed"; //设置输出文件类型为excel文件。  
                byte[] buffer2 = new byte[(int)s.Length];
                s.Read(buffer2, 0, buffer2.Length);
                contextResponse.BinaryWrite(buffer2);
            }
             //   contextResponse.OutputStream.Write(buffer1, 0, sourceBytes1);
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                contextResponse.Flush();
                contextResponse.End();
                s.Finish();
                s.Close();
            }
            return true;
        }
        /// <summary>
        /// 功能：解压zip格式的文件。
        /// </summary>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>
        /// <param name="err">出错信息</param>
        /// <returns>解压是否成功</returns>
        public bool UnZipFile(string zipFilePath, string unZipDir, out string err)
        {
            err = "";
            if (zipFilePath == string.Empty)
            {
                err = "压缩文件不能为空！";
                return false;
            }
            if (!File.Exists(zipFilePath))
            {
                err = "压缩文件不存在！";
                return false;
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("\\"))
                unZipDir += "\\";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith("\\"))
                            directoryName += "\\";
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }//while
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }//解压结束
        #endregion
    }
}
