using System;
namespace MPMSRS.Models.VM
{
    public class VisitingPeopleDto
    {
        public Guid VisitingPeopleId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingPeopleViewModel
    {
        public Guid VisitingPeopleId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingPeopleCreationMapDto
    {
        public Guid VisitingId { get; set; }
        public string[] EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingPeopleCreationDto
    {
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingPeopleUpdateDto
    {
        public Guid VisitingPeopleId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingPeopleDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
