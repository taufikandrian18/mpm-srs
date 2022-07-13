using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visiting_Types")]
    public class VisitingTypes
    {
        public VisitingTypes()
        {
            Visitings = new HashSet<Visitings>();
            Events = new HashSet<Events>();
        }
        [Key]
        public Guid VisitingTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string VisitingTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("VisitingType")]
        public virtual ICollection<Visitings> Visitings { get; set; }

        [InverseProperty("VisitingType")]
        public virtual ICollection<Events> Events { get; set; }
    }
}
