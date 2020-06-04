using ConnectMedia.Common.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using ConnectMedia.Common.DTO;

namespace ConnectMedia.Models
{
    public class NoticeViewModel
    {
        public NoticeViewModel()
        {
            noticeDTO = new NoticeDTO();
        }
        public NoticeDTO noticeDTO { get; set; }
        public List<SelectListItem> categoryDropdownList { get; set; }
        public List<SelectListItem> playlistDropdownList { get; set; }
    }
  
}
