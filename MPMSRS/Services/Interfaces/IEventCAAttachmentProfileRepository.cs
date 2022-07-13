using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventCAAttachmentProfileRepository : IUserProfile<EventCAAttachments>
    {
        void CreateEventCAAttachment(EventCAAttachments eventCAAttachment);
        Task<IEnumerable<EventCAAttachments>> GetEventCAAttachmentByEventCAIdWithoutPage(Guid eventCAId);
        void DeleteEventCAAttachmentByEventCAId(List<EventCAAttachments> listEventCAAttachment);
    }
}
