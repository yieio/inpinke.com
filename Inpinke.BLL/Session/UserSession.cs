using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;

namespace Inpinke.BLL.Session
{
    public class UserSession
    {
        public static Inpinke_User CurrentUser
        {
            get
            {              
                int id = 0;
                id = System.Web.HttpContext.Current.Session["CurrentUser"] == null ? 0 : (int)System.Web.HttpContext.Current.Session["CurrentUser"];
                Inpinke_User model = DBUserBLL.GetUserByID(id);
                return model;
            }
            set
            {
                if (value != null)
                {
                    int userID = ((Inpinke_User)value).ID;
                    System.Web.HttpContext.Current.Session["CurrentUser"] = userID;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["CurrentUser"] = null;
                }
            }
        }

        public static void ClearUserSession()
        {
            System.Web.HttpContext.Current.Session.Clear();
        }
    }
}
