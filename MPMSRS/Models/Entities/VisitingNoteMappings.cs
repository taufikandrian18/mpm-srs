using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Note_Mappings")]
    public class VisitingNoteMappings
    {
        public VisitingNoteMappings()
        {
        }
        [Key]
        public Guid VisitingNoteMappingId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        [Required]
        [StringLength(255)]
        public string VisitingNoteMappingDesc { get; set; }
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
        [InverseProperty("VisitingNoteMappings")]
        public virtual Visitings Visiting { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("VisitingNoteMappings")]
        public virtual Users User { get; set; }
    }
}
