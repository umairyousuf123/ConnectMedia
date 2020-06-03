using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Text;

namespace ConnectMedia.Common.Helper
{
    public class SendEmailHelper
    {
        readonly IConfiguration _configuration;

        readonly string appSettingSendGrid = "SendGrid";
        string SendGridApiKey = string.Empty;
        string SendGridApiUserName = string.Empty;
        readonly string getLoginURL = "AppSettings";

        public SendEmailHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public int SendEmail(User user)
        {
            string Subject = "Forgot password email";
            string Body = DesignForgotPasswordBody(user);
            int isEmailSend = SendEmailViaSendGrid("abcEmail", user.email, Subject, Body);
            return isEmailSend;
        }
        string DesignForgotPasswordBody(User user)
        {
            string encryptpwd = EncryptSecreteString.Encrypt(user.password);
            string LoginUrl = _configuration.GetSection(getLoginURL)["Login"] + "Authentication/ChangePassword/" + encryptpwd;
            string email_body = string.Empty;
            email_body += "<div>Dear " + user.firstName + " " + user.lastName + ",<br/><br/>";
            email_body += "We have sent you this email in response to your request to reset your password on. <br/><br/>";
            email_body += "To reset your password, Please follow the link below: <a href = '" + LoginUrl + "' >Reset Password</a> ";
            email_body += "<br/><br/>We recommend that you keep your password secure and not share it with anyone. <br/><br/>";
            email_body += "Regards <br/><br/> Connect Media.";
            return email_body;
        }

        int SendEmailViaSendGrid(string ToEmail, string FromEmail, string Subject, string Body)
        {

            int returnResult = -1;
            try
            {
                SendGridApiKey = "";// _configuration.GetSection(appSettingSendGrid)["SendgridAPIKey"];
                SendGridApiUserName = _configuration.GetSection(appSettingSendGrid)["SendgridUserName"];

                var client = new SendGridClient(SendGridApiKey);
                var To = new EmailAddress(ToEmail, "");
                var From = new EmailAddress(FromEmail, "");
                var htmlContent = Body;
                var msg = MailHelper.CreateSingleEmail(From, To, Subject, Body, htmlContent);
                var response = (System.Threading.Tasks.Task<SendGrid.Response>)(client.SendEmailAsync(msg));
                if (response != null)
                {
                    System.Net.HttpStatusCode status = response.Result.StatusCode;

                    switch (status)
                    {
                        case System.Net.HttpStatusCode.Accepted:
                            {
                                returnResult = 1;
                                break;
                            }
                        case System.Net.HttpStatusCode.OK:
                            {
                                returnResult = 1;
                                break;
                            }
                        default:
                            {
                                returnResult = 0;
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return returnResult;
        }
    }
}
