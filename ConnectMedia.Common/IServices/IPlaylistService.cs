using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IServices
{
    public interface IPlaylistService
    {
        List<PlaylistGridView> GetAllPlaylist(int UserId);
        PlaylistDTO GetPlaylist(int UserId);
        string AddEditPlaylist(PlaylistDTO playlist);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Building> GetUserAllowBuilding(int UserId);

        List<PlayListRunningSlots> GetPlayListRunningSlots(int runningState);

        List<NoticePlaylist> GetNoticePlayList(int playListId);

        int AddRunningNoticeClassified(RunningNoticeClassified model);
        int UpdatePlayListRunningSlot(int runningState, int Id);

        bool DeleteRunningNoticeClassified(int Id);

        List<RunningNoticeClassified> GetRunningNoticeClassified(int playlistId);

        NoticePlaylist GetNoticePlaylistbyId(int Id);

        List<NoticeDTO> PreviewPlaylistNotice(int playlistId);

    }
}
