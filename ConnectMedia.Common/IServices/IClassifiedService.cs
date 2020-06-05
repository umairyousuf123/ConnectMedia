using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IServices
{
    public interface IClassifiedService
    {
        List<ClassifiedGridView> getClassifiedList(int id);
        ClassifiedDTO getClassifiedDetail(int id);
        bool AddEditClassified(ClassifiedDTO classifiedDTO);
        bool Delete(int id, int CurrentUserId);
        bool Active(int id, int CurrentUserId);
        List<Playlist> PlaylistDropdown(int UserId);

    }
}
