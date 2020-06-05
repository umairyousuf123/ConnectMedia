using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Services
{
    public class ProfileService : IProfileService
    {
        IProfileRepository _profileRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<ProfileService> _logger;

        public ProfileService(ILogger<ProfileService> logger, IProfileRepository profileRepository, IConfiguration configuration)
        {
            _logger = logger;
            this._profileRepository = profileRepository;
            this._configuration = configuration;
        }
        public List<UserGridView> getAllUserDetail(int Userid)
        {
            List<UserGridView> userList = _profileRepository.getAllUserDetail(Userid);

            return userList;
        }
        public UserDTO getUserDetail(int id)
        {
            User user = new User();
            UserDTO userDTO = new UserDTO();
            user = _profileRepository.getUserDetail(id);
            if (user != null)
            {
                userDTO.Id = user.Id;
                userDTO.FirstName = user.firstName;
                userDTO.LastName = user.lastName;
                userDTO.Password = user.password;
                userDTO.Email = user.email;
                userDTO.ContactNumber = user.contactNumber;
                userDTO.RoleId = user.roleId;
                userDTO.BuildingId = user.BuildingIds.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            }
            return userDTO;
        }
        public bool AddEditUser(UserDTO userDTO)
        {
            bool isSaved = false;
            isSaved = _profileRepository.AddEditUser(userDTO);
            return isSaved;
        }
        public bool Delete(int id, int CurrentUserId)
        {
            bool isDeleted = false;
            isDeleted = _profileRepository.Delete(id, CurrentUserId);
            return isDeleted;
        }
        public bool Active(int id, int CurrentUserId)
        {
            bool isActive = false;
            isActive = _profileRepository.Active(id, CurrentUserId);
            return isActive;
        }
        public List<Role> roles(int RoleId)
        {
            List<Role> rolesList = _profileRepository.getAllRoles(RoleId);
            return rolesList;
        }
        public List<Building> buildings(int RoleId)
        {
            List<Building> buildings = _profileRepository.getAllBuildings(RoleId);
            return buildings;
        }
    }
}
