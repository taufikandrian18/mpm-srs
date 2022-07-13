using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Checklist_Evidences")]
    public class ChecklistEvidences
    {
        public ChecklistEvidences()
        {
        }
        [Key]
        public Guid ChecklistEvidenceId { get; set; }
        public Guid ChecklistId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ChecklistId")]
        [InverseProperty("ChecklistEvidences")]
        public virtual Checklists Checklist { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("ChecklistEvidences")]
        public virtual Attachments Attachment { get; set; }
    }
}
