using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConnectMedia.Services
{
    public class PlaylistService : IPlaylistService
    {
        IPlaylistRepository _playlistRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<PlaylistService> _logger;

        public PlaylistService(ILogger<PlaylistService> logger, IPlaylistRepository playlistRepository, IConfiguration configuration)
        {
            _logger = logger;
            this._playlistRepository = playlistRepository;
            this._configuration = configuration;
        }
        public List<PlaylistGridView> GetAllPlaylist(int Userid)
        {
            List<PlaylistGridView> playlistGrid = _playlistRepository.GetAllPlaylist(Userid);
            return playlistGrid;
        }

        public List<PlaylistBuilding> GetPlayListBuilding(int Userid, string key = "")
        {
            List<PlaylistBuilding> playlistBuilding = _playlistRepository.GetPlaylistBuildings(Userid, key);
            return playlistBuilding;
        }
        public PlaylistDTO GetPlaylist(int Id)
        {
            PlaylistDTO playlist = _playlistRepository.GetPlaylist(Id);
            return playlist;
        }
        public string AddEditPlaylist(PlaylistDTO playlist)
        {
            string isSaved = _playlistRepository.AddEditPlaylist(playlist);
            return isSaved;
        }
        public bool Delete(int id, int CurrentUserId)
        {
            bool isDeleted = false;
            isDeleted = _playlistRepository.Delete(id, CurrentUserId);
            return isDeleted;
        }
        public bool Active(int id, int CurrentUserId)
        {
            bool isActive = false;
            isActive = _playlistRepository.Active(id, CurrentUserId);
            return isActive;
        }
        public List<Building> GetUserAllowBuilding(int UserId)
        {
            List<Building> buildings = _playlistRepository.GetUserAllowBuilding(UserId);
            return buildings;
        }  
        public List<NoticeDTO> PreviewPlaylistNotice(int playlistId)
        {
            List<NoticeDTO>  noticeDTOs = _playlistRepository.PreviewPlaylistNotice(playlistId);
            return noticeDTOs;
        }

        public List<PlayListRunningSlots> GetPlayListRunningSlots(int runningState)
        {
            return _playlistRepository.GetAllSlots(runningState);
        }

        public List<NoticePlaylist> GetNoticePlayList(int playListId)
        {
            return _playlistRepository.GetNoticePlaylist(playListId);
        }

        public int AddRunningNoticeClassified(RunningNoticeClassified model)
        {
            return _playlistRepository.AddRunningNoticeClassified(model);
        }
        public int UpdatePlayListRunningSlot(int runningState,int Id)
        {
            return _playlistRepository.UpdatePlayListRunningSlot(runningState,Id);
        }
        public bool DeleteRunningNoticeClassified(int Id)
        {
            return _playlistRepository.DeleteRunningNoticeClassified(Id);
        }

        public NoticePlaylist GetNoticePlaylistbyId(int Id)
        {
          return  _playlistRepository.GetNoticePlaylistbyId(Id);
        }

        public List<RunningNoticeClassified> GetRunningNoticeClassified(int playlistId)
        {
            return _playlistRepository.GetRunningNoticeClassified(playlistId);

        }

    }
}
