using System;
using System.Collections.Generic;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class EventDto
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventDtoViewModel
    {
        public Guid EventId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public List<EventPeopleDtoViewModel> EventPeoples { get; set; }
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class EmployeeListInEventPeople
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EventDtoViewDetailModel
    {
        public Guid EventId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public Guid EventMasterDataId { get; set; }
        public string EventMasterDataName { get; set; }
        public string EventMasterDataLocation { get; set; }
        public string EventMasterDataLatitude { get; set; }
        public string EventMasterDataLongitude { get; set; }
        public List<EventPeopleDtoViewModel> EventPeoples { get; set; }
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventForCreationDto
    {
        public Guid EventMasterDataId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string[] EmployeeId { get; set; }
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventForUpdateDto
    {
        public Guid EventId { get; set; }
        public Guid EventMasterDataId { get; set; }
        public Guid VisitingTypeId { get; set; }
        public string[] EmployeeId { get; set; }
        public string EventComment { get; set; }
        public string EventStatus { get; set; }
        public string ApprovedByManager { get; set; }
        public string ApprovedByGM { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventForDeletionDto
    {
        public bool IsDeleted { get; set; }
    }
}
