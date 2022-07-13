using System;
namespace MPMSRS.Models.VM
{
    public class UserProblemCategoryMappingDto
    {
        public Guid UserProblemCategoryMappingId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserProblemCategoryMappingForCreationDto
    {
        public Guid ProblemCategoryId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserProblemCategoryMappingForUpdateDto
    {
        public Guid UserProblemCategoryMappingId { get; set; }
        public Guid ProblemCategoryId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserProblemCategoryMappingForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
