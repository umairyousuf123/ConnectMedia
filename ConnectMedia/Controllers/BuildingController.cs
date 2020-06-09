using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ConnectMedia.Controllers
{
//    // (AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
[Authorize]
    public class BuildingController : BaseController
    {
        private readonly IBuildingService _buildingService;
        private readonly ILogger<BuildingController> _logger;
        public BuildingController(ILogger<BuildingController> logger, IBuildingService buildingService)
        {
            _logger = logger;
            this._buildingService = buildingService;
        }
        public IActionResult Index()
        {
            List<Building> buildingGrid = new List<Building>();
            buildingGrid = _buildingService.getBuildingList(getCurrentUserId());
            return View(buildingGrid);
        }
        [HttpGet]
        public IActionResult Create(int Id)
        {
            try
            {
                Building building = new Building();
                if (Id > 0)
                {
                    building = _buildingService.getBuildingDetail(Id);
                }
                return View(building);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Create(Building building)
        {
            if (ModelState.IsValid)
            {
                if (building.Id > 0)
                    building.UpdatedBy = getCurrentUserId();
                else
                {
                    building.CreatedBy = getCurrentUserId();
                    building.CreatedOn = DateTime.Now;
                }
                building.Key = _buildingService.GeneratePassword(true, true, true, true, 64).Substring(0,4);
                _buildingService.AddEditBuilding(building);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _buildingService.Delete(Id, CurrentUserId);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddFromUser(Building building)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                if (building.Id > 0)
                    building.UpdatedBy = getCurrentUserId();
                else
                {
                    building.CreatedBy = getCurrentUserId();
                    building.CreatedOn = DateTime.Now;
                }
                TempData["Success"] = _buildingService.AddEditBuilding(building);
            }
            return RedirectToAction("Changes", "Profile");

        }
 
    }
}