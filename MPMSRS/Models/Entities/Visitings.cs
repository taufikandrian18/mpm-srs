using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Visitings")]
    public class Visitings
    {
        public Visitings()
        {
            VisitingPeoples = new HashSet<VisitingPeoples>();
            VisitingDetailReports = new HashSet<VisitingDetailReports>();
            VisitingNoteMappings = new HashSet<VisitingNoteMappings>();
            Checklists = new HashSet<Checklists>();
        }
        [Key]
        public Guid VisitingId { get; set; }
        public Guid NetworkId { get; set; }
        public Guid VisitingTypeId { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string VisitingComment { get; set; }
        [StringLength(int.MaxValue)]
        public string VisitingCommentByManager { get; set; }
        public string VisitingStatus { get; set; }
        [StringLength(50)]
        public string ApprovedByManager { get; set; }
        [StringLength(50)]
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("NetworkId")]
        [InverseProperty("Visitings")]
        public virtual Networks Network { get; set; }

        [ForeignKey("VisitingTypeId")]
        [InverseProperty("Visitings")]
        public virtual VisitingTypes VisitingType { get; set; }

        [InverseProperty("Visiting")]
        public virtual ICollection<VisitingPeoples> VisitingPeoples { get; set; }

        [InverseProperty("Visiting")]
        public virtual ICollection<VisitingDetailReports> VisitingDetailReports { get; set; }

        [InverseProperty("Visiting")]
        public virtual ICollection<VisitingNoteMappings> VisitingNoteMappings { get; set; }

        [InverseProperty("Visiting")]
        public virtual ICollection<Checklists> Checklists { get; set; }
    }
}
