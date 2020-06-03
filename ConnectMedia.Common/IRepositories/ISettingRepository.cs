using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface ISettingRepository
    {
        #region Template
        List<Template> Templates();
        Template GetTemplateDetail(int Id);
        bool AddEdit(Template template);
        bool Delete(int Id, int CurrentUserId);
        #endregion
        string RetriveLogo();
        string csvList(List<ResgisterUser> resgisterUsers);
        string UploadFileDetail(UploadFile uploadFile);


        List<ResgisterUser> CSVList(int UserId, string TeamName);
        ResgisterUser getEmailUserDetail(int Id);
        bool createUpdateEmailUser(ResgisterUser resgisterUser);
        bool DeleteCSVUser(int Id, int CurrentUserId);
        List<string> TeamList(int Id);


    }
}
