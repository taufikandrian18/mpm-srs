using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Corrective_Action_Problem_Categories")]
    public class EventCAProblemCategories
    {
        public EventCAProblemCategories()
        {
        }
        [Key]
        public Guid EventCAPCId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid EventCAId { get; set; }
        [Required]
        [StringLength(100)]
        public string EventCAPCName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EventCAId")]
        [InverseProperty("EventCAProblemCategories")]
        public virtual EventCAs EventCA { get; set; }

        [ForeignKey("ProblemCategoryId")]
        [InverseProperty("EventCAProblemCategories")]
        public virtual ProblemCategories ProblemCategory { get; set; }
    }
}
