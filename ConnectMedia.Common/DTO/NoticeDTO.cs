using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace ConnectMedia.Common.DTO
{
    public class NoticeDTO
    {
        [Key]
        public int Id { get; set; }
        [AllowHtml]
        [Required]
        public string Content { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [DataType(DataType.Duration)]
        public long Duration { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DataType(DataType.Time)]
        [Required]
        public TimeSpan StartTime { get; set; } = DateTime.Now.TimeOfDay;
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
        [DataType(DataType.Time)]
        [Required]
        public TimeSpan EndTime { get; set; } = DateTime.Now.TimeOfDay;
        public bool Expire { get; set; }
        [Required]
        public IEnumerable<int> PlayList { get; set; }
        public int entryBy { get; set; }
    }
    public class PDFDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "PDF is required")]
        public IFormFile pdfFile { get; set; }
        public string Desc { get; set; }
        public int enteryBy { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
    public class DocDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "MS word file is required")]
        public IFormFile docFile { get; set; }
        public string Desc { get; set; }
        public int enteryBy { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class GetRunningNoticeClassifiedDTO
    {
        public PlaylistDTO Playlist { get; set; }
        public List<NoticeDTO> Notices { get; set; }
        public List<ClassifiedDTO> Classifieds { get; set; }
    }
    public class SendEmailList
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegistrationNo { get; set; }
    }
    public class SendSMSList
    {
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string RegistrationNo { get; set; }
    }
}
