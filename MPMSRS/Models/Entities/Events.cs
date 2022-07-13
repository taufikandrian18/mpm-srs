using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Events")]
    public class Events
    {
        public Events()
        {
            EventPeoples = new HashSet<EventPeoples>();
            EventDetailReports = new HashSet<EventDetailReports>();
        }
        [Key]
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid VisitingTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
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

        [ForeignKey("EventMasterDataId")]
        [InverseProperty("Events")]
        public virtual EventMasterDatas EventMasterData { get; set; }

        [ForeignKey("VisitingTypeId")]
        [InverseProperty("Events")]
        public virtual VisitingTypes VisitingType { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventPeoples> EventPeoples { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventDetailReports> EventDetailReports { get; set; }
    }
}
