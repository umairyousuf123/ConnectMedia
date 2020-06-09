using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ConnectMedia.Controllers
{
    public class WebViewController : Controller
    {
        readonly INoticeService _noticeServices;

        public WebViewController(INoticeService noticeServices)
        {
            this._noticeServices = noticeServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PreviewTv(string Key)
        {
            NoticeDTO noticeDTO = new NoticeDTO();

            int UserId = 2;
            if (UserId > 0)
            {
                noticeDTO = _noticeServices.getNoticeDetail(2);
            }
            ViewBag.Key = Key;
            return View("Preview", noticeDTO);

        }
    }
}