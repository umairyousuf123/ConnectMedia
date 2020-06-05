using System;
using System.ComponentModel.DataAnnotations;

namespace ConnectMedia.Common.Model
{
  public class Classified : BaseModel
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Content { get; set; }
    public string ContactNumber { get; set; }
    public string Name { get; set; }
    public string Playlist { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime End { get; set; } = DateTime.Now;
    public string Status { get; set; }
  }
}
