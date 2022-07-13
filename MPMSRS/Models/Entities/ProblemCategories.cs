using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Problem_Categories")]
    public class ProblemCategories
    {
        public ProblemCategories()
        {
            VisitingDetailReportProblemCategories = new HashSet<VisitingDetailReportProblemCategories>();
            CorrectiveActionProblemCategories = new HashSet<CorrectiveActionProblemCategories>();
            UserProblemCategoryMappings = new HashSet<UserProblemCategoryMappings>();
            EventDetailReportProblemCategories = new HashSet<EventDetailReportProblemCategories>();
            EventCAProblemCategories = new HashSet<EventCAProblemCategories>();
        }
        [Key]
        public Guid ProblemCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProblemCategoryName { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChildId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("ProblemCategory")]
        public virtual ICollection<VisitingDetailReportProblemCategories> VisitingDetailReportProblemCategories { get; set; }

        [InverseProperty("ProblemCategory")]
        public virtual ICollection<CorrectiveActionProblemCategories> CorrectiveActionProblemCategories { get; set; }

        [InverseProperty("ProblemCategory")]
        public virtual ICollection<UserProblemCategoryMappings> UserProblemCategoryMappings { get; set; }

        [InverseProperty("ProblemCategory")]
        public virtual ICollection<EventDetailReportProblemCategories> EventDetailReportProblemCategories { get; set; }

        [InverseProperty("ProblemCategory")]
        public virtual ICollection<EventCAProblemCategories> EventCAProblemCategories { get; set; }
    }
}
