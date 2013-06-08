using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.Enum
{
    public enum PayMethod
    {
        //0-支付宝，1-网银，2-到付，3-其他
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 1,
        /// <summary>
        /// 网银
        /// </summary>
        NetBank = 2,
        /// <summary>
        /// 到付
        /// </summary>
        ArrivedPay = 3,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 4
    }
}
