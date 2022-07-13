﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMSRS.Models.Entities
{
    [Table("TB_Event_Corrective_Action_Attachments")]
    public class EventCAAttachments
    {
        public EventCAAttachments()
        {
        }
        [Key]
        public Guid EventCAAttachmentId { get; set; }
        public Guid EventCAId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [StringLength(64)]
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EventCAId")]
        [InverseProperty("EventCAAttachments")]
        public virtual EventCAs EventCA { get; set; }

        [ForeignKey("AttachmentId")]
        [InverseProperty("EventCAAttachments")]
        public virtual Attachments Attachment { get; set; }
    }
}
