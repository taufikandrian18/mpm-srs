using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_User_Problem_Category_Mappings")]
    public class UserProblemCategoryMappings
    {
        public UserProblemCategoryMappings()
        {
        }
        [Key]
        public Guid UserProblemCategoryMappingId { get; set; }
        public Guid ProblemCategoryId { get; set; }
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

        [ForeignKey("ProblemCategoryId")]
        [InverseProperty("UserProblemCategoryMappings")]
        public virtual ProblemCategories ProblemCategory { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("UserProblemCategoryMappings")]
        public virtual Users User { get; set; }
    }
}
