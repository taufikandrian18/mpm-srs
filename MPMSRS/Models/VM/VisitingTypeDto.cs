using System;
namespace MPMSRS.Models.VM
{
    public class VisitingTypeDto
    {
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingTypeForCreationDto
    {
        public string VisitingTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingTypeForUpdateDto
    {
        public Guid VisitingTypeId { get; set; }
        public string VisitingTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class VisitingTypeForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
