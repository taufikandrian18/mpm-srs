using System;
namespace MPMSRS.Models.VM
{
    public class CorrectiveActionAttachmentDto
    {
        public Guid CorrectiveActionAttachmentId { get; set; }
        public Guid CorrectiveActionId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CorrectivActionAttachmentForCreationDto
    {
        public Guid CorrectiveActionId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CorrectiveActionAttachmentForUpdateDto
    {
        public Guid CorrectiveActionAttachmentId { get; set; }
        public Guid CorrectiveActionId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
