using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inpinke.Helper;

namespace inpinke.com.Views
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EmailHelper.SendEmail("yieio@qq.com", "呵呵测试的，邮件，好开心!", "印品客测试邮件");
            Response.Redirect("/home");
        }
    }
}