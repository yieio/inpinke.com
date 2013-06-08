using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model;
using Helper;

namespace Inpinke.BLL
{
    public class DBImageScaleBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBImageScaleBLL));

        public static IList<Inpinke_ImageScale> GetAllImageScaleSize()
        {
            try
            {
                string key = "GetAllImageScaleSize";
                object cache = CacheHelper.GetCache(key);
                if (cache == null)
                {
                    var q = from t in InpinkeDataContext.Instance.Inpinke_ImageScales
                            select t;
                    IList<Inpinke_ImageScale> list = q.ToList();
                    CacheHelper.SetCache(key, q.ToList());
                    return list;
                }
                else
                {
                    return  cache as IList<Inpinke_ImageScale>;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetAllImageScaleSize Error:{0}", ex.ToString()));
                return null;
            }
 
        }
    }
}
