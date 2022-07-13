using System;
namespace MPMSRS.Models.VM
{
    public class VisitingNoteMappingDto
    {
        public Guid VisitingNoteMappingId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public string VisitingNoteMappingDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingNoteMappingForCreationDto
    {
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public string VisitingNoteMappingDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingNoteMappingForUpdateDto
    {
        public Guid VisitingNoteMappingId { get; set; }
        public Guid VisitingId { get; set; }
        public Guid EmployeeId { get; set; }
        public string VisitingNoteMappingDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingNoteMappingForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
