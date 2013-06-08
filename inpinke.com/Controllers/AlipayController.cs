using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.BLL.Filters;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.Helper.Alipay;
using System.Collections.Specialized;
using Inpinke.Model.Enum;
using Inpinke.Model.CustomClass;
using log4net;

namespace inpinke.com.Controllers
{
    public class AlipayController : Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(AlipayController));
        //
        // GET: /Alipay/
        [UserFilter]
        public ActionResult Index(int orderid)
        {
            Inpinke_Order model = DBOrderBLL.GetOrderByID(orderid);
            if (model == null)
            {
                ViewBag.Msg = "对不起，没有找到您要支付的订单，请尝试重新登录下单。";
                return View("error");
            }

            //必填参数//
            //请与贵网站订单系统中的唯一订单号匹配
            string out_trade_no = model.OrderCode;
            //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string subject = "Inpinke-印品客印品";
            //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
            string body = "您在Inpinke-印品客网站定制印刷的" + model.Inpinke_Order_Products.Count() + "件印品";
            //订单总金额，显示在支付宝收银台里的“应付总额”里
            string total_fee = model.TotalPrice.ToString();

            //扩展功能参数——默认支付方式//

            //默认支付方式，代码见“即时到帐接口”技术文档
            string paymethod = "";
            //默认网银代号，代号列表见“即时到帐接口”技术文档“附录”→“银行列表”
            string defaultbank = "";

            //扩展功能参数——防钓鱼//

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
            string exter_invoke_ip = "";
            //注意：
            //请慎重选择是否开启防钓鱼功能
            //exter_invoke_ip、anti_phishing_key一旦被设置过，那么它们就会成为必填参数
            //建议使用POST方式请求数据
            //示例：
            //exter_invoke_ip = "";
            //Service aliQuery_timestamp = new Service();
            //anti_phishing_key = aliQuery_timestamp.Query_timestamp();               //获取防钓鱼时间戳函数

            //扩展功能参数——其他//

            //商品展示地址，要用http:// 格式的完整路径，不允许加?id=123这类自定义参数
            string show_url = AlipayConfig.Show_Url;
            //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
            string extra_common_param = "";
            //默认买家支付宝账号
            string buyer_email = "";

            //扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)//

            //提成类型，该值为固定值：10，不需要修改
            string royalty_type = "";
            //提成信息集
            string royalty_parameters = "";
            //注意：
            //与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
            //各分润金额的总和须小于等于total_fee
            //提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
            //示例：
            //royalty_type = "10";
            //royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二";
            //构造即时到帐接口表单提交HTML数据，无需修改
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            AlipayService ali = new AlipayService();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            return Content(sHtmlText);
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
        /// <summary>
        /// 阿里支付成功同步回调地址
        /// </summary>
        /// <returns></returns>
        public ActionResult CallBack()
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                    string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                    string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                    string total_fee = Request.QueryString["total_fee"];            //获取总金额
                    string subject = Request.QueryString["subject"];                //商品名称、订单名称
                    string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];      //交易状态

                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        BaseResponse br = DBOrderBLL.OrderPaySuccess(order_no, decimal.Parse(total_fee), buyer_email);
                        if (br.IsSuccess)
                        {
                            return RedirectToAction("success", "order", new { ordercode = order_no });
                        }
                        else
                        {
                            ViewBag.Msg = "对不起未能成功处理您的订单[" + order_no + "]，原因是：" + br.Message;
                            return View("error");
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "对不起您的订单[" + order_no + "]支付失败了，交易状态：" + trade_status;
                        Logger.Info(ViewBag.Msg);
                        return View("error");
                    }
                }
                else//验证失败
                {
                    ViewBag.Msg = "对不起您的订单[" + Request.QueryString["trade_no"] + "]支付失败了，支付返回数据异常";
                    Logger.Info(ViewBag.Msg);
                    return View("error");
                }
            }
            else
            {
                ViewBag.Msg = "对不起您的订单[" + Request.QueryString["trade_no"] + "]订单支付失败了，支付返回参数为空";
                Logger.Info(ViewBag.Msg);
                return View("error");
            }
        }
        /// <summary>
        /// 阿里异步对账地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Notify()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                    string trade_no = Request.Form["trade_no"];         //支付宝交易号
                    string order_no = Request.Form["out_trade_no"];     //获取订单号
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string trade_status = Request.Form["trade_status"]; //交易状态

                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        BaseResponse br = DBOrderBLL.OrderPaySuccess(order_no, decimal.Parse(total_fee), buyer_email);
                        Logger.Info("Notify-1收到alipay的通知更新订单：" + order_no + "交易状态：" + trade_status);
                    }
                    else
                    {
                        Logger.Info("Notify-2收到alipay的通知更新订单：" + order_no + "交易状态：" + trade_status);
                    }
                    return Content("success");  //请不要修改或删除
                }
                else//验证失败
                {
                    return Content("fail");
                }
            }
            else
            {
                Logger.Info("Notify-3收到alipay的通知更新订单参数为空");
                return Content("无通知参数");
            }
        }

    }
}
