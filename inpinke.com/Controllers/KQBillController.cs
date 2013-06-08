using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using log4net;
using Inpinke.Model.CustomClass;
using Inpinke.BLL;

namespace inpinke.com.Controllers
{
    public class KQBillController : Controller
    {
        //
        // GET: /KQBill/

        public ActionResult Index()
        {
            return View();
        }

        #region 参数说明
        //人民币网关账号，该账号为11位人民币网关商户编号+01,该参数必填。
        public string merchantAcctId = "1002185531301";
        //编码方式，1代表 UTF-8; 2 代表 GBK; 3代表 GB2312 默认为1,该参数必填。
        public string inputCharset = "1";
        //接收支付结果的页面地址，该参数一般置为空即可。
        public string pageUrl = "";
        //服务器接收支付结果的后台地址，该参数务必填写，不能为空。
        public string bgUrl = "http://inpinke.com/kqbill/receive";
        //网关版本，固定值：v2.0,该参数必填。
        public string version = "v2.0";
        //语言种类，1代表中文显示，2代表英文显示。默认为1,该参数必填。
        public string language = "1";
        //签名类型,该值为4，代表PKI加密方式,该参数必填。
        public string signType = "4";
        //支付人姓名,可以为空。
        public string payerName = "佚名";
        //支付人联系类型，1 代表电子邮件方式；2 代表手机联系方式。可以为空。
        public string payerContactType = "1";
        //支付人联系方式，与payerContactType设置对应，payerContactType为1，则填写邮箱地址；payerContactType为2，则填写手机号码。可以为空。
        public string payerContact = "";
        //商户订单号，以下采用时间来定义订单号，商户可以根据自己订单号的定义规则来定义该值，不能为空。
        public string orderId = DateTime.Now.ToString("yyyyMMddHHmmss");
        //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试。该参数必填。
        public string orderAmount = "1";
        //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101，不能为空。
        public string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        //商品名称，可以为空。
        public string productName = "membook照片书";
        //商品数量，可以为空。
        public string productNum = "";
        //商品代码，可以为空。
        public string productId = "";
        //商品描述，可以为空。
        public string productDesc = "此商品为用户在Membook上DIY的相片书";
        //扩展字段1，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
        public string ext1 = "";
        //扩展自段2，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
        public string ext2 = "";
        //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10，必填。
        public string payType = "00";
        //银行代码，如果payType为00，该值可以为空；如果payType为10，该值必须填写，具体请参考银行列表。
        public string bankId = "";
        //同一订单禁止重复提交标志，实物购物车填1，虚拟产品用0。1代表只能提交一次，0代表在支付不成功情况下可以再提交。可为空。
        public string redoFlag = "0";
        //快钱合作伙伴的帐户号，即商户编号，可为空。
        public string pid = "10021855313";
        // signMsg 签名字符串 不可空，生成加密签名串
        public string signMsg = "";
        #endregion
        public ActionResult Send()
        {
            //orderId = Request["OrderID"];
            //IOrderService orderService = new OrderService();
            //IBookService bookService = new BookService();
            //OrderBM bm = orderService.GetOrder(int.Parse(orderId));
            //if (bm == null)
            //{
            //    return View("Receive");
            //}
            //productName = Request["ProductName"];
            //orderAmount = (bm.TotalMoney * 100).ToString();
            //orderId = bm.strOrderCode;
            //payerName = bm.strUserName;
            //payerContact.payerContactType = "2";
            //payerContact = bm.strPhone;



            //拼接字符串
            string signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset);
            signMsgVal = appendParam(signMsgVal, "pageUrl", pageUrl);
            signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "payerName", payerName);
            signMsgVal = appendParam(signMsgVal, "payerContactType", payerContactType);
            signMsgVal = appendParam(signMsgVal, "payerContact", payerContact);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "productName", productName);
            signMsgVal = appendParam(signMsgVal, "productNum", productNum);
            signMsgVal = appendParam(signMsgVal, "productId", productId);
            signMsgVal = appendParam(signMsgVal, "productDesc", productDesc);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag);
            signMsgVal = appendParam(signMsgVal, "pid", pid);

            ///PKI加密
            ///编码方式UTF-8 GB2312  用户可以根据自己系统的编码选择对应的加密方式
            ///byte[] OriginalByte=Encoding.GetEncoding("GB2312").GetBytes(OriginalString);
            log.Info(signMsgVal);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);

            X509Certificate2 cert = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + "Content\\KQBill\\99bill-rsa.pfx", "inpinke112886", X509KeyStorageFlags.MachineKeySet);

            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PrivateKey;
            RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
            byte[] result;
            f.SetHashAlgorithm("SHA1");
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(bytes);
            log.Info(result.ToString());
            signMsg = System.Convert.ToBase64String(f.CreateSignature(result)).ToString();
            //人民币网关账号，该账号为11位人民币网关商户编号+01,该参数必填。
            ViewData["merchantAcctId"] = merchantAcctId;
            //编码方式，1代表 UTF-8; 2 代表 GBK; 3代表 GB2312 默认为1,该参数必填。
            ViewData["inputCharset"] = inputCharset;
            //接收支付结果的页面地址，该参数一般置为空即可。
            ViewData["pageUrl"] = pageUrl;
            //服务器接收支付结果的后台地址，该参数务必填写，不能为空。
            ViewData["bgUrl"] = bgUrl;
            //网关版本，固定值：v2.0,该参数必填。
            ViewData["version"] = version;
            //语言种类，1代表中文显示，2代表英文显示。默认为1,该参数必填。
            ViewData["language"] = language;
            //签名类型,该值为4，代表PKI加密方式,该参数必填。
            ViewData["signType"] = signType;
            //支付人姓名,可以为空。
            ViewData["payerName"] = payerName;
            //支付人联系类型，1 代表电子邮件方式；2 代表手机联系方式。可以为空。
            ViewData["payerContactType"] = payerContactType;
            //支付人联系方式，与payerContactType设置对应，payerContactType为1，则填写邮箱地址；payerContactType为2，则填写手机号码。可以为空。
            ViewData["payerContact"] = payerContact;
            //商户订单号，以下采用时间来定义订单号，商户可以根据自己订单号的定义规则来定义该值，不能为空。
            ViewData["orderId"] = orderId;
            //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试。该参数必填。
            ViewData["orderAmount"] = orderAmount;
            //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101，不能为空。
            ViewData["orderTime"] = orderTime;
            //商品名称，可以为空。
            ViewData["productName"] = productName;
            //商品数量，可以为空。
            ViewData["productNum"] = productNum;
            //商品代码，可以为空。
            ViewData["productId"] = productId;
            //商品描述，可以为空。
            ViewData["productDesc"] = productDesc;
            //扩展字段1，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
            ViewData["ext1"] = ext1;
            //扩展自段2，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
            ViewData["ext2"] = ext2;
            //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10，必填。
            ViewData["payType"] = payType;
            //银行代码，如果payType为00，该值可以为空；如果payType为10，该值必须填写，具体请参考银行列表。
            ViewData["bankId"] = bankId;
            //同一订单禁止重复提交标志，实物购物车填1，虚拟产品用0。1代表只能提交一次，0代表在支付不成功情况下可以再提交。可为空。
            ViewData["redoFlag"] = redoFlag;
            //快钱合作伙伴的帐户号，即商户编号，可为空。
            ViewData["pid"] = pid;
            // signMsg 签名字符串 不可空，生成加密签名串
            ViewData["signMsg"] = signMsg;
            return View();
        }


        public ActionResult Receive()
        {
            //人民币网关账号，该账号为11位人民币网关商户编号+01,该值与提交时相同。
            string merchantAcctId = Request.QueryString["merchantAcctId"].ToString();
            //网关版本，固定值：v2.0,该值与提交时相同。
            string version = Request.QueryString["version"].ToString();
            //语言种类，1代表中文显示，2代表英文显示。默认为1,该值与提交时相同。
            string language = Request.QueryString["language"].ToString();
            //签名类型,该值为4，代表PKI加密方式,该值与提交时相同。
            string signType = Request.QueryString["signType"].ToString();
            //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10,该值与提交时相同。
            string payType = Request.QueryString["payType"].ToString();
            //银行代码，如果payType为00，该值为空；如果payType为10,该值与提交时相同。
            string bankId = Request.QueryString["bankId"].ToString();
            //商户订单号，,该值与提交时相同。
            string orderId = Request.QueryString["orderId"].ToString();
            //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101,该值与提交时相同。
            string orderTime = Request.QueryString["orderTime"].ToString();
            //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试,该值与支付时相同。
            string orderAmount = Request.QueryString["orderAmount"].ToString();
            // 快钱交易号，商户每一笔交易都会在快钱生成一个交易号。
            string dealId = Request.QueryString["dealId"].ToString();
            //银行交易号 ，快钱交易在银行支付时对应的交易号，如果不是通过银行卡支付，则为空
            string bankDealId = Request.QueryString["bankDealId"].ToString();
            //快钱交易时间，快钱对交易进行处理的时间,格式：yyyyMMddHHmmss，如：20071117020101
            string dealTime = Request.QueryString["dealTime"].ToString();
            //商户实际支付金额 以分为单位。比方10元，提交时金额应为1000。该金额代表商户快钱账户最终收到的金额。
            string payAmount = Request.QueryString["payAmount"].ToString();
            //费用，快钱收取商户的手续费，单位为分。
            string fee = Request.QueryString["fee"].ToString();
            //扩展字段1，该值与提交时相同。
            string ext1 = Request.QueryString["ext1"].ToString();
            //扩展字段2，该值与提交时相同。
            string ext2 = Request.QueryString["ext2"].ToString();
            //处理结果， 10支付成功，11 支付失败，00订单申请成功，01 订单申请失败
            string payResult = Request.QueryString["payResult"].ToString();
            //错误代码 ，请参照《人民币网关接口文档》最后部分的详细解释。
            string errCode = Request.QueryString["errCode"].ToString();
            //签名字符串 
            string signMsg = Request.QueryString["signMsg"].ToString();
            string signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            signMsgVal = appendParam(signMsgVal, "bankId", bankId);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "dealId", dealId);
            signMsgVal = appendParam(signMsgVal, "bankDealId", bankDealId);
            signMsgVal = appendParam(signMsgVal, "dealTime", dealTime);
            signMsgVal = appendParam(signMsgVal, "payAmount", payAmount);
            signMsgVal = appendParam(signMsgVal, "fee", fee);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payResult", payResult);
            signMsgVal = appendParam(signMsgVal, "errCode", errCode);

            ///UTF-8编码  GB2312编码  用户可以根据自己网站的编码格式来选择加密的编码方式
            ///byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
            byte[] SignatureByte = Convert.FromBase64String(signMsg);
            X509Certificate2 cert = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + "Content\\KQBill\\99bill.cert.rsa.20140728.cer", "");
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PublicKey.Key;
            rsapri.ImportCspBlob(rsapri.ExportCspBlob(false));
            RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapri);
            byte[] result;
            f.SetHashAlgorithm("SHA1");
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(bytes);


            if (f.VerifySignature(result, SignatureByte))
            {
                //支付成功
                //在这里商户可以写上自己的业务逻辑
                 
                //逻辑处理  写入数据库
                if (payResult == "10")
                {
                    BaseResponse br = DBOrderBLL.OrderPaySuccess(orderId, decimal.Parse(orderAmount), payerName);
                    if (br.IsSuccess)
                    {
                        return Content("<result>1</result><redirecturl>http://inpinke.com/order/success?ordercode=" + orderId + "</redirecturl>");
                    }
                    else
                    {
                        ViewBag.Msg = "对不起未能成功处理您的订单[" + orderId + "]，原因是：" + br.Message;
                        return View("error");
                    }                     
                }
                else
                {
                    ViewBag.Msg = string.Format("订单{0},支付失败，失败编号：{1},支付结果：{2}", orderId, errCode, payResult);
                    log.Info(ViewBag.Msg);
                    //以下是我们快钱设置的show页面，商户需要自己定义该页面。                    
                    return View("error");
                }
            }
            ViewBag.Msg = string.Format("订单{0},支付失败，失败编号：{1},支付结果：{2}", orderId, errCode, payResult);
            log.Info(ViewBag.Msg);
            //以下是我们快钱设置的show页面，商户需要自己定义该页面。                    
            return View("error");
        }

        ILog log = LogManager.GetLogger(typeof(KQBillController));

        //功能函数。将变量值不为空的参数组成字符串
        #region 字符串串联函数
        public string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    returnStr += "&" + paramId + "=" + paramValue;
                }
            }
            else
            {
                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }
            return returnStr;
        }
        #endregion
    }
}
