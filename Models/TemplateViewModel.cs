using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ConnectMedia.Models
{
    public class TemplateViewModel
    {
        public TemplateViewModel()
        {
            template = new Template();
        }
        public Template  template { get; set; }
        public List<SelectListItem> categoryDropdownList { get; set; }
    }
}
