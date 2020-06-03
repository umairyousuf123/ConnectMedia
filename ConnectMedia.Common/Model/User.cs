using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConnectMedia.Common.Model
{
    public class User : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public string BuildingIds { get; set; }
    }
}
