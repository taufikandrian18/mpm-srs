using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface ICorrectiveActionAttachmentProfileRepository : IUserProfile<CorrectiveActionAttachments>
    {
        void CreateCorrectiveActionAttachment(CorrectiveActionAttachments correctiveActionAttachment);
        Task<IEnumerable<CorrectiveActionAttachments>> GetCorrectiveActionAttachmentByCorrectiveActionIdWithoutPage(Guid correctiveActionId);
        void DeleteCorrectiveActionAttachmentByCorrectiveActionId(List<CorrectiveActionAttachments> listCorrectiveActionAttachment);
    }
}
