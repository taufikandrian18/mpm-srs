using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class EventDetailReportAttachmentRepository : UserBase<EventDetailReportAttachments>, IEventDetailReportAttachmentProfileRepository
    {
        public EventDetailReportAttachmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventDetailReportAttachment(EventDetailReportAttachments eventDetailReportAttachment)
        {
            Create(eventDetailReportAttachment);
        }

        public void DeleteEventDetailReportAttachmentByEventDetailReportId(List<EventDetailReportAttachments> listEventDetailReportAttachment)
        {
            RepositoryContext.EventDetailReportAttachments.RemoveRange(listEventDetailReportAttachment);
        }

        public async Task<IEnumerable<EventDetailReportAttachments>> GetEventDetailReportAttachmentByEventDetailReportIdWithoutPage(Guid eventDetailReportId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventDetailReportId == eventDetailReportId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
