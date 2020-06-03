using ConnectMedia.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.IRepositories
{
    public interface IBuildingRepository
    {
        List<Building> getBuildingList(int userId);
        Building getBuildingDetail(int id);
        string AddEditBuilding(Building building);
        string Delete(int Id, int userId);
    }
}
