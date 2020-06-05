using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMedia.Common.Helper
{
    public class SendEmail
    {
        readonly IConfiguration _configuration;
        readonly EmailSetting _emailSetting;
        readonly string Settings = "AppSettings";
        public SendEmail(IConfiguration configuration, EmailSetting emailSetting)
        {
            this._configuration = configuration;
            this._emailSetting = emailSetting;
        }
        private bool EmailSender(EmailDTO email)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_emailSetting.SmtpServer, 587))
                {
                    client.EnableSsl = _emailSetting.EnableSsl;
                    client.Credentials = new System.Net.NetworkCredential(_emailSetting.From, _emailSetting.Password);
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_emailSetting.From, _emailSetting.Username);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(email.To);
                    mailMessage.Body = email.Message;
                    mailMessage.Subject = email.Subject;
                    mailMessage.IsBodyHtml = email.IsBodyHtml;
                    client.Send(mailMessage);
                    mailMessage.Dispose();
                }
                return true;
            }
            catch
            {
                throw;
                return false;
            }
        }
        private async Task<bool> MultipleEmailSend(List<EmailDTO> emails)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_emailSetting.SmtpServer, 587))
                {
                    client.EnableSsl = _emailSetting.EnableSsl;
                    client.Credentials = new System.Net.NetworkCredential(_emailSetting.From, _emailSetting.Password);
                    foreach (var email in emails)
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(_emailSetting.From, _emailSetting.Username);
                        mailMessage.BodyEncoding = Encoding.UTF8;
                        mailMessage.To.Add(email.To);
                        mailMessage.Body = email.Message;
                        mailMessage.Subject = email.Subject;
                        mailMessage.IsBodyHtml = email.IsBodyHtml;
                        await client.SendMailAsync(mailMessage);
                    }
                }
                return true;
            }
            catch
            {
                throw;
                return false;
            }
        }
        public bool ForgotPassword(User user)
        {
            string Subject = "Forgot password email";
            string Body = EmailDesign.DesignForgotPasswordBody(user, _configuration.GetSection(Settings)["Login"]);
            EmailDTO emailDTO = new EmailDTO(user.email, Subject, Body, true);
            return this.EmailSender(emailDTO);
        }
        public bool SendNotice(List<SendEmailList> sender, NoticeDTO notice)
        {
            string Subject = "Notice created email";
            List<EmailDTO> emails = new List<EmailDTO>();
            foreach (var sendEmail in sender)
            {
                string Body = EmailDesign.DesignNoticeSendEmail(sendEmail,notice);
                EmailDTO emailDTO = new EmailDTO(sendEmail.Email, Subject, Body, true);
                emails.Add(emailDTO);
            }
            bool isSend = MultipleEmailSend(emails).Result;
            return isSend;
        }
        
    }
}
