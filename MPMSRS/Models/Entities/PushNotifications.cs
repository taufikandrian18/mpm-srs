using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_PushNotifications")]
    public class PushNotifications
    {
        public PushNotifications()
        {
        }
        [Key]
        public Guid PushNotificationId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid RecipientId { get; set; }
        [Required]
        [StringLength(50)]
        public string PushNotificationTitle { get; set; }
        [Required]
        [StringLength(255)]
        public string PushNotificationBody { get; set; }
        [Required]
        [StringLength(50)]
        public string ScreenID { get; set; }
        [Required]
        [StringLength(50)]
        public string Screen { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("PushNotifications")]
        public virtual Users User { get; set; }

        [ForeignKey("RecipientId")]
        [InverseProperty("PushNotificationRecipients")]
        public virtual Users UserRecipient { get; set; }
    }
}
