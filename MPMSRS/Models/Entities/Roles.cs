using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Roles")]
    public class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }
        [Key]
        public Guid RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
