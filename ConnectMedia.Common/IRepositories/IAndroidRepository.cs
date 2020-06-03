using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface IAndroidRepository
    {
        User UserLogin(string Email);
        string getRoleName(int RoleId);
        List<NoticeDTO> GetPlaylistFromBuilding(string Key);
    }

}
