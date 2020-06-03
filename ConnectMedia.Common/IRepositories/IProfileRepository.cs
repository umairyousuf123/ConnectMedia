using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface IProfileRepository
    {
        List<UserGridView> getAllUserDetail(int Userid);
        User getUserDetail(int id);
        bool AddEditUser(UserDTO userDTO);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Role> getAllRoles(int RoleId);
        List<Building> getAllBuildings(int RoleId);
    }
}
