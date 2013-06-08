using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace Helper.IO
{
    public class RARHelper
    {

        /// <summary>
        /// 利用 WinRAR 进行压缩
        /// </summary>
        /// <param name="workingDirectory">压缩和被压缩文件所在的目录</param>
        /// <param name="rarName">压缩后文件的名称（包括后缀）</param>
        /// <param name="fileName">将要被压缩文件的名称（包括后缀）</param>
        /// <returns>true 或 false。压缩成功返回 true，反之，false。</returns>
        public static bool WinRAR(string workingDirectory, string rarName, string fileName)
        {
            bool flag = false;
            string rarexe;       //WinRAR.exe 的完整路径

            string cmd;          //WinRAR 命令参数
            ProcessStartInfo startinfo;
            Process process;
            try
            {

                //rarexe = Server.MapPath(@"~/Tools/WinRAR.exe");
                rarexe = ConfigHelper.ReadConfig("RAR", "configuration/RarPath");
                if (rarexe == "" || !File.Exists(rarexe))
                {
                    rarexe = @"C:\Program Files\WinRAR\RAR.exe";
                    //rarexe = @"C:\Program Files (x86)\WinRAR\WinRAR.exe"; //39的winrar地址
                }

                if (!File.Exists(rarexe))
                {
                    throw new Exception("错误的rar.exe路径:" + rarexe);
                }
                else
                {
                    if (!Directory.Exists(workingDirectory))
                    {
                        Directory.CreateDirectory(workingDirectory);
                    }
                    //Directory.CreateDirectory(rarPath);
                    //压缩命令，相当于在要压缩的文件夹(path)上点右键 ->WinRAR->添加到压缩文件->输入压缩文件名(rarName)
                    cmd = string.Format("a  \"{0}\" \"{1}\" ", rarName, fileName);

                    startinfo = new ProcessStartInfo();
                    startinfo.FileName = rarexe;
                    startinfo.Arguments = cmd;                          //设置命令参数
                    startinfo.WindowStyle = ProcessWindowStyle.Hidden; //隐藏 WinRAR 窗口
                    startinfo.WorkingDirectory = workingDirectory;
                    process = new Process();
                    process.StartInfo = startinfo;
                    process.Start();
                    process.WaitForExit(); //无限期等待进程 winrar.exe 退出
                    if (process.HasExited)
                    {
                        flag = true;
                    }
                    process.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }


    }
}
