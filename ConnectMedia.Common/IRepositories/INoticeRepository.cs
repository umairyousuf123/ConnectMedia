using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface INoticeRepository
    {
        //List<UserGridView> getAllUserDetail(int Userid);
        List<NoticeGridView> getNotices(int id);
        NoticeDTO getNoticeDetail(int id);
        Template GetTemplate(int id);
        bool AddEditNotice(NoticeDTO noticeDTO);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Category> Categories();
        string UploadFileDetail(UploadFile uploadFile);
        List<Playlist> PlaylistDropdown(int UserId);
        List<UploadFile> GetUserUploadFile(int UserId);
        UploadFile getDocDetail(int UserId);
        string GetGuidUploadFile(int Id);
        List<SendEmailList> SenderList(int Id, string tn);
        List<SendSMSList> SenderSMSList(int Id, string tn);
        NoticeSendEmail TeamList(int Id);
        List<NoticeGridView> getAdminNotices();
    }
}
