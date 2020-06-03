using ConnectMedia.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.Helper
{
    public static class SMSBody
    {
        public static string ClassifiedSMSBody(ClassifiedDTO classifiedDTO)
        {
            string SMSBody = "This SMS is created for " + classifiedDTO.Name + "with the title " + classifiedDTO.Title + ". The start time:" + classifiedDTO.Start + " and end time" + classifiedDTO.End + ".";
            return SMSBody;
        }
        public static string NoticeSMSBody(NoticeDTO noticeDTO)
        {
            string SMSBody = "This SMS is created for " + noticeDTO.Name + "with the Start datetime " + noticeDTO.StartDate.ToShortDateString() + " " +noticeDTO.StartTime.ToString(@"hh\:mm") + ".";
            return SMSBody;
        }
    }
}
