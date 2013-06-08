using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;

namespace Inpinke.BLL.Session
{
    public class AdminSession
    {
        public static Inpinke_Admin CurrentUser
        {
            get
            {
                int id = 0;
                id = System.Web.HttpContext.Current.Session["CurrentAdmin"] == null ? 0 : (int)System.Web.HttpContext.Current.Session["CurrentAdmin"];
                Inpinke_Admin model = DBAdminBLL.GetAdminByID(id);
                return model;
            }
            set
            {
                if (value != null)
                {
                    int adminID = ((Inpinke_Admin)value).ID;
                    System.Web.HttpContext.Current.Session["CurrentAdmin"] = adminID;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["CurrentAdmin"] = null;
                }
            }
        }
    }
}
