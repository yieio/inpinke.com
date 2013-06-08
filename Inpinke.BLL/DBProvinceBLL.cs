using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;
using log4net;

namespace Inpinke.BLL
{
    public class DBProvinceBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBProvinceBLL));

        public static string GetProvName(int provID)
        {
            string name = "";

            var model = InpinkeDataContext.Instance.Base_Provinces.Where(e => e.ProvID == provID).FirstOrDefault();
            if (model != null)
            {
                name = model.ProvName;
            }

            return name;
        }

        public static string GetCityName(int cityID)
        {
            string name = "";

            var model = InpinkeDataContext.Instance.Base_Cities.Where(e => e.CityID == cityID).FirstOrDefault();
            if (model != null)
            {
                name = model.CityName;
            }

            return name;
        }



        public static string GetAreaName(int areaid)
        {
            string name = "";

            var model = InpinkeDataContext.Instance.Base_Areas.Where(e => e.AreaID == areaid).FirstOrDefault();
            if (model != null)
            {
                name = model.AreaName;
            }

            return name;
        }

        public static int GetProvID(int cityID)
        {
            int provID = 0;

            var model = InpinkeDataContext.Instance.Base_Cities.Where(e => e.CityID == cityID).FirstOrDefault();
            if (model != null)
            {
                provID = model.ProvID;
            }

            return provID;
        }
    }
}
