using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectMedia.Common.IServices
{
    public interface INoticeService
    {

        List<NoticeGridView> getNotices(int Userid);
        NoticeDTO getNoticeDetail(int UserId);
        
        NoticeDTO TemplateDetail(int templateId);
        bool AddEditNotice(NoticeDTO notice);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Category> Categories();
        Task<string> SavePDF(PDFDTO pdfDTO);
        Task<string> SaveDoc(DocDTO docDTO);
        List<Playlist> PlaylistDropdown(int UserId);
        List<UploadFile> GetUserUploadFile (int UserId);
        UploadFile getDocDetail(int Id);
        string SendTeamEmail(int id, string tn, int UserId);
        string SendTeamSMS(int id, string tn, int UserId);
        NoticeSendEmail TeamList(int NoticeId);
        PromotionDTO GetPromotionWithNotice(int UserId);

    }
}
