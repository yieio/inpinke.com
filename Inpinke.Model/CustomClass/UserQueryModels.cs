using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inpinke.Model.CustomClass
{
    public sealed class CompareSetAttribute : Attribute
    {       
        /// <summary>
        /// 忽略值
        /// </summary>
        public string IgnoreValue { get; set; }
        /// <summary>
        /// 比较符
        /// </summary>
        public string Compare { get; set; }
        /// <summary>
        /// 与哪个字段做比较
        /// </summary>
        public string CompareWith { get; set; }

    }
    public class UserQueryModels
    {
        [CompareSet(IgnoreValue = "")]
        public string Email { get; set; }

        public string NickName { get; set; }
         [CompareSet(IgnoreValue = "0")]
        public int UserStatus { get; set; }

        [CompareSet(IgnoreValue = "0")]
        public int ChannelID { get; set; }
    }
}