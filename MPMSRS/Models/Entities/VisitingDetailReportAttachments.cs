using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Detail_Report_Attachments")]
    public class VisitingDetailReportAttachments
    {
        public VisitingDetailReportAttachments()
        {
        }
        [Key]
        public Guid VisitingDetailReportAttachmentId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
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

        [ForeignKey("VisitingDetailReportId")]
        [InverseProperty("VisitingDetailReportAttachments")]
        public virtual VisitingDetailReports VisitingDetailReport { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("VisitingDetailReportAttachments")]
        public virtual Attachments Attachment { get; set; }
    }
}
