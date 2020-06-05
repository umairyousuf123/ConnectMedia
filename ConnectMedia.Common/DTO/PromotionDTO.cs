using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class PromotionDTO
    {
        public List<NoticeGridView> AdminNotices { get; set; }
        public List<NoticeGridView> UserNotices { get; set; }
    }
}
