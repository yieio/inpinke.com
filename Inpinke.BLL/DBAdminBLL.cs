using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model;
using Inpinke.Model.CustomClass;
using Helper;

namespace Inpinke.BLL
{
    public class DBAdminBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBAdminBLL));
        /// <summary>
        /// 根据管理员ID获取管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Inpinke_Admin GetAdminByID(int id)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Admins.Get(e => e.ID == id);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetAdminByID AdminID:{0},Error:{1}", id, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 验证用户输入的密码是否正确
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>验证成功时ResponseObj为Inpinke_User实例</returns>
        public static BaseResponse ValidateUser(string username, string password)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                MD5Encrypt md5 = new MD5Encrypt();
                password = md5.GetMD5FromString(password);
                Inpinke_Admin user = InpinkeDataContext.Instance.Inpinke_Admins.Get(e => e.UserName == username && e.Password == password);
                if (user != null)
                {
                    br.IsSuccess = true;
                    br.ResponseObj = user;
                    return br;
                }
                else
                {
                    br.IsSuccess = false;
                    return br;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("ValidateUser UserName:{0},Password:{1},Error:{2}", username, password, ex.ToString()));
                br.IsSuccess = false;
                return br;
            }
        }
    }
}
