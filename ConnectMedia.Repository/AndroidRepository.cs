using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Repository
{
    public class AndroidRepository : IAndroidRepository
    {

        IConfiguration _configuration;
        private ConnectMediaDB _db;
        readonly ILogger<AndroidRepository> _logger;

        public AndroidRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<AndroidRepository> logger)
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
        public List<NoticeDTO> GetPlaylistFromBuilding(string Key)
        {
            List<NoticeDTO>  notices = new List<NoticeDTO>();
            try
            {
                Building building = _db.Building.Where(x => x.Key == Key && x.IsDel == false && x.IsActive == true)?.FirstOrDefault();
                if (building != null)
                {
                    notices = (from BP in _db.PlaylistBuilding
                                 join P in _db.Playlist on BP.PlaylistId equals P.Id
                                 join NP in _db.NoticePlaylist on P.Id equals NP.PlaylistId
                                 join N in _db.Notice on NP.NoticeId equals N.Id
                                 select new NoticeDTO
                                 {
                                     Name = N.Name,
                                     Duration = N.Duration,
                                     StartDate = N.StartDate,
                                     StartTime = N.StartTime,
                                     EndDate = N.EndDate,
                                     EndTime = N.EndTime,
                                     CategoryId = N.CategoryId,
                                     Content = N.Content,
                                     Expire = N.Expire,
                                     Id = N.Id,
                                 }
                                ).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

            return notices;
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
