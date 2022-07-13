using System;
using MPMSRS.Models.Entities;

namespace MPMSRS.Models.VM
{
    public class UserDto
    {
        public Guid EmployeeId { get; set; }
        public Guid? AttachmentId { get; set; }
        public string CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WorkLocation { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InternalTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid? DivisionId { get; set; }
        public string DivisionName { get; set; }
    }

    public class UserDtoViewModel
    {
        public Guid EmployeeId { get; set; }
        public Guid? AttachmentId { get; set; }
        public string AttachmentUrl { get; set; }
        public string CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WorkLocation { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InternalTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid? DivisionId { get; set; }
        public string DivisionName { get; set; }
    }

    public class UserForCreationDto
    {
        public Guid? RoleId { get; set; }
        public Guid? DivisionId { get; set; }
        public Guid? AttachmentId { get; set; }
        public string CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WorkLocation { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InternalTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserForUpdateDto
    {
        public Guid EmployeeId { get; set; }
        public Guid? AttachmentId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? DivisionId { get; set; }
        public string CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WorkLocation { get; set; }
        public string DisplayName { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InternalTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserProdilePictureForUpdateDto
    {
        public Guid EmployeeId { get; set; }
    }

    public class UserForDeleteDto
    {
        public bool IsDeleted { get; set; }
    }
}
