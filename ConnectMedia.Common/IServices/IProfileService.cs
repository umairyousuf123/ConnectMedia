using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IServices
{
    public interface IProfileService
    {
        List<UserGridView> getAllUserDetail(int Userid);
        bool AddEditUser(UserDTO userDTO);
        UserDTO getUserDetail(int Userid);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Role> roles(int RoleId);
        List<Building> buildings(int RoleId);
    }
}
