using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;
using log4net;
using Inpinke.Model.Enum;
using Inpinke.Model.CustomClass;
using Helper.UI;

namespace Inpinke.BLL
{
    public class DBAddressBLL
    {

        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBAddressBLL));
        /// <summary>
        /// 获取用户收货人列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_User_Address> GetUserAddress(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_User_Addresses.Where(e => e.UserID == userid && e.Status == (int)RecordStatus.Nomral).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserAddress UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 分页获取收货人列表
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_User_Address> GetUserAddress(PageInfo pInfo, int userid)
        {
            try
            {
                var q = from t in InpinkeDataContext.Instance.Inpinke_User_Addresses
                        where t.UserID == userid && t.Status == (int)RecordStatus.Nomral
                        select t;
                pInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_User_Address where userid={0} and status={1}", userid, (int)RecordStatus.Nomral)).FirstOrDefault();

                return q.Skip(pInfo.Skip).Take(pInfo.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserAddress UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }

        /// <summary>
        /// 根据ID获取地址信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Inpinke_User_Address GetAddressByID(int id)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_User_Addresses.Get(e => e.ID == id && e.Status == (int)RecordStatus.Nomral);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserAddress ID:{0},Error:{1}", id, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 更新用户收货人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse UpdateUserAddress(Inpinke_User_Address model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                model.ProvName = DBProvinceBLL.GetProvName(model.ProvID);
                model.CityName = DBProvinceBLL.GetCityName(model.CityID);
                model.AreaName = DBProvinceBLL.GetAreaName(model.AreaID);
                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.ResponseObj = model;
                return br;
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "更新收货人信息失败，请稍后再试";
                Logger.Error(string.Format("UpdateUserAddress AddressID:{0},Error:{1}", model.ID, ex.ToString()));
                return br;
            }
        }
        /// <summary>
        /// 添加用户收货地址信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse AddUserAddress(Inpinke_User_Address model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Status = (int)RecordStatus.Nomral;
                model.ProvName = DBProvinceBLL.GetProvName(model.ProvID);
                model.CityName = DBProvinceBLL.GetCityName(model.CityID);
                model.AreaName = DBProvinceBLL.GetAreaName(model.AreaID);
                model.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.ResponseObj = model;
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddUserAddress UserID:{0},Error:{1}", model.UserID, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "保存收货人信息失败，请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 设置默认收货人
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static BaseResponse SetDefualtAddress(int id, int userid)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                string upSql = string.Format("update Inpinke_User_Address set isdefault=0 where userid={0} and isdefault=1", userid);
                int rcount = InpinkeDataContext.Instance.ExecuteCommand(upSql);
                upSql = string.Format("update Inpinke_User_Address set isdefault=1 where id={0}", id);
                rcount = InpinkeDataContext.Instance.ExecuteCommand(upSql);
                if (rcount == 1)
                {
                    br.IsSuccess = true;
                }
                else
                {
                    br.IsSuccess = false;
                    br.Message = "设置默认收货人失败";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("SetDefualtAddress ID:{0}, UserID:{1} ,Error:{2}", id, userid, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "设置默认收货人失败,请稍后再试";
            }
            return br;
        }
        /// <summary>
        /// 删除某个收货人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BaseResponse DeleteAddress(int id)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                string upSql = string.Format("update Inpinke_User_Address set status={1} where id={0}", id, (int)RecordStatus.Delete);
                int rcount = InpinkeDataContext.Instance.ExecuteCommand(upSql);
                if (rcount == 1)
                {
                    br.IsSuccess = true;
                }
                else
                {
                    br.IsSuccess = false;
                    br.Message = "删除收货人失败";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("DeleteAddress ID:{0},Error:{1}", id, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "删除收货人失败,请稍后再试";
            }
            return br;
        }
        /// <summary>
        /// 获取用户收货地址数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserAddressCount(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_User_Address where userid={0} and status={1}", userid, (int)RecordStatus.Nomral)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserAddressCount UserID:{0},Error:{1}", userid, ex.ToString()));
                return 0;
            }
        }
    }
}
