using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_UserFcmTokens")]
    public class UserFcmTokens
    {
        public UserFcmTokens()
        {
        }
        [Key]
        public Guid UserFcmTokenId { get; set; }
        public Guid EmployeeId { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("UserFcmTokens")]
        public virtual Users User { get; set; }
    }
}
