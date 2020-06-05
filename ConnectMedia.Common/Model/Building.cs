using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConnectMedia.Common.Model
{
    public class Building : BaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Address { get; set; }
        public string Key { get; set; }
    }
}
