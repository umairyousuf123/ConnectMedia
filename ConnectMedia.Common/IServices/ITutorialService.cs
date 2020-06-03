using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMedia.Common.IServices
{
    public interface ITutorialService
    {
        List<UploadFile> GetAllTutorial(int UserId);
        UploadFile GetTutorialById(int UserId);
        Task<string> AddEditTutorial(VideoDTO videoDTO);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
    }
}
