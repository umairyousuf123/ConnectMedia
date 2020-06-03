using System.ComponentModel.DataAnnotations;

namespace ConnectMedia.Common.Model
{
    public  class NoticePlaylist : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int? NoticeId { get; set; }
        public int? ClassifiedId { get; set; }
    }
}
