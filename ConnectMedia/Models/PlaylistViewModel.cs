using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMedia.Models
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel()
        {
            playlistDTO = new PlaylistDTO();
        }
        public PlaylistDTO playlistDTO { get; set; }
        public List<SelectListItem> BuildingDropdownList { get; set; }
    }
}
