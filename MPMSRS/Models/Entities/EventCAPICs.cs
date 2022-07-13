using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Corrective_Action_PICs")]
    public class EventCAPICs
    {
        public EventCAPICs()
        {
        }
        [Key]
        public Guid EventCAPICId { get; set; }
        public Guid EventCAId { get; set; }
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

        [ForeignKey("EventCAId")]
        [InverseProperty("EventCAPICs")]
        public virtual EventCAs EventCA { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EventCAPICs")]
        public virtual Users User { get; set; }
    }
}
