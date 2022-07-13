using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Checklist_PICs")]
    public class ChecklistPICs
    {
        public ChecklistPICs()
        {
        }
        [Key]
        public Guid ChecklistPICId { get; set; }
        public Guid ChecklistId { get; set; }
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
        [InverseProperty("ChecklistPICs")]
        public virtual Users User { get; set; }

        [ForeignKey("ChecklistId")]
        [InverseProperty("ChecklistPICs")]
        public virtual Checklists Checklist { get; set; }
    }
}
