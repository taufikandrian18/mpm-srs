using System;
namespace MPMSRS.Models.VM
{
    public class UserFcmTokenDto
    {
        public Guid UserFcmTokenId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserFcmTokenForCreationDto
    {
        public Guid EmployeeId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserFcmTokenForUpdateDto
    {
        public Guid EmployeeId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserFcmTokenForDeletionDto
    {
        public bool IsDeleted { get; set; }
    }
}
