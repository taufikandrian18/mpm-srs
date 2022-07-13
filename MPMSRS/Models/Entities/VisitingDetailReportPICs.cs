using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Detail_Report_PICs")]
    public class VisitingDetailReportPICs
    {
        public VisitingDetailReportPICs()
        {
        }
        [Key]
        public Guid VisitingDetailReportPICId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
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
        [InverseProperty("VisitingDetailReportPICs")]
        public virtual Users User { get; set; }

        [ForeignKey("VisitingDetailReportId")]
        [InverseProperty("VisitingDetailReportPICs")]
        public virtual VisitingDetailReports VisitingDetailReport { get; set; }
    }
}
