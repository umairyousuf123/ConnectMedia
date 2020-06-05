using System.ComponentModel.DataAnnotations;

namespace ConnectMedia.Common.Model
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
       public User Users { get; set; }
        public bool IsActive { get; set; }
    }
}
