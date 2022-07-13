using System;
namespace MPMSRS.Models.VM
{
    public class DivisionDto
    {
        public Guid DivisionId { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DivisionCreationForDto
    {
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DivisionUpdateForDto
    {
        public Guid DivisionId { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DivisionDeleteForDto
    {
        public bool IsDeleted { get; set; }
    }
}
