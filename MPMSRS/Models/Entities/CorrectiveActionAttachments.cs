using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Corrective_Action_Attachments")]
    public class CorrectiveActionAttachments
    {
        public CorrectiveActionAttachments()
        {
        }
        [Key]
        public Guid CorrectiveActionAttachmentId { get; set; }
        public Guid CorrectiveActionId { get; set; }
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

        [ForeignKey("CorrectiveActionId")]
        [InverseProperty("CorrectiveActionAttachments")]
        public virtual CorrectiveActions CorrectiveAction { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("CorrectiveActionAttachments")]
        public virtual Attachments Attachment { get; set; }
    }
}
