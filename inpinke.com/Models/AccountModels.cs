using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace inpinke.com.Models
{

    public class ChangePasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入密码")]
        [StringLength(20, ErrorMessage = "密码长度为{2}到{1}个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}的格式不正确")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入您注册时填写的邮箱")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }
    }

    public class LogOnModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}的格式不正确")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入您注册时填写的邮箱")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "密码长度为{2}到{1}个字符", MinimumLength = 6)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "下次自动登录")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "昵称将显示在您分享的照片书的作者栏上")]
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Remote("AjaxCheckEmailIsUsed", "Account", ErrorMessage = "该邮箱已经注册过了")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}的格式不正确")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "电子邮箱将用作登录和找回密码")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入密码")]
        [StringLength(20, ErrorMessage = "密码长度为{2}到{1}个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { get; set; }

        
    }
}
