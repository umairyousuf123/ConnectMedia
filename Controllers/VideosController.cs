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
    public class VideosController : BaseController
    {
        readonly IVideosService _videosService;
        readonly ILogger<VideosController> _logger;
        public VideosController(ILogger<VideosController> logger, IVideosService videosService)
        {
            _logger = logger;
            this._videosService = videosService;
        }
        public IActionResult Index()
        {
            var data = _videosService.GetAllVideos();
            return View("Index",data);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VideosDTO videoDTO)
        {

            if (ModelState.IsValid)
            {
                if (videoDTO.file == null || videoDTO.file.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Videos",
                            videoDTO.file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await videoDTO.file.CopyToAsync(stream);
                }
                videoDTO.FileName = path;

                _videosService.AddVideo(videoDTO);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var data = _videosService.GetVideos(Id);
            return View(data);
        }


        //[HttpGet]
        //public IActionResult Tutorials()
        //{
        //    int UserId = this.getCurrentUserId();
        //    List<UploadFile> uploadFiles = _tutorialService.GetAllTutorial(UserId);
        //    return View(uploadFiles);
        //}

        [HttpGet]
        public IActionResult Delete(int id)
        {
           _videosService.DeletVideo(id);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Active(int id)
        //{
        //    int NoticeId = id;
        //    if (NoticeId > 0)
        //    {
        //        int CurrentUserId = getCurrentUserId();
        //        _tutorialService.Active(NoticeId, CurrentUserId);
        //    }
        //    return RedirectToAction("Notices");
        //}


        //private async Task<IFormFile> GetFile(string filepath, string fileName)
        //{
        //    var memory = new MemoryStream();
        //    try
        //    {
        //        using (var stream = new FileStream(filepath, FileMode.Open))
        //        {
        //            await stream.CopyToAsync(memory);

        //        }
        //        return new FormFile(memory, 0, memory.Length, "Name", fileName);
        //    }
        //    catch (Exception e)
        //    {
        //        memory.Dispose();
        //        throw;
        //    }
        //    finally
        //    {
        //        memory.Dispose();
        //    }

        //}
    }
}