using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Detail_Report_Problem_Categories")]
    public class EventDetailReportProblemCategories
    {
        public EventDetailReportProblemCategories()
        {
        }
        [Key]
        public Guid EventDetailReportPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid EventDetailReportId { get; set; }
        [Required]
        [StringLength(100)]
        public string EventDetailReportPCName { get; set; }
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
        [InverseProperty("EventDetailReportProblemCategories")]
        public virtual ProblemCategories ProblemCategory { get; set; }

        [ForeignKey("EventDetailReportId")]
        [InverseProperty("EventDetailReportProblemCategories")]
        public virtual EventDetailReports EventDetailReport { get; set; }
    }
}
