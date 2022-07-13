using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Corrective_Actions")]
    public class CorrectiveActions
    {
        public CorrectiveActions()
        {
            CorrectiveActionProblemCategories = new HashSet<CorrectiveActionProblemCategories>();
            CorrectiveActionPICs = new HashSet<CorrectiveActionPICs>();
            CorrectiveActionAttachments = new HashSet<CorrectiveActionAttachments>();
        }
        [Key]
        public Guid CorrectiveActionId { get; set; }
        public Guid VisitingDetailReportId { get; set; }
        [StringLength(300)]
        public string CorrectiveActionName { get; set; }
        [StringLength(50)]
        public string ProgressBy { get; set; }
        [StringLength(50)]
        public string ValidateBy { get; set; }
        [StringLength(255)]
        public string CorrectiveActionDetail { get; set; }
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
        [InverseProperty("CorrectiveActions")]
        public virtual VisitingDetailReports VisitingDetailReport { get; set; }

        [InverseProperty("CorrectiveAction")]
        public virtual ICollection<CorrectiveActionProblemCategories> CorrectiveActionProblemCategories { get; set; }

        [InverseProperty("CorrectiveAction")]
        public virtual ICollection<CorrectiveActionPICs> CorrectiveActionPICs { get; set; }

        [InverseProperty("CorrectiveAction")]
        public virtual ICollection<CorrectiveActionAttachments> CorrectiveActionAttachments { get; set; }
    }
}
