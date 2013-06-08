using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model;
using Inpinke.Model.Enum;
using Helper.UI;

namespace Inpinke.BLL
{
   public  class DBCouponBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBCartBLL));
        /// <summary>
        /// 根据ID获取优惠活动
        /// </summary>
        /// <param name="cartid"></param>
        /// <returns></returns>
        public static Inpinke_Coupon GetCouponByID(int couponid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Coupons.Get(e => e.ID == couponid&&e.Status==(int)RecordStatus.Nomral);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetCouponByID CouponID:{0},Error:{1}", couponid, ex.ToString()));
                return null;
            }
        }
       /// <summary>
       /// 获取用户优惠券
       /// </summary>
       /// <param name="userid"></param>
       /// <returns></returns>
        public static IList<Inpinke_Coupon_Code> GetUserCoupon(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Coupon_Codes.Where(e => e.UserID==userid).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetCouponByID UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }            
        }
       /// <summary>
       /// 分页获取用户优惠券
       /// </summary>
       /// <param name="pInfo"></param>
       /// <param name="userid"></param>
       /// <returns></returns>
        public static IList<Inpinke_Coupon_Code> GetUserCoupon(PageInfo pInfo, int userid)
        {
            try
            {
                var q = from t in InpinkeDataContext.Instance.Inpinke_Coupon_Codes
                        where t.UserID == userid
                        select t;
                pInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from inpinke_coupon_code where userid={0}", userid)).FirstOrDefault();
                return q.Skip(pInfo.Skip).Take(pInfo.PageSize).ToList();                
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetCouponByID UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
    }
}
