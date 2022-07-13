using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Detail_Report_Attachments")]
    public class EventDetailReportAttachments
    {
        public EventDetailReportAttachments()
        {
        }
        [Key]
        public Guid EventDetailReportAttachmentId { get; set; }
        public Guid EventDetailReportId { get; set; }
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

        [ForeignKey("EventDetailReportId")]
        [InverseProperty("EventDetailReportAttachments")]
        public virtual EventDetailReports EventDetailReport { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("EventDetailReportAttachments")]
        public virtual Attachments Attachment { get; set; }
    }
}
