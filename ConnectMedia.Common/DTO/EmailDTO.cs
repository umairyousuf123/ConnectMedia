using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class EmailDTO
    {
        public EmailDTO(string to, string subject, string message, bool isBodyHtml)
        {
            To = to;
            Subject = subject;
            Message = message;
            IsBodyHtml = isBodyHtml;
        }
        public string To
        {
            get;
        }
        public string Subject
        {
            get;
        }
        public string Message
        {
            get;
        }
        public bool IsBodyHtml
        {
            get;
        }
    }
    public class EmailSetting
    {
        public string Username { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }
        public bool EnableSsl { get; set; }
    }
}

