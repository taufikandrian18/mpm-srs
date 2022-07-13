using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Master_Datas")]
    public class EventMasterDatas
    {
        public EventMasterDatas()
        {
            Events = new HashSet<Events>();
            EventDetailReports = new HashSet<EventDetailReports>();
        }
        [Key]
        public Guid EventMasterDataId { get; set; }
        [Required]
        [StringLength(150)]
        public string EventMasterDataName { get; set; }
        [Required]
        [StringLength(255)]
        public string EventMasterDataLocation { get; set; }
        [StringLength(255)]
        public string EventMasterDataLatitude { get; set; }
        [StringLength(255)]
        public string EventMasterDataLongitude { get; set; }
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

        [InverseProperty("EventMasterData")]
        public virtual ICollection<Events> Events { get; set; }

        [InverseProperty("EventMasterData")]
        public virtual ICollection<EventDetailReports> EventDetailReports { get; set; }
    }
}
