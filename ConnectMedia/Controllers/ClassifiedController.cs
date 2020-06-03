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
    [Authorize]

    public class ClassifiedController : BaseController
    {
        private readonly IClassifiedService _classifiedService;
        private readonly ILogger<ClassifiedController> _logger;
        public ClassifiedController(ILogger<ClassifiedController> logger, IClassifiedService classifiedService)
        {
            _logger = logger;
            this._classifiedService = classifiedService;
        }
        public IActionResult Index()
        {
            List<ClassifiedGridView> userGrid = new List<ClassifiedGridView>();
            userGrid = _classifiedService.getClassifiedList(getCurrentUserId());
            return View(userGrid);
        }
        [HttpGet]
        public IActionResult Create(string Id)
        {
            try
            {
                ClassifiedViewModel classifiedViewModel = new ClassifiedViewModel();
                if (!string.IsNullOrEmpty(Id))
                {
                    int classifiedId = Convert.ToInt32(EncryptSecreteString.Decrypt(Id));
                    if (classifiedId > 0)
                    {
                        classifiedViewModel.classifiedDTO = _classifiedService.getClassifiedDetail(classifiedId);
                    }
                }
                classifiedViewModel.playlistDropdownList = PlaylistDropdown();
                return View(classifiedViewModel);
            }
            catch (Exception ex)

            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Create(ClassifiedDTO classifiedDTO)
        {
            try
            {
                ClassifiedViewModel classifiedViewModel = new ClassifiedViewModel();
                if (ModelState.IsValid)
                {
                    classifiedDTO.entryBy = this.getCurrentUserId();
                    _classifiedService.AddEditClassified(classifiedDTO);
                    return RedirectToAction("Index");
                }
                else
                {
                    classifiedViewModel.classifiedDTO = classifiedDTO;
                    classifiedViewModel.playlistDropdownList = PlaylistDropdown();
                }
           
                return View(classifiedViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            int classifiedId = Convert.ToInt32(EncryptSecreteString.Decrypt(id));
            if (classifiedId > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _classifiedService.Delete(classifiedId, CurrentUserId);

            }
            return RedirectToAction("Index");
        }
        [HttpPut]
        public IActionResult Active(string id)
        {
            int classifiedId = Convert.ToInt32(EncryptSecreteString.Decrypt(id));
            if (classifiedId > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _classifiedService.Active(classifiedId, CurrentUserId);
            }
            return RedirectToAction("Index");
        }

        public List<SelectListItem> PlaylistDropdown()
        {
            List<Playlist> playlists = _classifiedService.PlaylistDropdown(this.getCurrentUserId());
            List<SelectListItem> DropdownBuilding = new List<SelectListItem>();

            foreach (var ddListOfplaylists in playlists)
            {
                DropdownBuilding.Add(new SelectListItem
                {
                    Value = ddListOfplaylists.Id.ToString(),
                    Text = ddListOfplaylists.Name
                });
            }
            return DropdownBuilding;
        }
    }
}