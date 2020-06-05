using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class ClaimModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string EmailAddress { get; set; }
        public bool EnableSsl { get; set; }
    }
    public class DocumentPath
    {
        public string LogoName { get; set; }
        public string Image { get; set; }
        public string CSV { get; set; }
        public string Video { get; set; }
        public string Pdf { get; set; }
        public string WordDocument { get; set; }
    }
    public class AppSetting
    {
        public string JWTSecurityKey { get; set; }
        public string Isuser { get; set; }
        public string Audience { get; set; }
        public string Login { get; set; }
    }
}
