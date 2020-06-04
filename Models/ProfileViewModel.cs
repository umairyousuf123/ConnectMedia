using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ConnectMedia.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {

            UserDTO = new UserDTO();
            building = new Building();

        }
        public UserDTO UserDTO { get; set; }
        public Building  building { get; set; }
        public List<SelectListItem> RolesDropdownList { get; set; }
        public List<SelectListItem> BuildingDropdownList { get; set; }
    }
}

