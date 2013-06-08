using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace inpinke.com.Models
{
    public class OrderInfoModel
    {
        
        public int AddressID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写收货人姓名")]
        [Display(Name = "收货人")]
        public string Consignee { get; set; }

        
        [Display(Name = "省")]
        public int ProvID { get; set; }
        
        [Display(Name = "市")]
        public int CityID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择省市区")]
        [Display(Name = "区")]
        public int AreaID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写收货人的详细地址")]
        [Display(Name = "详细地址")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写收货人的手机号码")]
        [Display(Name = "手机号码")]
        public string Mobile { get; set; }

        [Display(Name="备注")]
        public string Remark { get; set; }
    }
}