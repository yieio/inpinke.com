<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>订单提交</title>
</head>

<body onload="javascript:document.kqPay.submit()" >
   <form name="kqPay" action="https://www.99bill.com/gateway/recvMerchantInfoAction.htm" method="post">
				<input type="hidden" name="inputCharset" value="<%=ViewData["inputCharset"]%>" />
				<input type="hidden" name="pageUrl" value="<%=ViewData["pageUrl"]%>" />
				<input type="hidden" name="bgUrl" value="<%=ViewData["bgUrl"]%>" />
				<input type="hidden" name="version" value="<%=ViewData["version"]%>" />
				<input type="hidden" name="language" value="<%=ViewData["language"]%>" />
				<input type="hidden" name="signType" value="<%=ViewData["signType"]%>" />
				<input type="hidden" name="signMsg" value="<%=ViewData["signMsg"]%>" />
				<input type="hidden" name="merchantAcctId" value="<%=ViewData["merchantAcctId"]%>" />
				<input type="hidden" name="payerName" value="<%=ViewData["payerName"]%>" />
				<input type="hidden" name="payerContactType" value="<%=ViewData["payerContactType"]%>" />
				<input type="hidden" name="payerContact" value="<%=ViewData["payerContact"]%>" />
				<input type="hidden" name="orderId" value="<%=ViewData["orderId"]%>" />
				<input type="hidden" name="orderAmount" value="<%=ViewData["orderAmount"]%>" />
				<input type="hidden" name="orderTime" value="<%=ViewData["orderTime"]%>" />
				<input type="hidden" name="productName" value="<%=ViewData["productName"]%>" />
				<input type="hidden" name="productNum" value="<%=ViewData["productNum"]%>" />
				<input type="hidden" name="productId" value="<%=ViewData["productId"]%>" />
				<input type="hidden" name="productDesc" value="<%=ViewData["productDesc"]%>" />
				<input type="hidden" name="ext1" value="<%=ViewData["ext1"]%>" />
				<input type="hidden" name="ext2" value="<%=ViewData["ext2"]%>" />
				<input type="hidden" name="payType" value="<%=ViewData["payType"]%>" />
				<input type="hidden" name="bankId" value="<%=ViewData["bankId"]%>" />
				<input type="hidden" name="redoFlag" value="<%=ViewData["redoFlag"]%>" />
				<input type="hidden" name="pid" value="<%=ViewData["pid"]%>" />
				 
			</form>
</body>
</html>
