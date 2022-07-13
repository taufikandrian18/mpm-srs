using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Users")]
    public class Users
    {
        public Users()
        {
            Assignments = new HashSet<Assignments>();
            RefreshToken = new HashSet<RefreshToken>();
            VisitingPeoples = new HashSet<VisitingPeoples>();
            VisitingDetailReportPICs = new HashSet<VisitingDetailReportPICs>();
            CorrectiveActionPICs = new HashSet<CorrectiveActionPICs>();
            UserNetworkMappings = new HashSet<UserNetworkMappings>();
            VisitingNoteMappings = new HashSet<VisitingNoteMappings>();
            UserProblemCategoryMappings = new HashSet<UserProblemCategoryMappings>();
            UserFcmTokens = new HashSet<UserFcmTokens>();
            PushNotifications = new HashSet<PushNotifications>();
            PushNotificationRecipients = new HashSet<PushNotifications>();
            EventPeoples = new HashSet<EventPeoples>();
            EventDetailReportPICs = new HashSet<EventDetailReportPICs>();
            EventCAPICs = new HashSet<EventCAPICs>();
            ChecklistPICs = new HashSet<ChecklistPICs>();
        }
        [Key]
        public Guid EmployeeId { get; set; }
        public Guid? AttachmentId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? DivisionId { get; set; }
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
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("Users")]
        public virtual Attachments Attachment { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Roles Role { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("Users")]
        public virtual Divisions Division { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Assignments> Assignments { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<VisitingPeoples> VisitingPeoples { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<VisitingDetailReportPICs> VisitingDetailReportPICs { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<CorrectiveActionPICs> CorrectiveActionPICs { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserNetworkMappings> UserNetworkMappings { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<VisitingNoteMappings> VisitingNoteMappings { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserProblemCategoryMappings> UserProblemCategoryMappings { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserFcmTokens> UserFcmTokens { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<PushNotifications> PushNotifications { get; set; }

        [InverseProperty("UserRecipient")]
        public virtual ICollection<PushNotifications> PushNotificationRecipients { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<EventPeoples> EventPeoples { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<EventDetailReportPICs> EventDetailReportPICs { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<EventCAPICs> EventCAPICs { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<ChecklistPICs> ChecklistPICs { get; set; }
    }
}
