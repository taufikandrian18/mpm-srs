using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Divisions")]
    public class Divisions
    {
        public Divisions()
        {
            Assignments = new HashSet<Assignments>();
            Users = new HashSet<Users>();
            VisitingDetailReports = new HashSet<VisitingDetailReports>();
            EventDetailReports = new HashSet<EventDetailReports>();
        }
        [Key]
        public Guid DivisionId { get; set; }
        [Required]
        [StringLength(100)]
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("Division")]
        public virtual ICollection<Assignments> Assignments { get; set; }

        [InverseProperty("Division")]
        public virtual ICollection<Users> Users { get; set; }

        [InverseProperty("Division")]
        public virtual ICollection<VisitingDetailReports> VisitingDetailReports { get; set; }

        [InverseProperty("Division")]
        public virtual ICollection<EventDetailReports> EventDetailReports { get; set; }
    }
}
