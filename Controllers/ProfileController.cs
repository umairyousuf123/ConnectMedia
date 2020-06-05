using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using ConnectMedia.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ConnectMedia.Controllers
{
    [Authorize(Roles = "Super Admin,Admin,Master")]
    // (AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)

    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileServices;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(ILogger<ProfileController> logger, IProfileService profileServices)
        {
            _logger = logger;
            this._profileServices = profileServices;
        }

        public IActionResult Index()
        {
            List<UserGridView> userGrid = new List<UserGridView>();
            userGrid = _profileServices.getAllUserDetail(getCurrentUserId());
            return View(userGrid);
        }

        [HttpGet]
        public IActionResult Changes(string id)
        {          
            try
            {
                ProfileViewModel profileDTO = new ProfileViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    int UserId = Convert.ToInt32(EncryptSecreteString.Decrypt(id));
                    if (UserId > 0)
                    {
                        profileDTO.UserDTO = _profileServices.getUserDetail(UserId);
                    }
                }
            
                profileDTO.RolesDropdownList = this.BindRoleDropdown();
                profileDTO.BuildingDropdownList = this.BindBuildingsDropdown();
                return View(profileDTO);
            }
            catch (Exception ex)

            {
                ModelState.AddModelError("Error", ex.Message);
                return RedirectToAction("Index", "Profile");
            }
        }

        [HttpPost]
        public IActionResult Changes(UserDTO userDTO)
        {
            ProfileViewModel profileDTO = new ProfileViewModel();

            if (ModelState.IsValid)
            {
                userDTO.entryBy = getCurrentUserId();
                bool Success = _profileServices.AddEditUser(userDTO);
                if (Success)
                {
                    ViewData["Success"] = "User Added Sucessfully";
                    return RedirectToAction("Index");
                }
            }
            profileDTO.UserDTO = userDTO;
            profileDTO.BuildingDropdownList = this.BindBuildingsDropdown();
            profileDTO.RolesDropdownList = this.BindRoleDropdown();
            return View(profileDTO);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            int UserId = Convert.ToInt32(EncryptSecreteString.Decrypt(id));
            int CurrentUserId = getCurrentUserId();
            if (UserId > 0)
                _profileServices.Delete(UserId, CurrentUserId);
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public IActionResult Active(string id)
        {
            int UserId = Convert.ToInt32(EncryptSecreteString.Decrypt(id));
            int CurrentUserId = getCurrentUserId();
            if (UserId > 0)
                _profileServices.Active(UserId, CurrentUserId);
            return RedirectToAction("Index", "Profile");
        }

        public List<SelectListItem> BindRoleDropdown()
        {
            List<Role> listOfRoles = _profileServices.roles(this.getCurrentUserRoleId());
            List<SelectListItem> DropdownRoles = new List<SelectListItem>();

            foreach (var ddListOfRoles in listOfRoles)
            {
                DropdownRoles.Add(new SelectListItem
                {
                    Value = ddListOfRoles.Id.ToString(),
                    Text = ddListOfRoles.RoleName
                });
            }
            return DropdownRoles;
        }
        public List<SelectListItem> BindBuildingsDropdown()
        {
            List<Building> listOfBuilding = _profileServices.buildings(this.getCurrentUserRoleId());
            List<SelectListItem> DropdownBuilding = new List<SelectListItem>();

            foreach (var ddListOfBuilding in listOfBuilding)
            {
                DropdownBuilding.Add(new SelectListItem
                {
                    Value = ddListOfBuilding.Id.ToString(),
                    Text = ddListOfBuilding.Name
                });
            }
            return DropdownBuilding;
        }

    }
}