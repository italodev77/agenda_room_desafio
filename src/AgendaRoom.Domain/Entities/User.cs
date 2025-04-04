using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaRoom.Domain.Entities;

[Table("TB_User")]
public class User
{
        [Key]
        public int UserId { get; set; }
        [Required]
        public string name { get; set; } = string.Empty;
        [Required]
        public string email { get; set; } = string.Empty;
        [Required]
        public string hashPassword { get; set; } = string.Empty;
    
        public ICollection<Reservation> Reservation{ get; set; } = new List<Reservation>();

}