using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectMedia.Common.IServices
{
    public interface ISettingService
    {
        #region Template
        List<Template> Templates(int userId);
        Template GetTemplateDetail(int Id);
        bool AddEdit(Template template);
        bool Delete(int Id, int CurrentUserId);
        #endregion

        #region Registered Email
        Task<List<ResgisterUser>> CSVList(int UserId, string TeamName);
        Task<ResgisterUser> getEmailUserDetail(int Id);
        Task<bool> createUpdateEmailUser(ResgisterUser resgisterUser);
        Task<bool> DeleteCSVUser(int Id, int CurrentUserId);
        List<string> TeamList(int Id);
        #endregion

        #region Save Files CSV And Logo
        string RetriveLogo();
        Task<string> SaveLogo(ImageDTO imageDTO);
        Task<string> SaveCSV(CSVDTO csvDTO);

        #endregion
        
    }
}
