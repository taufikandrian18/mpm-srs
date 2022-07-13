using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Checklists")]
    public class Checklists
    {
        public Checklists()
        {
            ChecklistPICs = new HashSet<ChecklistPICs>();
            ChecklistEvidences = new HashSet<ChecklistEvidences>();
        }
        [Key]
        public Guid ChecklistId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid AttachmentId { get; set; }
        [StringLength(255)]
        public string ChecklistItem { get; set; }
        [StringLength(255)]
        public string ChecklistIdentification { get; set; }
        [StringLength(255)]
        public string ChecklistActualCondition { get; set; }
        [StringLength(int.MaxValue)]
        public string ChecklistActualDetail { get; set; }
        [StringLength(int.MaxValue)]
        public string ChecklistFix { get; set; }
        [StringLength(50)]
        public string ChecklistStatus { get; set; }
        public DateTime? ChecklistDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("VisitingId")]
        [InverseProperty("Checklists")]
        public virtual Visitings Visiting { get; set; }

        [ForeignKey("NetworkId")]
        [InverseProperty("Checklists")]
        public virtual Networks Network { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("Checklists")]
        public virtual Attachments Attachment { get; set; }

        [InverseProperty("Checklist")]
        public virtual ICollection<ChecklistPICs> ChecklistPICs { get; set; }

        [InverseProperty("Checklist")]
        public virtual ICollection<ChecklistEvidences> ChecklistEvidences { get; set; }
    }
}
