using System;
namespace MPMSRS.Models.VM
{
    public class EventPeopleDto
    {
        public Guid EventPeopleId { get; set; }
        public Guid EventId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventPeopleDtoViewModel
    {
        public Guid EventPeopleId { get; set; }
        public Guid EventId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventPeopleCreationMapDto
    {
        public Guid EventId { get; set; }
        public string[] EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventPeopleForCreationDto
    {
        public Guid EventId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventPeopleForUpdateDto
    {
        public Guid EventPeopleId { get; set; }
        public Guid EventId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventPeopleForDeletionDto
    {
        public bool IsDeleted { get; set; }
    }
}
