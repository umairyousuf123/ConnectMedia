using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace ConnectMedia.Controllers
{
    public class BaseController : Controller
    {

        public string getCurrentUserName()
        {
            string UserName = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            return UserName;
        }
        public int getCurrentUserId()
        {


            int UserId = 0;
            string StrUserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            if (!string.IsNullOrEmpty(StrUserId))
            {
                UserId = Convert.ToInt32(StrUserId);
            }
            return UserId;
        }
        public string getCurrentUserEmail()
        {
            string UserEmail = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
            return UserEmail;
        }
        public int getCurrentUserRoleId()
        {
            int RoleId = 0;
            string strRoleId = User.Claims.Where(c => c.Type == "RoleId").Select(c => c.Value).SingleOrDefault();
            if (!string.IsNullOrEmpty(strRoleId))
            {
                RoleId = Convert.ToInt32(strRoleId);
            }

            return RoleId;
        }
        public string getCurrentUserUserRoleName()
        {
            string UserRoleName = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            return UserRoleName;
        }
     
    }
}