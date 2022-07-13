using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Peoples")]
    public class VisitingPeoples
    {
        public VisitingPeoples()
        {
        }
        [Key]
        public Guid VisitingPeopleId { get; set; }
        public Guid VisitingId { get; set; }
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

        [ForeignKey("VisitingId")]
        [InverseProperty("VisitingPeoples")]
        public virtual Visitings Visiting { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("VisitingPeoples")]
        public virtual Users User { get; set; }
    }
}
