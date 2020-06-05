using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
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
    public class PlaylistRepository : IPlaylistRepository
    {
        readonly IConfiguration _configuration;
        ConnectMediaDB _db;
        readonly ILogger<PlaylistRepository> _logger;
        readonly int AllowRole = 2;
        public PlaylistRepository(ILogger<PlaylistRepository> logger, IConfiguration configuration, ConnectMediaDB db, IProfileRepository profileRepository)
        {
            this._configuration = configuration;
            this._db = db;
            this._logger = logger;
        }
        public List<PlaylistGridView> GetAllPlaylist(int UserId)
        {
            List<PlaylistGridView> playlistGridViews = new List<PlaylistGridView>();
            int roleid = _db.Users.Where(x => x.Id == UserId).FirstOrDefault().roleId;

            List<Building> buildings = _db.Building.Where(t => t.IsDel == false).ToList();

            if (roleid <= AllowRole)
            {
                List<PlaylistBuilding> playlistBuildings = _db.PlaylistBuilding.Where(t => t.IsDel == false).ToList();
                playlistGridViews = (from P in _db.Playlist
                                     where P.IsDel == false
                                     orderby P.Id descending
                                     select new PlaylistGridView
                                     {
                                         Id = P.Id,
                                         PlaylistName = P.Name,
                                         BuildingName = getBuildingName(P.Id, buildings, playlistBuildings)
                                     }).ToList();
            }
            else
            {
                List<PlaylistBuilding> playlistBuildings = _db.PlaylistBuilding.Where(t => t.IsDel == false && t.CreatedBy == UserId).ToList();
                playlistGridViews = (from P in _db.Playlist
                                     where P.IsDel == false && P.CreatedBy == UserId
                                     orderby P.Id descending
                                     select new PlaylistGridView
                                     {
                                         Id = P.Id,
                                         PlaylistName = P.Name,
                                         BuildingName = getBuildingName(P.Id, buildings, playlistBuildings)
                                     }).ToList();
            }

            return playlistGridViews;
        }
        public PlaylistDTO GetPlaylist(int id)
        {
            PlaylistDTO playlistDTO = new PlaylistDTO();
            IEnumerable<int> buildingIds = _db.PlaylistBuilding.Where(x => x.PlaylistId == id && x.IsDel == false).Select(x => x.BuildingId).ToList();
            Playlist playlist = _db.Playlist.SingleOrDefault(x => x.Id == id && x.IsDel == false);
            playlistDTO.Id = playlist.Id;
            playlistDTO.Name = playlist.Name;
            playlistDTO.BuildingId = buildingIds;
            return playlistDTO;
        }
        public string AddEditPlaylist(PlaylistDTO playlistDTO)
        {
            int Success;
            string ErrorMessage = string.Empty;

            List<PlaylistBuilding> playlistBuildings = new List<PlaylistBuilding>();
            if (playlistDTO.Id > 0)
            {
                playlistBuildings = _db.PlaylistBuilding.Where(x => x.PlaylistId == playlistDTO.Id && x.IsDel == false).ToList();
                if (playlistBuildings.Count() > 0)
                {
                    playlistBuildings.ForEach(a => { a.IsDel = true; a.UpdatedOn = DateTime.Now; a.UpdatedBy = playlistDTO.entryBy; });
                    Success = _db.SaveChanges();
                    playlistBuildings.Clear();
                }
            }
            try
            {
                Playlist playlist = new Playlist();
                if (playlistDTO.Id > 0)
                {
                    playlist = _db.Playlist.Where(x => x.Id == playlistDTO.Id && x.IsDel == false).SingleOrDefault();
                    playlist.Id = playlistDTO.Id;
                    playlist.Name = playlistDTO.Name;
                    playlist.UpdatedOn = DateTime.UtcNow;
                    playlist.UpdatedBy = playlistDTO.entryBy;
                    _db.Playlist.Attach(playlist);
                    _db.Entry(playlist).State = EntityState.Modified;
                }
                else
                {
                    playlist.Id = playlistDTO.Id;
                    playlist.Name = playlistDTO.Name;
                    playlist.CreatedOn = DateTime.UtcNow;
                    playlist.CreatedBy = playlistDTO.entryBy;
                    _db.Playlist.Add(playlist);
                }

                Success = _db.SaveChanges();
                if (playlist.Id > 0 && playlistDTO.BuildingId.Count() > 0)
                {
                    foreach (int buildingId in playlistDTO.BuildingId)
                    {
                        PlaylistBuilding playlistBuilding = new PlaylistBuilding();
                        playlistBuilding.BuildingId = buildingId;
                        playlistBuilding.PlaylistId = playlist.Id;
                        playlistBuilding.CreatedBy = playlistDTO.entryBy;
                        playlistBuilding.CreatedOn = DateTime.Now;
                        playlistBuildings.Add(playlistBuilding);
                    }
                    _db.PlaylistBuilding.AddRange(playlistBuildings);
                    Success = _db.SaveChanges();

                }
                ErrorMessage = (Success > 0) ? "Playlist added sucessfully " : "Playlist not added";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorMessage = ex.Message.ToString();
            }
            return ErrorMessage;
        }
        public bool Delete(int Id, int CurrentUserId)
        {
            bool isDeleted = false;
            try
            {
                int Success = 0;

                Playlist playlist = _db.Playlist.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                playlist.IsDel = true;
                playlist.UpdatedOn = DateTime.UtcNow;
                playlist.UpdatedBy = CurrentUserId;
                _db.Playlist.Attach(playlist);
                _db.Entry(playlist).State = EntityState.Modified;
                Success = _db.SaveChanges();

                var playlistBuildings = _db.PlaylistBuilding.Where(x => x.PlaylistId == Id && x.IsDel == false).ToList();
                if (playlistBuildings.Count() > 0)
                {
                    Success = 0;
                    playlistBuildings.ForEach(a => { a.IsDel = true; a.UpdatedOn = DateTime.Now; a.UpdatedBy = CurrentUserId; });
                    Success = _db.SaveChanges();
                    playlistBuildings.Clear();
                }
                List<NoticePlaylist> noticePlaylists = _db.NoticePlaylist.Where(x => x.PlaylistId == Id && x.IsDel == false).ToList();
                if (noticePlaylists.Count() > 0)
                {
                    Success = 0;
                    noticePlaylists.ForEach(a => { a.IsDel = true; a.UpdatedOn = DateTime.Now; a.UpdatedBy = CurrentUserId; });
                    Success = _db.SaveChanges();
                    noticePlaylists.Clear();
                }
                isDeleted = (Success > 0) ? true : false;
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
                Playlist playlist = _db.Playlist.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                playlist.IsActive = true;
                playlist.UpdatedOn = DateTime.UtcNow;
                playlist.UpdatedBy = CurrentUserId;
                _db.Playlist.Attach(playlist);
                _db.Entry(playlist).State = EntityState.Modified;
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
        public List<Building> GetUserAllowBuilding(int UserId)
        {
            List<Building> buildings = new List<Building>();
            try
            {
                int roleid = _db.Users.Where(x => x.Id == UserId).FirstOrDefault().roleId;
                if (roleid <= AllowRole)
                {
                    buildings = _db.Building.Where(x => x.IsDel == false).ToList();
                }
                else
                {
                    User user = _db.Users.SingleOrDefault(x => x.IsDel == false && x.Id == UserId);
                    if (user != null)
                    {
                        int[] buildingId = user.BuildingIds.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                        buildings = _db.Building.Where(t => buildingId.Contains(t.Id)).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return buildings;
        }
        public List<NoticeDTO> PreviewPlaylistNotice(int playlistId)
        {
            List<NoticeDTO> noticeDTOs = new List<NoticeDTO>();
            try
            {
                noticeDTOs = (from P in _db.Playlist
                              join NP in _db.NoticePlaylist on P.Id equals NP.PlaylistId
                              join N in _db.Notice on NP.NoticeId equals N.Id
                              where P.Id == playlistId && P.IsDel == false && NP.IsDel == false && N.IsDel == false &&
                              P.IsActive == true && NP.IsActive == true && N.IsActive == true
                              select new NoticeDTO
                              {
                                  Name = N.Name,
                                  Duration = N.Duration,
                                  StartDate = N.StartDate,
                                  StartTime = N.StartTime,
                                  EndDate = N.EndDate,
                                  EndTime = N.EndTime,
                                  CategoryId = N.CategoryId,
                                  Content = N.Content,
                                  Expire = N.Expire,
                                  Id = N.Id,
                              }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return noticeDTOs;
        }
        private static string getBuildingName(int playistid, List<Building> buildings, List<PlaylistBuilding> playlistBuildings)
        {
            IEnumerable<int> buildingIds = playlistBuildings.Where(x => x.PlaylistId == playistid && x.IsDel == false).Select(x => x.BuildingId).ToList();
            IEnumerable<string> buildingNames = buildings.Where(x => buildingIds.Contains(x.Id)).Select(x => x.Name).ToList();
            return string.Join(", ", buildingNames);
        }

        public List<PlayListRunningSlots> GetAllSlots(int runningStateId)
        {

            return _db.playlistrunningslots.Where(x => x.RunningState == runningStateId)?.ToList();
        }

        public List<NoticePlaylist> GetNoticePlaylist(int playListId)
        {
            return _db.NoticePlaylist.Where(x => x.PlaylistId == playListId)?.ToList();
        }

        public NoticePlaylist GetNoticePlaylistbyId(int Id)
        {
            return _db.NoticePlaylist.Where(x => x.Id == Id)?.FirstOrDefault();
        }
        public int AddRunningNoticeClassified(RunningNoticeClassified model)
        {
            _db.RunningNoticeClassified.Add(model);
            _db.SaveChanges();
            return model.Id;
        }

        public int UpdatePlayListRunningSlot(int runningStateId,int Id)
        {
            var data = _db.playlistrunningslots.Where(x => x.Id == Id).FirstOrDefault();
             data.RunningState = runningStateId;
            _db.playlistrunningslots.Attach(data);
            _db.Entry(data).State = EntityState.Modified;
            _db.SaveChanges();
         
            return Id;
        }

        public bool DeleteRunningNoticeClassified(int Id)
        {
            bool isDeleted = false;
            try
            {
                var data = _db.RunningNoticeClassified.Where(x => x.EntityId == Id).FirstOrDefault();
                _db.RunningNoticeClassified.Remove(data);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                isDeleted = false;

            }
            return isDeleted;
        }

        public List<RunningNoticeClassified> GetRunningNoticeClassified(int playlistId)
        {
            return _db.RunningNoticeClassified.Where(x => x.PlayListId == playlistId)?.ToList();
            
        }
    }
}
