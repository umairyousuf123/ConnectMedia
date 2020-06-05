using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConnectMedia.Common.DTO
{
    public class ClassifiedDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContactNumber { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
        public string Status { get; set; }
        [Required]
        public IEnumerable<int> PlayList { get; set; }
        public int entryBy { get; set; }
    }
}
