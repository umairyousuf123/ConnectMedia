using ConnectMedia.Common.Database;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectMedia.Repository
{
    public class BuildingRepository : IBuildingRepository
    {
        ConnectMediaDB _db;
        readonly ILogger<BuildingRepository> _logger;
        public BuildingRepository(ILogger<BuildingRepository> logger, ConnectMediaDB db)
        {
            this._db = db;
            this._logger = logger;
        }
        public List<Building> getBuildingList(int userId)
        {
            List<Building> buildings = _db.Building.Where(x => x.IsDel == false).OrderByDescending(x=>x.Id).ToList();
            return buildings;
        }
        public Building getBuildingDetail(int id)
        {
            Building building = _db.Building.SingleOrDefault(b => b.Id == id && b.IsDel == false);
            return building;
        }
        public string AddEditBuilding(Building building)
        {
            string ErrorMessage = string.Empty;
            try
            {
                if (building.Id > 0)
                {
                    _db.Building.Attach(building);
                    _db.Entry(building).State = EntityState.Modified;
                }
                else
                {
                    _db.Building.Add(building);
                }
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Building added sucessfully " : "Building not added";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                _logger.LogError(ErrorMessage);
               
            }
            return ErrorMessage;
        }
        public string Delete(int Id, int CurrentUserId)
        {
            string ErrorMessage = string.Empty;
            Building building = new Building();
            try
            {
                building = _db.Building.SingleOrDefault(x => x.Id == Id && x.IsDel == false);
                building.IsDel = true;
                building.UpdatedOn = DateTime.UtcNow;
                building.UpdatedBy = CurrentUserId;
                _db.Building.Attach(building);
                _db.Entry(building).State = EntityState.Modified;
                int Success = _db.SaveChanges();
                ErrorMessage = (Success > 0) ? "Building deleted sucessfully " : "Building not deleted";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
            return ErrorMessage;
        }
    }
}
