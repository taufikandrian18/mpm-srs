using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Detail_Reports")]
    public class EventDetailReports
    {
        public EventDetailReports()
        {
            EventDetailReportProblemCategories = new HashSet<EventDetailReportProblemCategories>();
            EventDetailReportPICs = new HashSet<EventDetailReportPICs>();
            EventDetailReportAttachments = new HashSet<EventDetailReportAttachments>();
            EventCAs = new HashSet<EventCAs>();
        }
        [Key]
        public Guid EventDetailReportId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid DivisionId { get; set; }
        [StringLength(255)]
        public string EventDetailReportProblemIdentification { get; set; }
        [StringLength(255)]
        public string EventCAProblemIdentification { get; set; }
        [StringLength(50)]
        public string EventDetailReportStatus { get; set; }
        public DateTime? EventDetailReportDeadline { get; set; }
        [StringLength(50)]
        public string EventDetailReportFlagging { get; set; }
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
        [InverseProperty("EventDetailReports")]
        public virtual Events Event { get; set; }

        [ForeignKey("EventMasterDataId")]
        [InverseProperty("EventDetailReports")]
        public virtual EventMasterDatas EventMasterData { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("EventDetailReports")]
        public virtual Divisions Division { get; set; }

        [InverseProperty("EventDetailReport")]
        public virtual ICollection<EventDetailReportProblemCategories> EventDetailReportProblemCategories { get; set; }

        [InverseProperty("EventDetailReport")]
        public virtual ICollection<EventDetailReportPICs> EventDetailReportPICs { get; set; }

        [InverseProperty("EventDetailReport")]
        public virtual ICollection<EventDetailReportAttachments> EventDetailReportAttachments { get; set; }

        [InverseProperty("EventDetailReport")]
        public virtual ICollection<EventCAs> EventCAs { get; set; }
    }
}
