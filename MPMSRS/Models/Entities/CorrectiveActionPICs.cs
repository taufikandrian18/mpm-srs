using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Corrective_Action_PICs")]
    public class CorrectiveActionPICs
    {
        public CorrectiveActionPICs()
        {
        }
        [Key]
        public Guid CorrectiveActionPICId { get; set; }
        public Guid CorrectiveActionId { get; set; }
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

        [ForeignKey("CorrectiveActionId")]
        [InverseProperty("CorrectiveActionPICs")]
        public virtual CorrectiveActions CorrectiveAction { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("CorrectiveActionPICs")]
        public virtual Users User { get; set; }
    }
}
