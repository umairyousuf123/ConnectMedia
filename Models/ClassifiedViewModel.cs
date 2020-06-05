using ConnectMedia.Common.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMedia.Models
{
    public class ClassifiedViewModel
    {
        public ClassifiedViewModel()
        {
            classifiedDTO = new ClassifiedDTO();
        }
        public ClassifiedDTO classifiedDTO { get; set; }
        public List<SelectListItem> playlistDropdownList { get; set; }
    }
}
