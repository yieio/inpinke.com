using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper.Attributes;

namespace Inpinke.Model.Enum
{ 

    public enum OrderStatus
    {
        /// <summary>
        /// 待审核/未支付
        /// </summary>         
        [Desc("未支付")]
        Create = 1,
        /// <summary>
        /// 已支付/待发货
        /// </summary>
        [Desc("待发货")]
        WaitSend = 2,
        /// <summary>
        /// 配送中/已发货
        /// </summary>
        [Desc("已发货")]
        Sended = 3,
        /// <summary>
        /// 已领取/交易成功
        /// </summary>
        [Desc("交易成功")]
        Success = 4,
        /// <summary>
        /// 已取消,配送失败取消订单
        /// </summary>
         [Desc("交易失败")]
        Fail = 5,
        /// <summary>
        /// 审核失败
        /// </summary>
       [Desc("审核失败")]
        NotPass = 6,
        /// <summary>
        /// 交易关闭
        /// </summary>
        [Desc("交易关闭")]
        Closed = 7
    }


    public class OrderStatusDesc
    {
        /// <summary>
        /// 获取订单状态描述
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetOrderStatusDesc(int status)
        {
            string statusDesc = "";
            switch (status)
            {
                case 1: statusDesc = "未支付";
                    break;
                case 2: statusDesc = "待发货";
                    break;
                case 3: statusDesc = "已发货";
                    break;
                case 4: statusDesc = "交易完成";
                    break;
                case 5: statusDesc = "交易失败";
                    break;
                case 6: statusDesc = "审核失败";
                    break;
                case 7: statusDesc = "交易关闭";
                    break;
                default: statusDesc = "未知";
                    break;
            }
            return statusDesc;
        }
     
    }
}
