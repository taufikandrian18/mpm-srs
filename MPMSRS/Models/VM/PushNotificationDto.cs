using System;
namespace MPMSRS.Models.VM
{
    public class PushNotificationDto
    {
        public Guid PushNotificationId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid RecipientId { get; set; }
        public string PushNotificationTitle { get; set; }
        public string PushNotificationBody { get; set; }
        public string ScreenID { get; set; }
        public string Screen { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class PushNotificationForCreationDto
    {
        public Guid EmployeeId { get; set; }
        public Guid RecipientId { get; set; }
        public string PushNotificationTitle { get; set; }
        public string PushNotificationBody { get; set; }
        public string ScreenID { get; set; }
        public string Screen { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class PushNotificationForUpdateDto
    {
        public Guid PushNotificationId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid RecipientId { get; set; }
        public string PushNotificationTitle { get; set; }
        public string PushNotificationBody { get; set; }
        public string ScreenID { get; set; }
        public string Screen { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class PushNotificationForDeletionDto
    {
        public bool IsDeleted { get; set; }
    }
}
