using ConnectMedia.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.Model
{
    public class PlayListRunningSlots
    {
 
            public int Id { get; set; }
            public int PlaylistId { get; set; }

            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }

            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            public string Weekdays { get; set; }
            public string Months { get; set; }
            public string WeekNo { get; set; }

           public int RunningState { get; set; }
    }

    public class PlayListRunningSlotDTO 
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Weekdays { get; set; }
        public string Months { get; set; }
        public string WeekNo { get; set; }

        public int RunningState { get; set; }

        public PlaylistDTO PlayList { get; set; }
    }
}
