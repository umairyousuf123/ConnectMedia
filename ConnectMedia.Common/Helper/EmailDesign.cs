using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ConnectMedia.Common.Helper
{
    public class EmailDesign
    {
        public static string DesignForgotPasswordBody(User user, string BaseUrl)
        {
            string encryptpwd = EncryptSecreteString.Encrypt(user.password);
            string LoginUrl = BaseUrl + "Authentication/ChangePassword/" + encryptpwd;
            string email_body = string.Empty;
            email_body += "<div>Dear " + user.firstName + " " + user.lastName + ",<br/><br/>";
            email_body += "We have sent you this email in response to your request to reset your password on. <br/><br/>";
            email_body += "To reset your password, Please follow the link below: <a href = '" + LoginUrl + "' >Reset Password</a> ";
            email_body += "<br/><br/>We recommend that you keep your password secure and not share it with anyone. <br/><br/>";
            email_body += "Regards <br/><br/> Connect Media.";
            return email_body;
        }
        public static string DesignNoticeSendEmail(SendEmailList sendEmail, NoticeDTO notice)
        {
            string email_body = string.Empty;
            email_body += "<div>Dear " + sendEmail.Name + " ,<br/><br/>";
            email_body += "We have generated a notice name:" + notice.Name + ".<br/>" + notice.Content + " <br/><br/>";
            email_body += "of the duration " + notice.Duration + "s and the Start time of notice is" + notice.StartDate + " " + notice.StartTime + " <br/><br/>";
            email_body += "<br/><br/>We recommend that you keep your email secure and not share it with anyone. <br/><br/>";
            email_body += "Regards <br/><br/> Connect Media.";
            return email_body;
        }
    }
}
