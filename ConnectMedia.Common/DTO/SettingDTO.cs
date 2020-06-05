using ConnectMedia.Common.Enum;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConnectMedia.Common.DTO
{
    public class SettingDTO
    {

    }
    public class ImageDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile imgFile { get; set; }
        public string Desc { get; set; }
        public int enteryBy { get; set; }
    }
    public class CSVDTO
    {
        [Required(ErrorMessage = "Team is required")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "CSV file is required")]
        public IFormFile csvFile { get; set; }
        public string Desc { get; set; }
        public int enteryBy { get; set; }
    }
    public class VideoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required(ErrorMessage = "Video is required")]
        public IFormFile videoFile { get; set; }
        [Required(ErrorMessage = "Tabs are required")]
        public int Portion { get; set; }
        [Required(ErrorMessage = "Sequence are required")]
        public int Sequence { get; set; }
        public int enteryBy { get; set; }
    }
    public class TeamCSVList
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
