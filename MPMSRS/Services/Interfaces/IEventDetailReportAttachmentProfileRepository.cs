using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventDetailReportAttachmentProfileRepository : IUserProfile<EventDetailReportAttachments>
    {
        void CreateEventDetailReportAttachment(EventDetailReportAttachments eventDetailReportAttachment);
        Task<IEnumerable<EventDetailReportAttachments>> GetEventDetailReportAttachmentByEventDetailReportIdWithoutPage(Guid eventDetailReportId);
        void DeleteEventDetailReportAttachmentByEventDetailReportId(List<EventDetailReportAttachments> listEventDetailReportAttachment);
    }
}
