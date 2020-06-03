using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ConnectMedia.Common.Model
{
    public class Template : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [AllowHtml]
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }

    }
}
