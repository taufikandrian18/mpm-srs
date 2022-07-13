using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Corrective_Action_Problem_Categories")]
    public class CorrectiveActionProblemCategories
    {
        public CorrectiveActionProblemCategories()
        {
        }
        [Key]
        public Guid CorrectiveActionPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid CorrectiveActionId { get; set; }
        [Required]
        [StringLength(100)]
        public string CorrectiveActionPCName { get; set; }
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
        [InverseProperty("CorrectiveActionProblemCategories")]
        public virtual CorrectiveActions CorrectiveAction { get; set; }

        [ForeignKey("ProblemCategoryId")]
        [InverseProperty("CorrectiveActionProblemCategories")]
        public virtual ProblemCategories ProblemCategory { get; set; }
    }
}
