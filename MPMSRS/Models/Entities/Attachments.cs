using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Attachments")]
    public class Attachments
    {
        public Attachments()
        {
            Users = new HashSet<Users>();
            VisitingDetailReportAttachments = new HashSet<VisitingDetailReportAttachments>();
            CorrectiveActionAttachments = new HashSet<CorrectiveActionAttachments>();
            EventDetailReportAttachments = new HashSet<EventDetailReportAttachments>();
            EventCAAttachments = new HashSet<EventCAAttachments>();
            Checklists = new HashSet<Checklists>();
            ChecklistEvidences = new HashSet<ChecklistEvidences>();
        }
        [Key]
        public Guid AttachmentId { get; set; }
        [Required]
        [StringLength(255)]
        public string AttachmentUrl { get; set; }
        [Required]
        [StringLength(50)]
        public string AttachmentMime { get; set; }
        public DateTime CreatedAt { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<Users> Users { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<VisitingDetailReportAttachments> VisitingDetailReportAttachments { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<CorrectiveActionAttachments> CorrectiveActionAttachments { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<EventDetailReportAttachments> EventDetailReportAttachments { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<EventCAAttachments> EventCAAttachments { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<Checklists> Checklists { get; set; }

        [InverseProperty("Attachment")]
        public virtual ICollection<ChecklistEvidences> ChecklistEvidences { get; set; }
    }
}
