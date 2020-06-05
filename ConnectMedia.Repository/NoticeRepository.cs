using ConnectMedia.Common;
using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Enum;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        readonly IConfiguration _configuration;
        private ConnectMediaDB _db;
        readonly ILogger<NoticeRepository> _logger;
        readonly int AllowRole = 3;
        readonly int[] RoleIds = new int[] { 2, 3 };
        public NoticeRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<NoticeRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }


        public List<NoticeGridView> getNotices(int Userid)
        {
            int roleid = _db.Users.Where(x => x.Id == Userid).FirstOrDefault().roleId;
            List<NoticeGridView> notices = new List<NoticeGridView>();
            try
            {
                if (roleid <= AllowRole)
                {
                    List<Playlist> playlists = _db.Playlist.Where(x => x.IsDel == false).ToList();
                    List<NoticePlaylist> noticePlaylists = _db.NoticePlaylist.Where(x => x.IsDel == false && x.NoticeId != null).ToList();
                    notices = (from N in _db.Notice
                               join C in _db.Category on N.CategoryId equals C.Id
                               where N.IsDel == false
                               orderby N.Id descending
                               select new NoticeGridView
                               {
                                   Id = N.Id,
                                   Category = C.Name,
                                   Content = N.Content,
                                   Duration = N.Duration.ToString() + " s",
                                   Start = N.StartDate.ToShortDateString() + " " + N.StartTime.Hours.ToString() + ":" + N.StartTime.Minutes.ToString(),
                                   End = N.EndDate.ToShortDateString() + " " + N.EndTime.Hours.ToString() + ":" + N.EndTime.Minutes.ToString(),
                                   Name = N.Name,
                                   Playlist = getPlayListName(N.Id, playlists, noticePlaylists),
                                   IsActive = N.IsActive
                               }).ToList();
                }
                else
                {
                    List<Playlist> playlists = _db.Playlist.Where(x => x.IsDel == false && x.CreatedBy == Userid).ToList();
                    List<NoticePlaylist> noticePlaylists = _db.NoticePlaylist.Where(x => x.IsDel == false && x.CreatedBy == Userid && x.NoticeId != null).ToList();
                    notices = (from N in _db.Notice
                               join C in _db.Category on N.CategoryId equals C.Id
                               where N.IsDel == false && N.CreatedBy == Userid
                               orderby N.Id descending
                               select new NoticeGridView
                               {
                                   Id = N.Id,
                                   Category = C.Name,
                                   Content = N.Content,
                                   Duration = N.Duration.ToString() + "s",
                                   Start = N.StartDate.ToShortDateString() + " " + N.StartTime.ToString(@"hh\:mm"),
                                   End = N.EndDate.ToShortDateString() + " " + N.EndTime.ToString(@"hh\:mm"),
                                   Name = N.Name,
                                   Playlist = getPlayListName(N.Id, playlists, noticePlaylists),
                               }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return notices;
        }

        public NoticeDTO getNoticeDetail(int id)
        {
            NoticeDTO noticeDTO = new NoticeDTO();
            IEnumerable<int> NoticePlaylists = _db.NoticePlaylist.Where(x => x.NoticeId == id && x.IsDel == false).Select(x => x.PlaylistId).ToList();

            Notice notice = _db.Notice.SingleOrDefault(b => b.Id == id && b.IsDel == false);
            if (notice != null)
            {
                DataCopier.Copy(notice, noticeDTO);
            }
            noticeDTO.PlayList = NoticePlaylists;
            return noticeDTO;
        }
        public bool AddEditNotice(NoticeDTO noticeDTO)
        {
            int Success;
            Notice notice = new Notice();
            List<NoticePlaylist> noticePlaylists = new List<NoticePlaylist>();
            if (noticeDTO.Id > 0)
            {
                noticePlaylists = _db.NoticePlaylist.Where(x => x.NoticeId == noticeDTO.Id && x.IsDel == false).ToList();
                if (noticePlaylists.Count() > 0)
                {
                    noticePlaylists.ForEach(a => { a.IsDel = true; a.UpdatedOn = DateTime.Now; a.UpdatedBy = noticeDTO.entryBy; });
                    Success = _db.SaveChanges();
                    noticePlaylists.Clear();
                }
            }
            DataCopier.Copy(noticeDTO, notice);
            try
            {
                if (notice.Id > 0)
                {
                    Notice Oldnotice = _db.Notice.SingleOrDefault(b => b.Id == notice.Id && b.IsDel == false);
                    Oldnotice.Name = notice.Name;
                    Oldnotice.CategoryId = notice.CategoryId;
                    Oldnotice.Content = notice.Content;
                    Oldnotice.Duration = notice.Duration;
                    Oldnotice.StartDate = notice.StartDate;
                    Oldnotice.StartTime = notice.StartTime;
                    Oldnotice.EndDate = notice.EndDate;
                    Oldnotice.EndTime = notice.EndTime;
                    Oldnotice.Expire = notice.Expire;
                    Oldnotice.UpdatedOn = DateTime.Now;
                    Oldnotice.UpdatedBy = noticeDTO.entryBy;

                    _db.Notice.Attach(Oldnotice);
                    _db.Entry(Oldnotice).State = EntityState.Modified;
                }
                else
                {
                    notice.CreatedOn = DateTime.Now;
                    notice.CreatedBy = noticeDTO.entryBy;
                    _db.Notice.Add(notice);
                }
                Success = _db.SaveChanges();
                if (notice.Id > 0 && noticeDTO.PlayList.Count() > 0)
                {
                    foreach (int playlist in noticeDTO.PlayList)
                    {
                        NoticePlaylist playlistNote = new NoticePlaylist();
                        playlistNote.Id = 0;
                        playlistNote.NoticeId = notice.Id;
                        playlistNote.PlaylistId = playlist;
                        playlistNote.CreatedBy = noticeDTO.entryBy;
                        playlistNote.CreatedOn = DateTime.Now;
                        noticePlaylists.Add(playlistNote);
                    }

                    _db.NoticePlaylist.AddRange(noticePlaylists);
                    Success = _db.SaveChanges();
                }
                return (Success > 0) ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }
        public bool Delete(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            Notice notice = new Notice();
            try
            {
                notice = _db.Notice.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                notice.IsDel = true;
                notice.UpdatedOn = DateTime.UtcNow;
                notice.UpdatedBy = CurrentUserId;
                _db.Notice.Attach(notice);
                _db.Entry(notice).State = EntityState.Modified;
                _db.SaveChanges();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                isDeleted = false;

            }
            return isDeleted;
        }
        public bool Active(int Id, int CurrentUserId)
        {
            bool isActive = false;
            Notice notice = new Notice();
            try
            {
                notice = _db.Notice.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                notice.IsActive = !notice.IsActive;
                notice.UpdatedOn = DateTime.UtcNow;
                notice.UpdatedBy = CurrentUserId;
                _db.Notice.Attach(notice);
                _db.Entry(notice).State = EntityState.Modified;
                _db.SaveChanges();
                isActive = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                isActive = false;

            }
            return isActive;
        }
        public List<Category> Categories()
        {
            List<Category> categoryList = new List<Category>();
            try
            {
                categoryList = _db.Category.Where(x => x.IsDel == false).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }
            return categoryList;
        }
        public Template GetTemplate(int templateId)
        {
            Template template = _db.Template.SingleOrDefault(b => b.Id == templateId && b.IsDel == false);
            return template;
        }
        public string UploadFileDetail(UploadFile uploadFile)
        {
            string ErrorMessage = string.Empty;
            try
            {
                if (uploadFile.Id > 0)
                {

                    UploadFile uploadFileObj = _db.UploadFile.SingleOrDefault(x => x.Id == uploadFile.Id);
                    uploadFileObj.Title = uploadFile.Title;
                    uploadFileObj.Desc = uploadFile.Desc;
                    uploadFileObj.UploadType = UploadDocumentType.PDF.ToString();
                    uploadFileObj.FileName = uploadFile.FileName;
                    uploadFileObj.FilePath = uploadFile.FilePath;
                    _db.UploadFile.Attach(uploadFileObj);
                    _db.Entry(uploadFileObj).State = EntityState.Modified;
                }
                else
                {
                    _db.UploadFile.Add(uploadFile);
                }
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Upload sucessfully " : "Upload not ";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                _logger.LogError(ex.ToString());
            }
            return ErrorMessage;
        }

        public List<Playlist> PlaylistDropdown(int UserId)
        {
            {
                List<Playlist> playlists = new List<Playlist>();
                try
                {
                    int roleid = _db.Users.Where(x => x.Id == UserId).FirstOrDefault().roleId;
                    if (roleid <= AllowRole)
                    {
                        playlists = _db.Playlist.Where(x => x.IsDel == false).ToList();
                    }
                    else
                    {
                        playlists = _db.Playlist.Where(x => x.CreatedBy == UserId && x.IsDel == false).ToList();

                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return playlists;
            }

        }
        private static string getPlayListName(int NoticeId, List<Playlist> playlists, List<NoticePlaylist> noticePlaylists)
        {
            IEnumerable<int> playlistIds = noticePlaylists.Where(x => x.NoticeId == NoticeId && x.IsDel == false).Select(x => x.PlaylistId).ToList();
            IEnumerable<string> PlaylistNames = playlists.Where(x => playlistIds.Contains(x.Id)).Select(x => x.Name).ToList();
            return string.Join(", ", PlaylistNames);
        }

        public List<UploadFile> GetUserUploadFile(int UserId)
        {
            {
                List<UploadFile> uploadFiles = new List<UploadFile>();
                try
                {
                    int roleid = _db.Users.Where(x => x.Id == UserId).FirstOrDefault().roleId;
                    if (roleid <= AllowRole)
                    {
                        uploadFiles = _db.UploadFile.Where(x => x.IsDel == false && (x.UploadType == UploadDocumentType.Doc.ToString() || x.UploadType == UploadDocumentType.PDF.ToString())).OrderBy(x => x.Id).ToList();
                    }
                    else
                    {
                        uploadFiles = _db.UploadFile.Where(x => x.CreatedBy == UserId && x.IsDel == false && (x.UploadType == UploadDocumentType.Doc.ToString() || x.UploadType == UploadDocumentType.PDF.ToString())).OrderBy(x => x.Id).ToList();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return uploadFiles;
            }
        }

        public UploadFile getDocDetail(int Id)
        {
            {
                UploadFile uploadFile = new UploadFile();
                try
                {
                    uploadFile = _db.UploadFile.Where(x => x.Id == Id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return uploadFile;
            }
        }

        public string GetGuidUploadFile(int Id)
        {
            {
                string fileName = string.Empty;
                try
                {
                    fileName = _db.UploadFile.SingleOrDefault(x => x.Id == Id).FileName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return fileName;
            }
        }

        public NoticeSendEmail TeamList(int NoticeId)
        {
            NoticeSendEmail noticeSendEmail = new NoticeSendEmail();
            try
            {
                noticeSendEmail.EmailTeam = _db.ResgisterUser.Where(b => b.IsDel == false).Select(o => o.TeamName).Distinct().ToList();
                noticeSendEmail.NoticeId = NoticeId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return noticeSendEmail;
        }
        public List<SendEmailList> SenderList(int UserId, string TeamName)
        {
            List<SendEmailList> sendEmailList = new List<SendEmailList>();
            try
            {
                sendEmailList = (from RU in _db.ResgisterUser
                                 where RU.IsDel == false && RU.TeamName == TeamName
                                 select new SendEmailList
                                 {
                                     Name = RU.Name,
                                     Email = RU.Email,
                                     RegistrationNo = RU.RegistrationNumber

                                 }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return sendEmailList;
        }
        public List<SendSMSList> SenderSMSList(int UserId, string TeamName)
        {
            List<SendSMSList> sendSMSList = new List<SendSMSList>();
            try
            {
                sendSMSList = (from RU in _db.ResgisterUser
                               where RU.IsDel == false && RU.TeamName == TeamName
                               select new SendSMSList
                               {
                                   Name = RU.Name,
                                   PhoneNo = RU.Phone,
                                   RegistrationNo = RU.RegistrationNumber

                               }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return sendSMSList;
        }

        public List<NoticeGridView> getAdminNotices()
        {
            List<int> UserIds = _db.Users.Where(x => RoleIds.Contains(x.roleId) && x.IsDel == false).Select(y => y.Id).ToList();
            List<NoticeGridView> notices = new List<NoticeGridView>();
            try
            {
                List<Playlist> playlists = _db.Playlist.Where(x => x.IsDel == false).ToList();
                List<NoticePlaylist> noticePlaylists = _db.NoticePlaylist.Where(x => x.IsDel == false && x.NoticeId != null).ToList();
                notices = (from N in _db.Notice
                           join C in _db.Category on N.CategoryId equals C.Id
                           where N.IsDel == false && UserIds.Contains(N.CreatedBy.Value)
                           orderby N.Id descending
                           select new NoticeGridView
                           {
                               Id = N.Id,
                               Category = C.Name,
                               Content = N.Content,
                               Duration = N.Duration.ToString() + " s",
                               Start = N.StartDate.ToShortDateString() + " " + N.StartTime.Hours.ToString() + ":" + N.StartTime.Minutes.ToString(),
                               End = N.EndDate.ToShortDateString() + " " + N.EndTime.Hours.ToString() + ":" + N.EndTime.Minutes.ToString(),
                               Name = N.Name,
                               Playlist = getPlayListName(N.Id, playlists, noticePlaylists),
                               IsActive = N.IsActive
                           }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            return notices;
        }
    }
}