using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net.Config;
using log4net;

namespace inpinke.com
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(MvcApplication));

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
             
            //routes.MapRoute(
            //    "HtmlDefault", // Route name
            //    "{controller}/{action}.html", // URL with parameters
            //    new { controller = "home", action = "index", id = UrlParameter.Optional } // Parameter defaults
            //);
            //routes.MapRoute(
            //              "IDHtmlDefault", // Route name
            //              "{controller}/{action}/{id}.html", // URL with parameters
            //              new { controller = "home", action = "index", id = UrlParameter.Optional } // Parameter defaults
            //          );
            routes.MapRoute(
                           "NullDefault", // Route name
                           "{controller}/{action}/{id}", // URL with parameters
                           new { controller = "home", action = "index", id = UrlParameter.Optional } // Parameter defaults
                       );

        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();//log4net
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}