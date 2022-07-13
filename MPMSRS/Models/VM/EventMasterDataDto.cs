using System;
namespace MPMSRS.Models.VM
{
    public class EventMasterDataDto
    {
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventMasterDataCreationDto
    {
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventMasterDataUpdateDto
    {
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventMasterDataDeletionDto
    {
        public bool IsDeleted { get; set; }
    }
}
