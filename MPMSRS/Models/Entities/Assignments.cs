using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Assignments")]
    public class Assignments
    {
        public Assignments()
        {
        }
        [Key]
        public Guid AssignmentId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
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

        [ForeignKey("EmployeeId")]
        [InverseProperty("Assignments")]
        public virtual Users User { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("Assignments")]
        public virtual Divisions Division { get; set; }

        [ForeignKey("NetworkId")]
        [InverseProperty("Assignments")]
        public virtual Networks Network { get; set; }
    }
}
