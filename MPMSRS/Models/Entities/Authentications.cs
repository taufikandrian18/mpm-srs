using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Authentications")]
    public class Authentications
    {
        public Authentications()
        {
        }
        [Key]
        public Guid AuthenticationId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(10)]
        public string Otp { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string UserData { get; set; }
    }
}
