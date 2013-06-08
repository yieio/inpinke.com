using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model;
using Inpinke.Model.Enum;

namespace Inpinke.BLL
{
    public class DBProductBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBProductBLL));
        /// <summary>
        /// 根据产品ID获取产品信息
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static Inpinke_Product GetProductByID(int prodid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Products.Get(e => e.ID == prodid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetProductByID ProductID:{0},Error:{1}", prodid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取某产品的附赠品
        /// </summary>
        /// <param name="prodid">-1时获取所有附赠品</param>
        /// <returns></returns>
        public static IList<Inpinke_Product> GetPlusProduct(int prodid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Products.Where(e => (e.PlusTo == prodid || (prodid == -1 && e.PlusTo != 0)) && e.Status == (int)RecordStatus.Nomral).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetPlusProduct ProductID:{0},Error:{1}", prodid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取指定类别的赠品
        /// </summary>
        /// <param name="prodid"></param>
        /// <param name="prodType"></param>
        /// <returns></returns>
        public static IList<Inpinke_Product> GetPlusProduct(int prodid, ProductType prodType)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Products.Where(e => e.PlusTo == prodid && e.ProductType == (int)prodType && e.Status == (int)RecordStatus.Nomral).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetPlusProduct ProductID:{0},ProductType:{1},Error:{2}", prodid, (int)prodType, ex.ToString()));
                return null;
            }
        }
    }
}
