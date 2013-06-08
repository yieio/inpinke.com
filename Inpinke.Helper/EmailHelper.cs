using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Helper;
using System.Net;
using System.Reflection;
using log4net;

namespace Inpinke.Helper
{
    public class EmailHelper
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(EmailHelper));

        public static void SendEmail(string email, string emailMsg, string title)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            string emailAddress = ConfigHelper.ReadConfig("email", "configuration/EmailAddress");
            string emailPassword = ConfigHelper.ReadConfig("email", "configuration/EmailPassword");
            string emailSMTP = ConfigHelper.ReadConfig("email", "configuration/EmailSMTP");
            string displayName = ConfigHelper.ReadConfig("email", "configuration/DisplayName");

            //set the addresses
            mail.From = new MailAddress(emailAddress, displayName);
            mail.To.Add(email);

            //set the content
            mail.Subject = title;
            mail.Body = emailMsg;
            mail.IsBodyHtml = true;

            //send the message
            SmtpClient smtp = new SmtpClient(emailSMTP);
            smtp.Credentials = new NetworkCredential(emailAddress, emailPassword);


            smtp.Send(mail);
        }
        /// <summary>
        /// 填充邮件模版参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emailTemplate"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ReplaceTemplateVar<T>(string emailTemplate, T t)
        {
            try
            {
                if (string.IsNullOrEmpty(emailTemplate))
                {
                    return "";
                }
                PropertyInfo[] pis = t.GetType().GetProperties();
                foreach (PropertyInfo p in pis)
                {
                    if (p.GetValue(t, null) != null)
                    {
                        //设置节点属性               
                       emailTemplate = emailTemplate.Replace("{" + p.Name + "}", p.GetValue(t, null).ToString());
                    }
                }
                return emailTemplate;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("ReplaceTemplateVar Error:{0},EmailTemplate:{1}", ex.ToString(), emailTemplate));
                return string.Empty;
            }
        }
    }
}
