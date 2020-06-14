using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using ConnectMedia.Controllers;
using ConnectMedia.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MaxTVMedia.Controllers
{
    // [Authorize(AuthenticationSchemes= CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize]
    public class NoticeController : BaseController
    {

        readonly INoticeService _noticeServices;
        IPlaylistService _playlistService;
        IVideosService _videoService;
        readonly ISettingService _settingService;
        readonly ILogger<NoticeController> _logger;
        readonly IClassifiedService _classifiedServices;
        public NoticeController(ILogger<NoticeController> logger, INoticeService noticeServices, ISettingService settingService,
            IPlaylistService playlistService, IClassifiedService classifiedServices, IVideosService videoService)
        {
            _logger = logger;
            this._settingService = settingService;
            this._noticeServices = noticeServices;
            this._playlistService = playlistService;
            this._classifiedServices = classifiedServices;
            this._videoService = videoService;
        }
        public async Task<IActionResult> Index()
        {
            List<GetRunningNoticeClassifiedDTO> transfer = new List<GetRunningNoticeClassifiedDTO>();
            var data = _playlistService.GetAllPlaylist(getCurrentUserId()).Select(x => x.Id)?.ToList();

            await Task.Run(() =>
            {

                var playList = _playlistService.GetPlayListRunningSlots(2).Where(x => data.Contains(x.PlaylistId))?.ToList(); //Get Activate Slot Data

                var currentDate = DateTime.UtcNow.AddHours(5);
                var Date = currentDate.ToShortDateString();
                var currentTime = currentDate.TimeOfDay;
                var currentMonth = currentDate.Month;
                var currentDay = currentDate.DayOfWeek;

                foreach (var item in playList)
                {
                    GetRunningNoticeClassifiedDTO obj = new GetRunningNoticeClassifiedDTO();
                    obj.Notices = new List<NoticeDTO>();
                    obj.Classifieds = new List<ClassifiedDTO>();
                    var runningNoticeClassifiedlist = _playlistService.GetRunningNoticeClassified(item.PlaylistId);
                    foreach (var item1 in runningNoticeClassifiedlist)
                    {
                        var model = _playlistService.GetNoticePlaylistbyId(item1.EntityId);

                        if (model.ClassifiedId == null && model.NoticeId != null)
                        {
                            //Check Notice
                            var noticeDetails = _noticeServices.getNoticeDetail(model.NoticeId.Value);
                            if(noticeDetails.StartDate.Date >=currentDate.Date || currentDate.Date <= noticeDetails.EndDate.Date)
                            {
                                if (noticeDetails.StartTime.Hours == currentTime.Hours && noticeDetails.StartTime.Minutes == currentTime.Minutes)
                                {
                                    obj.Notices.Add(noticeDetails);
                                }

                            }

                        }
                        else if (model.NoticeId == null)
                        {
                            //Check Classified
                            var classifiedDetails = _classifiedServices.getClassifiedDetail(model.ClassifiedId.Value);
                            if (classifiedDetails.Start.Date >= currentDate.Date || currentDate.Date <= classifiedDetails.End.Date)
                            {
                                obj.Classifieds.Add(classifiedDetails);
                            }
                        }

                    }
                    transfer.Add(obj);


                }


            });
   
            //var data = GetCurrentPlaylist();
            return View(transfer);
        }
        public IActionResult Notices()
        {
            int UserId = this.getCurrentUserId();
            List<NoticeGridView> notices = _noticeServices.getNotices(UserId);
            return View(notices);
        }

        [HttpGet]
        public IActionResult Create(int id, int template)
        {
            try
            {
                NoticeViewModel noticeViewModel = new NoticeViewModel();
                int UserId = id;
                if (UserId > 0)
                {
                    noticeViewModel.noticeDTO = _noticeServices.getNoticeDetail(UserId);
                }
                else if (template > 0)
                {
                    noticeViewModel.noticeDTO = _noticeServices.TemplateDetail(template);
                }
                noticeViewModel.categoryDropdownList = BindCategoryDropdown();
                noticeViewModel.playlistDropdownList = PlaylistDropdown();
                return View(noticeViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(NoticeDTO noticeDTO)
        {
            NoticeViewModel noticeViewModel = new NoticeViewModel();
            if (ModelState.IsValid)
            {
                noticeDTO.entryBy = this.getCurrentUserId();
                bool IsSaved = _noticeServices.AddEditNotice(noticeDTO);
                if (IsSaved)
                    return RedirectToAction("Notices");
            }
            noticeViewModel.noticeDTO = noticeDTO;
            noticeViewModel.categoryDropdownList = BindCategoryDropdown();
            noticeViewModel.playlistDropdownList = PlaylistDropdown();
            return View(noticeViewModel);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            int NoticeId = id;
            if (NoticeId > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _noticeServices.Delete(NoticeId, CurrentUserId);

            }
            return RedirectToAction("Notices");
        }

        [HttpGet]
        public IActionResult Active(int id)
        {
            int NoticeId = id;
            if (NoticeId > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _noticeServices.Active(NoticeId, CurrentUserId);
            }
            return RedirectToAction("Notices");
        }

        public IActionResult CreateTemplate()
        {
            List<Template> template = _settingService.Templates(getCurrentUserId());
            return View(template);
        }

        public IActionResult ViewDocuments()
        {
            int UserId = this.getCurrentUserId();
            List<UploadFile> uploadFiles = _noticeServices.GetUserUploadFile(UserId);
            return View(uploadFiles);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePdf(int id)
        {
            try
            {
                PDFDTO pdfDTO = new PDFDTO();
                int UserId = id;
                if (UserId > 0)
                {
                    UploadFile upload = _noticeServices.getDocDetail(UserId);
                    pdfDTO.Id = upload.Id;
                    pdfDTO.Title = upload.Title;
                    pdfDTO.Desc = upload.Desc;
                }
                return View(pdfDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePdf(PDFDTO pdfDTO)
        {
            if (ModelState.IsValid)
            {
                if (pdfDTO.pdfFile != null && pdfDTO.pdfFile.Length > 0 && pdfDTO.pdfFile.FileName.Contains(".pdf"))
                {
                    pdfDTO.enteryBy = getCurrentUserId();
                    string Message = await _noticeServices.SavePDF(pdfDTO);
                    ModelState.AddModelError("CSV Upload!", Message);
                }
                else
                    ModelState.AddModelError("CSV Upload!", "Please upload file or in correct format");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateDoc(int id)
        {
            try
            {
                DocDTO docDTO = new DocDTO();
                int UserId = id;
                if (UserId > 0)
                {
                    UploadFile upload = _noticeServices.getDocDetail(UserId);
                    docDTO.Id = upload.Id;
                    docDTO.Title = upload.Title;
                    docDTO.Desc = upload.Desc;
                }
                return View(docDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoc(DocDTO docDTO)
        {
            if (ModelState.IsValid)
            {
                if (docDTO.docFile != null && docDTO.docFile.Length > 0 && docDTO.docFile.FileName.Contains(".doc"))
                {
                    docDTO.enteryBy = getCurrentUserId();
                    string Message = await _noticeServices.SaveDoc(docDTO);
                    ModelState.AddModelError("CSV Upload!", Message);
                }
                else
                    ModelState.AddModelError("CSV Upload!", "Please upload file or in correct format");
            }

            return View();
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
        [HttpGet]
        public IActionResult Preview(int id)
        {
           
            NoticeDTO noticeDTO = new NoticeDTO();
            int UserId = id;
            if (UserId > 0)
            {
                noticeDTO = _noticeServices.getNoticeDetail(UserId);
                ViewBag.CurrrentNotice = _videoService.GetAllVideos().Where(x => x.Date.ToShortDateString() == DateTime.UtcNow.ToShortDateString())?.Select(x=>x.FileName).FirstOrDefault();
            }

            return View(noticeDTO);
        }
        public List<SelectListItem> PlaylistDropdown()
        {
            List<Playlist> playlists = _noticeServices.PlaylistDropdown(this.getCurrentUserId());
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
        [HttpGet]
        public async Task<IActionResult> DownloadFile(int Id)
        {
            try
            {
                string contentType = string.Empty;
                var memory = new MemoryStream();
                UploadFile upload = new UploadFile();
                int UserId = Id;
                if (UserId > 0)
                {
                    upload = _noticeServices.getDocDetail(UserId);

                    using (var stream = new FileStream(upload.FilePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    new FileExtensionContentTypeProvider().TryGetContentType(upload.FilePath, out contentType);
                    memory.Position = 0;
                }
                return File(memory, contentType, upload.FileName);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> PreviewFile(int Id)
        {
            try
            {
                string contentType = string.Empty;
                UploadFile upload = new UploadFile();
                int UserId = Id;
                if (UserId > 0)
                {
                    upload = _noticeServices.getDocDetail(UserId);
                }
                return View(upload);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> SendTeamEmail(int Id, string tn)
        {
            try
            {
                int CurrentUserId = getCurrentUserId();
                string Message = _noticeServices.SendTeamEmail(Id, tn, CurrentUserId);
                TempData["Success"] = Message;
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> SendTeamSMS(int Id, string tn)
        {
            try
            {
                int CurrentUserId = getCurrentUserId();
                string Message = _noticeServices.SendTeamSMS(Id, tn, CurrentUserId);
                TempData["Success"] = Message;
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public PartialViewResult GetTeamList(int Id)
        {
            int UserId = getCurrentUserId();
            NoticeSendEmail model = _noticeServices.TeamList(UserId);
            return PartialView("_TeamEmail", model);
        }

        public IActionResult ViewPromotion()
        {
            int UserId = this.getCurrentUserId();
            PromotionDTO promotion = _noticeServices.GetPromotionWithNotice(UserId);
            return View(promotion);
        }

       
    }
}