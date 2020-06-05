using ConnectMedia.Common.Model;
using System.Collections.Generic;

namespace ConnectMedia.Common.IServices
{
    public interface IBuildingService
    {
        List<Building> getBuildingList(int userId);
        Building getBuildingDetail(int id);
        string AddEditBuilding(Building building);
        string Delete(int Id, int userId);
        public string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
      int passwordSize);
    }
}
