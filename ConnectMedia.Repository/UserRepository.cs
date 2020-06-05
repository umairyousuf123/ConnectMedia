using ConnectMedia.Common.Database;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Repository
{
    public class UserRepository : IUserRepository
    {
        IConfiguration _configuration;
        private ConnectMediaDB _db;
        readonly ILogger<UserRepository> _logger;

        public UserRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<UserRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }

        public User UserLogin(string email)
        {
            User user = new User();
            try
            {
                user = _db.Users.SingleOrDefault(x => x.email == email && x.IsActive == true && x.IsDel == false);
                // return null if user not found
                if (user == null)
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return user;
        }
        public User GetUserDetail(string hashKey)
        {
            User user = new User();
            try
            {
                user = _db.Users.SingleOrDefault(x => x.password == hashKey);
                // return null if user not found
                if (user == null)
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return user;
        }

        public bool ConfirmPwdRequest(User user)
        {
            try
            {
                User oldUser = _db.Users.SingleOrDefault(b => b.email == user.email && b.IsDel == false);
                oldUser.password = user.password;
                oldUser.UpdatedOn = DateTime.UtcNow;
                oldUser.UpdatedBy = 1;
                _db.Users.Attach(oldUser);
                _db.Entry(oldUser).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
        public string getRoleName(int RoleId)
        {
            string RoleName = string.Empty;
            if (RoleId > 0)
            {
                RoleName = _db.Role.First(x => x.Id == RoleId).RoleName;
            }

            return RoleName;

        }

    }
}
