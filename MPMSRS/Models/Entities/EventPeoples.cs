using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Peoples")]
    public class EventPeoples
    {
        public EventPeoples()
        {
        }
        [Key]
        public Guid EventPeopleId { get; set; }
        public Guid EventId { get; set; }
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

        [ForeignKey("EventId")]
        [InverseProperty("EventPeoples")]
        public virtual Events Event { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EventPeoples")]
        public virtual Users User { get; set; }
    }
}
