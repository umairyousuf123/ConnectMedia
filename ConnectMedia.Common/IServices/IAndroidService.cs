using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMedia.Common.IServices
{
    public interface IAndroidService
    {
        Task<DataTransferObject<loginDetails>> UserLogin(LoginRequestDTO data);
        List<NoticeDTO> GetPlatlistFromBuilding(string Key);
    }
}
