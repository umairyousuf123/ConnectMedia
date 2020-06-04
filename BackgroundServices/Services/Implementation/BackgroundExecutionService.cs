




using ConnectMedia.BackgroundServices.Services.Interface;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectMedia.BackgroundServices.Services.Implementation
{
    public class BackgroundExecutionService : IBackgroundExecutionService
    {

        private readonly IPlaylistService _playlistService;
        private readonly IClassifiedService _classifiedService;
        private readonly INoticeService _noticeService;
        public BackgroundExecutionService(IPlaylistService playlistService, IClassifiedService classifiedService,
            INoticeService noticeService)
        {
            this._playlistService = playlistService;
            this._classifiedService = classifiedService;
            this._noticeService = noticeService;
        }


        public async Task ActivatePlayList()
        {
            await Task.Run(() =>
            {

                var playList = _playlistService.GetPlayListRunningSlots(1); //Get Stop Slot Data


                foreach (var item in playList)
                {
                    bool isActivate = true;
                    var currentDate = DateTime.UtcNow.AddHours(5);
                    var Date = currentDate.ToShortDateString();
                    var currentTime = currentDate.TimeOfDay;
                    var currentMonth = currentDate.Month;
                    var currentDay = currentDate.DayOfWeek;
                    var currentWeek = GetWeekNumberOfMonth(currentDate);
                    if (item.StartDate.Date >= currentDate.Date || currentDate.Date <= item.EndDate.Date)
                    {

                        if (!string.IsNullOrEmpty(item.Months))
                        {
                            var itemMonth = item.Months.Split(',').ToList().Where(x => x == currentMonth.ToString()).Count();
                            if (itemMonth == 0)
                            {
                                isActivate = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(item.WeekNo))
                        {
                            var itemWeekNo = item.WeekNo.Split(',').ToList().Where(x => x == currentWeek.ToString()).Count();
                            if (itemWeekNo == 0)
                            {
                                isActivate = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(item.Weekdays))
                        {
                            var itemWeekDay = item.Weekdays.Split(',').ToList().Where(x => x.ToLower() == currentDay.ToString().ToLower()).Count();
                            if (itemWeekDay == 0)
                            {
                                isActivate = false;
                            }
                        }

                        if (item.StartTime.Hours == currentTime.Hours && item.StartTime.Minutes == currentTime.Minutes && isActivate == true)
                        {
                            var noticePlaylistData = _playlistService.GetNoticePlayList(item.PlaylistId);
                            foreach (var item1 in noticePlaylistData)
                            {
                                var model = new RunningNoticeClassified { EntityId = item1.Id, PlayListId = item.PlaylistId };
                                _playlistService.AddRunningNoticeClassified(model);
                                //if (item1.ClassifiedId == null && item1.NoticeId != null)
                                //{
                                //    //Check Notice
                                //    var noticeDetails = _noticeService.getNoticeDetail(item1.NoticeId.Value);
                                //    if (noticeDetails.StartDate.Date >= currentDate.Date || currentDate.Date <= noticeDetails.EndDate.Date)
                                //    {
                                //        //if (noticeDetails.StartTime.Hours == currentTime.Hours && noticeDetails.StartTime.Minutes == currentTime.Minutes)
                                //        //{
                                //            var model = new RunningNoticeClassified { EntityId = item1.Id, PlayListId = item.PlaylistId };
                                //            _playlistService.AddRunningNoticeClassified(model);
                                //        //}

                                //    }

                                //}
                                //else if (item1.NoticeId == null)
                                //{
                                //    //Check Classified
                                //    var classifiedDetails = _classifiedService.getClassifiedDetail(item1.ClassifiedId.Value);
                                //    if ( classifiedDetails.Start.Date >= currentDate.Date || currentDate.Date <= classifiedDetails.End.Date)
                                //    {
                                //        var model = new RunningNoticeClassified { EntityId = item1.Id, PlayListId = item.PlaylistId };
                                //        _playlistService.AddRunningNoticeClassified(model);
                                //    }
                                //}

                            }
                            _playlistService.UpdatePlayListRunningSlot(2, item.Id);

                        }


                    }
                }




            });
        }

        //public async Task AddNoticeofActiveRunningPlayList()
        //{
        //    await Task.Run(() =>
        //    {

        //        var playList = _playlistService.GetPlayListRunningSlots(2); //Get Stop Slot Data

        //        var currentDate = DateTime.UtcNow.AddHours(5);
        //        var Date = currentDate.ToShortDateString();
        //        var currentTime = currentDate.TimeOfDay;
        //        var currentMonth = currentDate.Month;
        //        var currentDay = currentDate.DayOfWeek;
        //        var currentWeek = GetWeekNumberOfMonth(currentDate);
        //        var datetime = YearWeekDayToDateTime(currentDate.Year, currentDay, currentWeek);
        //        foreach (var item in playList)
        //        {
        //            var noticePlaylistData = _playlistService.GetNoticePlayList(item.PlaylistId);
        //            foreach (var item1 in noticePlaylistData)
        //            {
        //                if (item1.ClassifiedId == null && item1.NoticeId != null)
        //                {
        //                    //Check Notice
        //                    var noticeDetails = _noticeService.getNoticeDetail(item1.NoticeId.Value);
        //                    if (currentDate >= noticeDetails.StartDate || currentDate <= noticeDetails.EndDate)
        //                    {
        //                        if (noticeDetails.StartTime.Hours == currentTime.Hours && noticeDetails.StartTime.Minutes == currentTime.Minutes)
        //                        {
        //                            var model = new RunningNoticeClassified { EntityId = item1.Id, PlayListId = item.PlaylistId };
        //                            _playlistService.AddRunningNoticeClassified(model);
        //                        }

        //                    }

        //                }
        //                else if (item1.NoticeId == null)
        //                {
        //                    //Check Classified
        //                    var classifiedDetails = _classifiedService.getClassifiedDetail(item1.ClassifiedId.Value);
        //                    if (classifiedDetails.Start >= currentDate || classifiedDetails.End <= currentDate)
        //                    {
        //                        var model = new RunningNoticeClassified { EntityId = item1.Id, PlayListId = item.PlaylistId };
        //                        _playlistService.AddRunningNoticeClassified(model);
        //                    }
        //                }

        //            }


        //        }





        //    });

        //}

        public async Task DeActivatePlayList()
        {
            await Task.Run(() =>
            {

                var playList = _playlistService.GetPlayListRunningSlots(2); //Get Running Slot Data
                foreach (var item in playList)
                {
                    var currentDate = DateTime.UtcNow.AddHours(5);
                    var Date = currentDate.ToShortDateString();
                    var currentTime = currentDate.TimeOfDay;
                    var currentMonth = currentDate.Month;
                    var currentDay = currentDate.DayOfWeek;
                    var currentWeek = GetWeekNumberOfMonth(currentDate);

                    if (item.EndTime.Hours == currentTime.Hours && item.EndTime.Minutes == currentTime.Minutes)
                    {
                        var noticePlaylistData = _playlistService.GetNoticePlayList(item.PlaylistId);
                        foreach (var item1 in noticePlaylistData)
                        {
                            _playlistService.DeleteRunningNoticeClassified(item1.Id);

                        }
                        _playlistService.UpdatePlayListRunningSlot(1, item.Id);

                    }



                }


            });


        }

        static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }





    }
}
