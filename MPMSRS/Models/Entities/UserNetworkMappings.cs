using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_User_Network_Mappings")]
    public class UserNetworkMappings
    {
        public UserNetworkMappings()
        {
        }
        [Key]
        public Guid UserNetworkMappingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("NetworkId")]
        [InverseProperty("UserNetworkMappings")]
        public virtual Networks Network { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("UserNetworkMappings")]
        public virtual Users User { get; set; }
    }
}
