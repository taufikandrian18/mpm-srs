using System;
using System.Collections.Generic;

namespace MPMSRS.Models.VM
{
    public class ProblemCategoryDto
    {
        public Guid ProblemCategoryId { get; set; }
        public string ProblemCategoryName { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChildId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ProblemCategoryDtoView
    {
        public Guid ProblemCategoryId { get; set; }
        public string ProblemCategoryName { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
        public Guid? ChildId { get; set; }
        public string ChildName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<UserProblemCategoryMappingListViewDto> ListEmployees { get; set; }

    }

    public class UserProblemCategoryMappingListViewDto
    {
        public Guid EmployeeId { get; set; }
        public string DisplayName { get; set; }
    }

    public class UserProblemCategoryMappingListDto
    {
        public Guid EmployeeId { get; set; }
    }

    public class ProblemCategoryCreationForDto
    {
        public string ProblemCategoryName { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChildId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<UserProblemCategoryMappingListDto> ListEmployees { get; set; }
    }

    public class ProblemCategoryUpdateForDto
    {
        public Guid ProblemCategoryId { get; set; }
        public string ProblemCategoryName { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChildId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<UserProblemCategoryMappingListDto> ListEmployees { get; set; }
    }

    public class ProblemCategoryDeleteForDto
    {
        public bool IsDeleted { get; set; }
    }
}
