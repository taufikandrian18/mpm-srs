using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Detail_Report_Problem_Categories")]
    public class VisitingDetailReportProblemCategories
    {
        public VisitingDetailReportProblemCategories()
        {
        }
        [Key]
        public Guid VisitingDetailReportPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        [Required]
        [StringLength(100)]
        public string VisitingDetailReportPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProblemCategoryId")]
        [InverseProperty("VisitingDetailReportProblemCategories")]
        public virtual ProblemCategories ProblemCategory { get; set; }

        [ForeignKey("VisitingDetailReportId")]
        [InverseProperty("VisitingDetailReportProblemCategories")]
        public virtual VisitingDetailReports VisitingDetailReport { get; set; }
    }
}
