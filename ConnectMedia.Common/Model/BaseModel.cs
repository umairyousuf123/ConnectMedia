using System;

namespace ConnectMedia.Common.Model
{
    public class BaseModel
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } 
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDel { get; set; } = false;
    }
}
