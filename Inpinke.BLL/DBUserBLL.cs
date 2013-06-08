using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;
using log4net;
using Helper;
using Inpinke.Model.CustomClass;
using Inpinke.Model.Enum;
using Helper.UI;
using Inpinke.Model.DataAccess;

namespace Inpinke.BLL
{
    public class DBUserBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBUserBLL));

        /// <summary>
        /// 添加新的用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse CreateUser(Inpinke_User model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                MD5Encrypt md5 = new MD5Encrypt();
                model.Password = md5.GetMD5FromString(model.Password);
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("CreateUser Email:{0},Password:{1},NickName:{2} Error:{3}", model.Email, model.Password, model.NickName, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "服务器异常，请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse UpdateUser(Inpinke_User model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                MD5Encrypt md5 = new MD5Encrypt();
                model.Password = md5.GetMD5FromString(model.Password);
                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("UpdateUser Email:{0},Password:{1},NickName:{2} Error:{3}", model.Email, model.Password, model.NickName, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "更新用户信息失败，请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 判断邮箱是否存在,存在时返回true，反之为false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userid">为0时忽略，大于0时表示不包括该所属用户</param>
        /// <returns>邮箱存在时返回true，反之为false</returns>
        public static BaseResponse CheckEmailIsExist(string email, int userid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = true;
            try
            {

                Inpinke_User user = InpinkeDataContext.Instance.Inpinke_Users.Get(e => e.Email == email && e.ID != userid);
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
                Logger.Error(string.Format("CheckEmailIsExist Email:{0},UserID:{1},Error:{2}", email, userid, ex.ToString()));
                return br;
            }

        }
        /// <summary>
        /// 验证用户输入的密码是否正确
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>验证成功时ResponseObj为Inpinke_User实例</returns>
        public static BaseResponse ValidateUser(string email, string password)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                MD5Encrypt md5 = new MD5Encrypt();
                password = md5.GetMD5FromString(password);
                Inpinke_User user = InpinkeDataContext.Instance.Inpinke_Users.Get(e => e.Email == email && e.Password == password);
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
                Logger.Error(string.Format("ValidateUser Email:{0},Password:{1},Error:{2}", email, password, ex.ToString()));
                br.IsSuccess = false;
                return br;
            }
        }
        /// <summary>
        /// 根据用ID获取用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Inpinke_User GetUserByID(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Users.Get(e => e.ID == userid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserByID UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据用户验证获取用户，用于通过邮箱重置密码
        /// </summary>
        /// <param name="validate"></param>
        /// <returns></returns>
        public static Inpinke_User GetUserByValidateCode(string validate)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Users.Get(e => e.ValidateCode == validate);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserByID Validate:{0},Error:{1}", validate, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static IList<Inpinke_User> GetUserByQueryModel(PageInfo pInfo, UserQueryModels query)
        {
            try
            {
                IList<Inpinke_User> list = new List<Inpinke_User>();
                string field = " U.* ";
                string table = " Inpinke_User U ";
                string where = FormatQModel.FormatQueryModel<UserQueryModels>(query);
                string orderby = " U.ID ";

                string countQ = PagerHelper.GetCountSQL(table, where);
                string qSql = PagerHelper.GetPager(table, pInfo.Skip, pInfo.PageSize, field, orderby, where);
                pInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(countQ).FirstOrDefault();
                list = InpinkeDataContext.Instance.ExecuteQuery<Inpinke_User>(qSql).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserByQueryModel Error:{0}", ex.ToString()));
                return null;
            }
        }


         
    }
}
