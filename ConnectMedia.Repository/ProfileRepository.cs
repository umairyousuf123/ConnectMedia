using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
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
    public class ProfileRepository : IProfileRepository
    {
        IConfiguration _configuration;
        private ConnectMediaDB _db;
        readonly ILogger<ProfileRepository> _logger;
        public ProfileRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<ProfileRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }


        public List<UserGridView> getAllUserDetail(int Userid)
        {
            List<UserGridView> userGridViews = new List<UserGridView>();
            try
            {
                int roleId = 1;
                roleId = _db.Users.Where(x => x.Id == Userid).FirstOrDefault().roleId;
                List<Building> buildings = _db.Building.Where(t => t.IsDel == false).ToList(); ;

                userGridViews = (from U in _db.Users
                                 join R in _db.Role on U.roleId equals R.Id
                                 where U.IsDel == false && U.CreatedBy == Userid && U.roleId > roleId
                                 orderby U.Id descending
                                 select new UserGridView
                                 {
                                     Id = EncryptSecreteString.Encrypt(U.Id.ToString()),
                                     FullName = U.firstName + " " + U.lastName,
                                     Email = U.email,
                                     ContactNumber = U.contactNumber,
                                     RoleName = R.RoleName,
                                     BuildingName = ProfileRepository.BuildingName(U.BuildingIds, buildings),
                                 }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }

            return userGridViews;
        }
        public User getUserDetail(int id)
        {
            User user = _db.Users.SingleOrDefault(b => b.Id == id && b.IsDel == false);
            return user;
        }
        public bool AddEditUser(UserDTO userDTO)
        {
            try
            {
                string BuildingIds = string.Empty;
                if (userDTO.BuildingId.Count() > 0)
                {
                    BuildingIds = string.Join(",", userDTO.BuildingId);
                }
                if (userDTO.Id > 0)
                {
                    User Olduser = _db.Users.SingleOrDefault(b => b.Id == userDTO.Id && b.IsDel == false);
                    Olduser.Id = userDTO.Id;
                    Olduser.firstName = userDTO.FirstName;
                    Olduser.lastName = userDTO.LastName;
                    Olduser.email = userDTO.Email;
                    Olduser.password = userDTO.Password;
                    Olduser.contactNumber = userDTO.ContactNumber;
                    Olduser.BuildingIds = BuildingIds;
                    Olduser.roleId = userDTO.RoleId;
                    Olduser.UpdatedOn = DateTime.UtcNow;
                    Olduser.UpdatedBy = userDTO.entryBy;
                    _db.Users.Attach(Olduser);
                    _db.Entry(Olduser).State = EntityState.Modified;
                }
                else
                {
                    User user = new User();
                    user.firstName = userDTO.FirstName;
                    user.lastName = userDTO.LastName;
                    user.email = userDTO.Email;
                    user.password = userDTO.Password;
                    user.BuildingIds = BuildingIds;
                    user.contactNumber = userDTO.ContactNumber;
                    user.roleId = userDTO.RoleId;
                    user.CreatedOn = DateTime.UtcNow;
                    user.CreatedBy = userDTO.entryBy;

                    _db.Users.Add(user);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }
        public bool Delete(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            User user = new User();
            try
            {
                user = _db.Users.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                user.IsDel = true;
                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy = CurrentUserId;
                _db.Users.Attach(user);
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                isDeleted = false;

            }
            return isDeleted;
        }
        public bool Active(int Id, int CurrentUserId)
        {
            bool isActive = false;
            User user = new User();
            try
            {
                user = _db.Users.SingleOrDefault(x => x.Id == Id && x.IsDel == false && x.IsActive == true);
                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy = CurrentUserId;
                _db.Users.Attach(user);
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                isActive = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                isActive = false;

            }
            return isActive;
        }
        public List<Role> getAllRoles(int RoleId)
        {
            List<Role> roleList = new List<Role>();
            try
            {
                roleList = _db.Role.Where(x => x.isDel == false && x.Id > RoleId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }
            return roleList;
        }
        public List<Building> getAllBuildings(int RoleId)
        {
            List<Building> buildings = new List<Building>();
            try
            {
                buildings = _db.Building.Where(x => x.IsDel == false).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }
            return buildings;
        }
        private static string BuildingName(string BuildingIds, List<Building> buildings)
        {
            int[] buildingId = BuildingIds.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            IEnumerable<string> listBuildingName = buildings.Where(t => buildingId.Contains(t.Id)).Select(x => x.Name).ToList();
            return string.Join(",", listBuildingName);
        }
    }
}
