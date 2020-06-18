using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ConnectMedia.Controllers
{
    public class WebViewController : BaseController
    {
        readonly INoticeService _noticeServices;
        IVideosService _videoService;
        readonly IPlaylistService _playlistService;
        readonly IClassifiedService _classifiedService;

        public  static string gKey = "";
        public WebViewController(INoticeService noticeServices, IVideosService videoService, IPlaylistService playlistService,
            IClassifiedService classifiedService)
        {
            this._noticeServices = noticeServices;
            this._videoService = videoService;
            this._playlistService = playlistService;
            this._classifiedService = classifiedService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PreviewTv(string Key)
        
        {
            
            NoticeDTO noticeDTO = new NoticeDTO();
            gKey = Key;
            List<GetRunningNoticeClassifiedDTO> transfer = new List<GetRunningNoticeClassifiedDTO>();
            var data = _playlistService.GetPlayListBuilding(getCurrentUserId(),Key).Select(x => x.PlaylistId)?.ToList();

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
                            if (noticeDetails.StartDate.Date >= currentDate.Date || currentDate.Date <= noticeDetails.EndDate.Date)
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
                            var classifiedDetails = _classifiedService.getClassifiedDetail(model.ClassifiedId.Value);
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
           
                ViewBag.CurrrentNotice = _videoService.GetAllVideos()?.Where(x => x.Date.ToShortDateString() == DateTime.UtcNow.ToShortDateString())?.Select(x => x.FileName)?.FirstOrDefault();
         

            return View(transfer);


        }


        [HttpGet]
        public async Task<IActionResult> GetData()

        {
            NoticeDTO noticeDTO = new NoticeDTO();

            List<GetRunningNoticeClassifiedDTO> transfer = new List<GetRunningNoticeClassifiedDTO>();
            var data = _playlistService.GetPlayListBuilding(getCurrentUserId(), gKey).Select(x => x.Id)?.ToList();

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
                            if (noticeDetails.StartDate.Date >= currentDate.Date || currentDate.Date <= noticeDetails.EndDate.Date)
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
                            var classifiedDetails = _classifiedService.getClassifiedDetail(model.ClassifiedId.Value);
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

            ViewBag.CurrrentNotice = _videoService.GetAllVideos().Where(x => x.Date.ToShortDateString() == DateTime.UtcNow.ToShortDateString())?.Select(x => x.FileName).FirstOrDefault();


            return PartialView("GetCurrentPlaylist", transfer);
        }
   
    }
}