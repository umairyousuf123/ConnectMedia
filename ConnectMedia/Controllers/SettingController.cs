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
    [Authorize(Roles = "Super Admin,Master")]
    public class SettingController : BaseController
    {
        private readonly INoticeService _noticeServices;
        private readonly ISettingService _settingService;
        private readonly ILogger<SettingController> _logger;

        public SettingController(ILogger<SettingController> logger, INoticeService noticeServices, ISettingService settingService)
        {
            _logger = logger;
            this._noticeServices = noticeServices;
            this._settingService = settingService;

        }

        #region CSV User Add, Update and Delete
        public IActionResult Index()
        {
            List<Template> template = _settingService.Templates(getCurrentUserId());
            return View(template);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            try
            {
                TemplateViewModel templateViewModel = new TemplateViewModel();
                int UserId = id;
                if (UserId > 0)
                {
                    templateViewModel.template = _settingService.GetTemplateDetail(UserId);
                }
                templateViewModel.categoryDropdownList = BindCategoryDropdown();
                return View(templateViewModel);
            }
            catch (Exception ex)

            {
                ModelState.AddModelError("Url", ex.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Template template)
        {
            TemplateViewModel templateViewModel = new TemplateViewModel();
            if (ModelState.IsValid)
            {
                if (template.Id > 0)
                    template.UpdatedBy = this.getCurrentUserId();
                else
                    template.CreatedBy = this.getCurrentUserId(); template.CreatedOn = DateTime.Now;
                bool IsSaved = _settingService.AddEdit(template);
                if (IsSaved)
                    return RedirectToAction("Index");
            }
            templateViewModel.template = template;
            templateViewModel.categoryDropdownList = BindCategoryDropdown();
            return View(templateViewModel);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _settingService.Delete(id, CurrentUserId);
            }
            return RedirectToAction("Index", "News");
        }
        private List<SelectListItem> BindCategoryDropdown()
        {
            List<Category> Categories = _noticeServices.Categories();
            List<SelectListItem> DropdownRoles = new List<SelectListItem>();

            foreach (var category in Categories)
            {
                DropdownRoles.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            return DropdownRoles;
        }

        #endregion

        #region  Upload All type of Files
        [HttpGet]
        public IActionResult ChangeLogo()
        {
            string imagePath = _settingService.RetriveLogo();
            ViewBag.Message = imagePath;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeLogo(ImageDTO imageDTO)
        {
            if (ModelState.IsValid)
            {
                if (imageDTO.imgFile != null && imageDTO.imgFile.Length > 0)
                {
                    imageDTO.enteryBy = getCurrentUserId();
                    string Message = await _settingService.SaveLogo(imageDTO);
                    ModelState.AddModelError("Image Upload!", Message);

                }
                else
                {
                    ModelState.AddModelError("Image Upload!", "Please upload file");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UploadCSV()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCSV(CSVDTO csvDTO)
        {
            if (ModelState.IsValid)
            {
                if (csvDTO.csvFile != null && csvDTO.csvFile.Length > 0 && csvDTO.csvFile.FileName.Contains(".csv"))
                {
                    csvDTO.enteryBy = getCurrentUserId();
                    string Message = await _settingService.SaveCSV(csvDTO);
                    return RedirectToAction("Teamlist", "Setting");
                }
                else
                    ModelState.AddModelError("CSV Upload!", "Please upload file or in correct format");
            }

            return View();
        }
        #endregion

        #region CSV user email List

        [HttpGet]
        public async Task<IActionResult> CSVList(string tn)
        {
            int UserId = getCurrentUserId();
            List<ResgisterUser> resgister = new List<ResgisterUser>();
            resgister = await _settingService.CSVList(UserId, tn);
            return View(resgister);
        }

        [HttpGet]
        public async Task<IActionResult> AddEmailUser(int id)
        {
            ResgisterUser resgister = new ResgisterUser();
            if (id > 0)
            {
                resgister = await _settingService.getEmailUserDetail(id);
            }
            return View(resgister);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmailUser(ResgisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                if (registerUser.Id > 0)
                    registerUser.UpdatedBy = getCurrentUserId();
                else
                    registerUser.CreatedBy = getCurrentUserId(); registerUser.CreatedOn = DateTime.Now;

                await _settingService.createUpdateEmailUser(registerUser);
            }
            return RedirectToAction("Teamlist", "Setting");
        }

        [HttpGet]
        public async Task<IActionResult> Teamlist()
        {
            int CurrentUserId = getCurrentUserId();
            List<string> TeamList = _settingService.TeamList(CurrentUserId);
            return View(TeamList);
        }

        #endregion

    }
}