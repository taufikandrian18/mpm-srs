using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Detail_Reports")]
    public class VisitingDetailReports
    {
        public VisitingDetailReports()
        {
            VisitingDetailReportProblemCategories = new HashSet<VisitingDetailReportProblemCategories>();
            VisitingDetailReportPICs = new HashSet<VisitingDetailReportPICs>();
            VisitingDetailReportAttachments = new HashSet<VisitingDetailReportAttachments>();
            CorrectiveActions = new HashSet<CorrectiveActions>();
        }
        [Key]
        public Guid VisitingDetailReportId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid DivisionId { get; set; }
        [StringLength(255)]
        public string VisitingDetailReportProblemIdentification { get; set; }
        [StringLength(255)]
        public string CorrectiveActionProblemIdentification { get; set; }
        [StringLength(50)]
        public string VisitingDetailReportStatus { get; set; }
        public DateTime? VisitingDetailReportDeadline { get; set; }
        [StringLength(50)]
        public string VisitingDetailReportFlagging { get; set; }
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
        [InverseProperty("VisitingDetailReports")]
        public virtual Visitings Visiting { get; set; }

        [ForeignKey("NetworkId")]
        [InverseProperty("VisitingDetailReports")]
        public virtual Networks Network { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("VisitingDetailReports")]
        public virtual Divisions Division { get; set; }

        [InverseProperty("VisitingDetailReport")]
        public virtual ICollection<VisitingDetailReportProblemCategories> VisitingDetailReportProblemCategories { get; set; }

        [InverseProperty("VisitingDetailReport")]
        public virtual ICollection<VisitingDetailReportPICs> VisitingDetailReportPICs { get; set; }

        [InverseProperty("VisitingDetailReport")]
        public virtual ICollection<VisitingDetailReportAttachments> VisitingDetailReportAttachments { get; set; }

        [InverseProperty("VisitingDetailReport")]
        public virtual ICollection<CorrectiveActions> CorrectiveActions { get; set; }
    }
}
