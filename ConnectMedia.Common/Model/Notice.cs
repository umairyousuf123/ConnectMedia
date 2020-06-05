using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ConnectMedia.Common.Model
{
    public class Notice : BaseModel
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
    }
}
