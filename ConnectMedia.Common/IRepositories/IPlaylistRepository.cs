using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IRepositories
{
    public interface IPlaylistRepository
    {
        List<PlaylistGridView> GetAllPlaylist(int Userid);

        List<PlaylistBuilding> GetPlaylistBuildings(int userId, string key);

        PlaylistDTO GetPlaylist(int Userid);
        string AddEditPlaylist(PlaylistDTO playlist);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Building> GetUserAllowBuilding(int UserId);

        List<PlayListRunningSlots> GetAllSlots(int runningStateId);

        List<NoticePlaylist> GetNoticePlaylist(int playListId);

        int AddRunningNoticeClassified(RunningNoticeClassified model);

        int UpdatePlayListRunningSlot(int runningStateId, int Id);

        bool DeleteRunningNoticeClassified(int Id);

        NoticePlaylist GetNoticePlaylistbyId(int Id);

        List<RunningNoticeClassified> GetRunningNoticeClassified(int playlistId);
        List<NoticeDTO> PreviewPlaylistNotice(int playlistId);


    }
}
