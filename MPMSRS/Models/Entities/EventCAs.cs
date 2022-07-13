using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Corrective_Actions")]
    public class EventCAs
    {
        public EventCAs()
        {
            EventCAProblemCategories = new HashSet<EventCAProblemCategories>();
            EventCAPICs = new HashSet<EventCAPICs>();
            EventCAAttachments = new HashSet<EventCAAttachments>();
        }
        [Key]
        public Guid EventCAId { get; set; }
        public Guid EventDetailReportId { get; set; }
        [StringLength(300)]
        public string EventCAName { get; set; }
        [StringLength(50)]
        public string ProgressBy { get; set; }
        [StringLength(50)]
        public string ValidateBy { get; set; }
        [StringLength(255)]
        public string EventCADetail { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EventDetailReportId")]
        [InverseProperty("EventCAs")]
        public virtual EventDetailReports EventDetailReport { get; set; }

        [InverseProperty("EventCA")]
        public virtual ICollection<EventCAProblemCategories> EventCAProblemCategories { get; set; }

        [InverseProperty("EventCA")]
        public virtual ICollection<EventCAPICs> EventCAPICs { get; set; }

        [InverseProperty("EventCA")]
        public virtual ICollection<EventCAAttachments> EventCAAttachments { get; set; }
    }
}
