using ConnectMedia.Common;
using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Enum;
using ConnectMedia.Common.Helper;
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
    public class ClassifiedRepository : IClassifiedRepository
    {
        readonly IConfiguration _configuration;
        ConnectMediaDB _db;
        readonly ILogger<ClassifiedRepository> _logger;
        readonly int AllowRole = 3;

        public ClassifiedRepository(IConfiguration configuration, ConnectMediaDB db, ILogger<ClassifiedRepository> logger)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }
        public List<ClassifiedGridView> getClassifiedList(int UserId)
        {
            int roleid = _db.Users.Where(x => x.Id == UserId).FirstOrDefault().roleId;
            List<ClassifiedGridView> userGridViews;
            if (roleid == 1 || roleid == 2)
            {
                List<Playlist> playlists = _db.Playlist.Where(x => x.IsDel == false).ToList();
                List<NoticePlaylist> classifiedPlaylists = _db.NoticePlaylist.Where(x => x.IsDel == false && x.ClassifiedId != null).ToList();

                userGridViews = (from C in _db.Classified
                                 join U in _db.Users on C.CreatedBy equals U.Id
                                 where C.IsDel == false
                                 orderby C.Id descending
                                 select new ClassifiedGridView
                                 {
                                     Id = EncryptSecreteString.Encrypt(C.Id.ToString()),
                                     Title = C.Title,
                                     Content = C.Content,
                                     Playlist = getPlaylistName(C.Id, playlists, classifiedPlaylists),
                                     ContactNumber = C.ContactNumber,
                                     Status = C.Status,
                                     PostedBy = U.firstName + " " + U.lastName,
                                     Start = C.Start.ToShortDateString(),
                                     End = C.End.ToShortDateString()
                                 }).ToList();
            }
            else
            {
                List<Playlist> playlists = _db.Playlist.Where(x => x.IsDel == false && x.CreatedBy == UserId).ToList();
                List<NoticePlaylist> classifiedPlaylists = _db.NoticePlaylist.Where(x => x.IsDel == false && x.CreatedBy == UserId && x.ClassifiedId != null).ToList();

                userGridViews = (from C in _db.Classified
                                 join U in _db.Users on C.CreatedBy equals U.Id
                                 where C.IsDel == false && C.CreatedBy == UserId
                                 orderby C.Id descending
                                 select new ClassifiedGridView
                                 {
                                     Id = EncryptSecreteString.Encrypt(C.Id.ToString()),
                                     Title = C.Title,
                                     Content = C.Content,
                                     Playlist = getPlaylistName(C.Id, playlists, classifiedPlaylists),
                                     ContactNumber = C.ContactNumber,
                                     Status = C.Status,
                                     PostedBy = U.firstName + " " + U.lastName,
                                     Start = C.Start.ToShortDateString(),
                                     End = C.End.ToShortDateString()
                                 }).ToList();
            }

            return userGridViews;
        }
        public ClassifiedDTO getClassifiedDetail(int id)
        {
            ClassifiedDTO classifiedDTO = new ClassifiedDTO();
            IEnumerable<int> NoticePlaylists = _db.NoticePlaylist.Where(x => x.ClassifiedId == id && x.IsDel == false).Select(x => x.PlaylistId).ToList();
            Classified classified = _db.Classified.SingleOrDefault(b => b.Id == id && b.IsDel == false);
            if (classified != null)
            {
                DataCopier.Copy(classified, classifiedDTO);
            }
            classifiedDTO.PlayList = NoticePlaylists;

            return classifiedDTO;
        }

        public bool AddEditClassified(ClassifiedDTO classifiedDTO)
        {
            int Success;
            Classified classified = new Classified();
            List<NoticePlaylist> classifiedPlaylists = new List<NoticePlaylist>();
            try
            {
                if (classifiedDTO.Id > 0)
                {
                    classifiedPlaylists = _db.NoticePlaylist.Where(x => x.ClassifiedId == classifiedDTO.Id && x.IsDel == false).ToList();
                    if (classifiedPlaylists.Count() > 0)
                    {
                        classifiedPlaylists.ForEach(a => { a.IsDel = true; a.UpdatedOn = DateTime.Now; a.UpdatedBy = classifiedDTO.entryBy; });
                        Success = _db.SaveChanges();
                        classifiedPlaylists.Clear();
                    }
                }
                DataCopier.Copy(classifiedDTO, classified);
                if (classified.Id > 0)
                {
                    Classified Oldclassified = _db.Classified.SingleOrDefault(b => b.Id == classified.Id && b.IsDel == false);
                    Oldclassified.Title = classified.Title;
                    Oldclassified.Content = classified.Content;
                    Oldclassified.Playlist = classified.Playlist;
                    Oldclassified.Name = classified.Name;
                    Oldclassified.ContactNumber = classified.ContactNumber;
                    Oldclassified.Status = Status.Approval.ToString();
                    Oldclassified.Start = classified.Start;
                    Oldclassified.End = classified.End;
                    Oldclassified.UpdatedOn = DateTime.UtcNow;
                    Oldclassified.UpdatedBy = classified.UpdatedBy;
                    _db.Classified.Attach(Oldclassified);
                    _db.Entry(Oldclassified).State = EntityState.Modified;
                }
                else
                {

                    classified.Status = Status.Approval.ToString();
                    classified.CreatedOn = DateTime.Now;
                    classified.CreatedBy = classifiedDTO.entryBy;
                    _db.Classified.Add(classified);
                }
                Success = _db.SaveChanges();
                if (classified.Id > 0 && classifiedDTO.PlayList.Count() > 0)
                {
                    foreach (int classifiedObj in classifiedDTO.PlayList)
                    {
                        NoticePlaylist classfiedNotice = new NoticePlaylist();
                        classfiedNotice.Id = 0;
                        classfiedNotice.ClassifiedId = classified.Id;
                        classfiedNotice.PlaylistId = classifiedObj;
                        classfiedNotice.CreatedBy = classifiedDTO.entryBy;
                        classfiedNotice.CreatedOn = DateTime.Now;
                        classifiedPlaylists.Add(classfiedNotice);
                    }
                    _db.NoticePlaylist.AddRange(classifiedPlaylists);
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
            Classified classified = new Classified();
            try
            {
                classified = _db.Classified.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                classified.IsDel = true;
                classified.UpdatedOn = DateTime.UtcNow;
                classified.UpdatedBy = CurrentUserId;
                _db.Classified.Attach(classified);
                _db.Entry(classified).State = EntityState.Modified;
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
            Classified classified = new Classified();
            try
            {
                classified = _db.Classified.SingleOrDefault(x => x.Id == Id && x.IsDel == false && x.IsActive == true);
                classified.UpdatedOn = DateTime.UtcNow;
                classified.UpdatedBy = CurrentUserId;
                _db.Classified.Attach(classified);
                _db.Entry(classified).State = EntityState.Modified;
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

        private static string getPlaylistName(int ClassifiedId, List<Playlist> playlists, List<NoticePlaylist> classifiedPlaylists)
        {
            IEnumerable<int> playlistIds = classifiedPlaylists.Where(x => x.ClassifiedId == ClassifiedId && x.IsDel == false).Select(x => x.PlaylistId).ToList();
            IEnumerable<string> PlaylistNames = playlists.Where(x => playlistIds.Contains(x.Id)).Select(x => x.Name).ToList();
            return string.Join(", ", PlaylistNames);
        }
    }
}
