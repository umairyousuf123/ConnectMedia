using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface ITutorialRepository
    {
        List<UploadFile> GetAllTutorial(int UserId);
        UploadFile GetTutorialById(int Id);
        string UploadFileDetail(UploadFile uploadFile);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        string GetGuidUploadFile(int Id);
    }
}
