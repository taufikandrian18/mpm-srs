using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    public class RefreshToken
    {
        public RefreshToken()
        {
        }
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string Token { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("RefreshToken")]
        public virtual Users User { get; set; }
    }
}
