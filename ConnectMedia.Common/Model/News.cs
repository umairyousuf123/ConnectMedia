using System;

namespace ConnectMedia.Common.Model
{
    public class News : BaseModel
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public string Type { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        
    }
}
