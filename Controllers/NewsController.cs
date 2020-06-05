using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConnectMedia.Controllers
{
    [Authorize]

    public class NewsController : BaseController
    {
        private readonly INewsService _newsServices;
        private readonly ILogger<AuthenticationController> _logger;
        public NewsController(ILogger<AuthenticationController> logger, INewsService newsServices)
        {
            this._logger = logger;
            this._newsServices = newsServices;
        }

        public IActionResult Index()
        {
            NewsUpdatesDTO newsUpdates = new NewsUpdatesDTO();
            newsUpdates.newsUpdates = _newsServices.getListOfNews(getCurrentUserId());
            return View(newsUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            News news = new News();
            if (id > 0)
            {
                news = await _newsServices.getUserDetail(id);
            }
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.Id > 0)
                    news.UpdatedBy = getCurrentUserId();
                else
                    news.CreatedBy = getCurrentUserId(); news.CreatedOn = DateTime.Now;

                await _newsServices.createUpdate(news);
            }

            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _newsServices.deleteNews(id, CurrentUserId);
            }
            return RedirectToAction("Index", "News");
        }
    }
}