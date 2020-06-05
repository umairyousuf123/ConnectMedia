using ConnectMedia.Common.DTO;
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
using System.Threading.Tasks;

namespace ConnectMedia.Controllers
{
    // (AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
    [Authorize]
    public class PlaylistController : BaseController
    {
        private readonly IPlaylistService _playlistService;
        private readonly ILogger<PlaylistController> _logger;
        public PlaylistController(ILogger<PlaylistController> logger, IPlaylistService playlistService)
        {
            _logger = logger;
            this._playlistService = playlistService;
        }

        public IActionResult Index()
        {
            List<PlaylistGridView> playlistGrid = new List<PlaylistGridView>();
            playlistGrid = _playlistService.GetAllPlaylist(getCurrentUserId());
            return View(playlistGrid);
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            PlaylistViewModel playlistView = new PlaylistViewModel();
            try
            {
                if (Id > 0)
                {
                    playlistView.playlistDTO = _playlistService.GetPlaylist(Id);
                }
                playlistView.BuildingDropdownList = this.BindBuildingsDropdown();
               
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
            return View(playlistView);
        }

        [HttpPost]
        public IActionResult Create(PlaylistDTO playlistDTO)
        {
            if (ModelState.IsValid)
            {
                playlistDTO.entryBy = getCurrentUserId();
                string isInserted = _playlistService.AddEditPlaylist(playlistDTO);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            int UserId = id;
            int CurrentUserId = getCurrentUserId();
            if (UserId > 0)
                _playlistService.Delete(UserId, CurrentUserId);
            return RedirectToAction("Index", "Playlist");
        }

        [HttpGet]
        public IActionResult Active(int id)
        {
            int UserId = id;
            int CurrentUserId = getCurrentUserId();
            if (UserId > 0)
                _playlistService.Active(UserId, CurrentUserId);
            return RedirectToAction("Index", "Profile");
        }
        [HttpGet]
        public async Task<IActionResult> PreviewPlaylist(int Id)
        {
            try
            {
                List<NoticeDTO> noticeDTOs = new List<NoticeDTO>();
                int UserId = Id;
                if (UserId > 0)
                {
                    noticeDTOs = _playlistService.PreviewPlaylistNotice(Id);
                }
                return View(noticeDTOs);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }

        public List<SelectListItem> BindBuildingsDropdown()
        {
            List<Building> listOfBuilding = _playlistService.GetUserAllowBuilding(this.getCurrentUserId());
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