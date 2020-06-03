using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectMedia.Common.Model
{
    public class ResgisterUser : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string SerialNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
