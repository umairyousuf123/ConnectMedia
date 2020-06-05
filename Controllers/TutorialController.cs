using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Enum;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConnectMedia.Controllers
{
    // (AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
    [Authorize(Roles = "Super Admin,Admin,Master")]
    public class TutorialController : BaseController
    {
        readonly ITutorialService _tutorialService;
        readonly ILogger<TutorialController> _logger;
        public TutorialController(ILogger<TutorialController> logger, ITutorialService tutorialService)
        {
            _logger = logger;
            this._tutorialService = tutorialService;
        }
        public IActionResult Index()
        {
            int UserId = this.getCurrentUserId();
            List<UploadFile> uploadFiles = _tutorialService.GetAllTutorial(UserId);
            return View(uploadFiles);
        }

        
        [HttpGet]
        public async Task<IActionResult> Create(int Id)
        {
            try
            {
                VideoDTO videoDTO = new VideoDTO();
                int UserId = Id;
                if (UserId > 0)
                {
                   UploadFile uploadFile = _tutorialService.GetTutorialById(Id);
                    if (uploadFile != null && uploadFile.UploadType == UploadDocumentType.Video.ToString())
                    {
                        videoDTO.Id = uploadFile.Id;
                        videoDTO.Title = uploadFile.Title;
                        videoDTO.Desc = uploadFile.Desc;
                        videoDTO.Sequence = uploadFile.Sequence;
                        videoDTO.Portion = uploadFile.Portion;
                        videoDTO.videoFile = GetFile(uploadFile.FilePath, uploadFile.FileName).Result;
                    }

                }
                return View(videoDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Create(VideoDTO videoDTO)
        {

            if (ModelState.IsValid)
            {
                if (videoDTO.videoFile != null && videoDTO.videoFile.Length > 0)
                {
                    videoDTO.enteryBy = getCurrentUserId();
                    string Message = await _tutorialService.AddEditTutorial(videoDTO);
                    ModelState.AddModelError("Video Upload!", Message);
                    return RedirectToAction("Tutorials");
                }
                else
                {
                    ModelState.AddModelError("Video Upload!", "Please upload file");
                }
            }
            return View();
        }
       
        [HttpGet]
        public IActionResult Tutorials()
        {
            int UserId = this.getCurrentUserId();
            List<UploadFile> uploadFiles = _tutorialService.GetAllTutorial(UserId);
            return View(uploadFiles);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            int TutorialId = id;
            if (TutorialId > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _tutorialService.Delete(TutorialId, CurrentUserId);

            }
            return RedirectToAction("Tutorials");
        }

        [HttpGet]
        public IActionResult Active(int id)
        {
            int NoticeId = id;
            if (NoticeId > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _tutorialService.Active(NoticeId, CurrentUserId);
            }
            return RedirectToAction("Notices");
        }


        private async Task<IFormFile> GetFile(string filepath, string fileName)
        {
            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(filepath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);

                }
                return new FormFile(memory, 0, memory.Length, "Name", fileName);
            }
            catch (Exception e)
            {
                memory.Dispose();
                throw;
            }
            finally
            {
                memory.Dispose();
            }

        }
    }
}