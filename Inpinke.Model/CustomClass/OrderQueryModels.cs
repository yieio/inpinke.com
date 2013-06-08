using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.CustomClass
{
    public class OrderQueryModels
    {

        [CompareSet(IgnoreValue = "", CompareWith = "OrderCode", Compare = "=")]
        public string OrderCode { get; set; }

        [CompareSet(IgnoreValue = "0", CompareWith = "OrderStatus", Compare = "=")]
        public int OrderStatus { get; set; }

        [CompareSet(IgnoreValue = "", CompareWith = "Email", Compare = "=")]
        public string Email { get; set; }

        [CompareSet(IgnoreValue = "", CompareWith = "O.CreateTime", Compare = ">")]
        public string StartTime { get; set; }

        private DateTime? _EndTime;

        [CompareSet(IgnoreValue = "", CompareWith = "O.CreateTime", Compare = "<")]
        public string EndTime
        {
            get
            {
                if (!_EndTime.HasValue)
                {
                    return "";
                }
                else
                {
                    return (_EndTime.Value.AddDays(1)).ToString("yyyy-MM-dd mm:ss");
                }
            }
            set{_EndTime = DateTime.Parse(value);}
        }

        [CompareSet(IgnoreValue = "", CompareWith = "O.PayTime", Compare = ">")]
        public string PayStartTime { get; set; }

        private DateTime? _PayEndTime;

        [CompareSet(IgnoreValue = "", CompareWith = "O.PayTime", Compare = "<")]
        public string PayEndTime
        {
            get
            {
                if (!_PayEndTime.HasValue)
                {
                    return "";
                }
                else
                {
                    return (_PayEndTime.Value.AddDays(1)).ToString("yyyy-MM-dd mm:ss");
                }
            }
            set { _PayEndTime = DateTime.Parse(value); }
        }


    }
}
