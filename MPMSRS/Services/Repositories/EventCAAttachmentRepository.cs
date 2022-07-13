using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class EventCAAttachmentRepository : UserBase<EventCAAttachments>, IEventCAAttachmentProfileRepository
    {
        public EventCAAttachmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventCAAttachment(EventCAAttachments eventCAAttachment)
        {
            Create(eventCAAttachment);
        }

        public void DeleteEventCAAttachmentByEventCAId(List<EventCAAttachments> listEventCAAttachment)
        {
            RepositoryContext.EventCAAttachments.RemoveRange(listEventCAAttachment);
        }

        public async Task<IEnumerable<EventCAAttachments>> GetEventCAAttachmentByEventCAIdWithoutPage(Guid eventCAId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventCAId == eventCAId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
