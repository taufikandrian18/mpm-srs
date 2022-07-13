using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventDetailReportProfileRepository : IUserProfile<EventDetailReports> {

        Task<EventDetailReportDto> GetAllEventDetailReport(Guid eventId, int pageSize, int pageIndex);
        Task<IEnumerable<EventDetailReportDtoViewModel>> GetEventDetailReportIdentification(Guid eventId, int pageSize, int pageIndex);
        Task<EventDetailReports> GetEventDetailReportById(Guid eventDetailReportId);
        Task<IEnumerable<EventDetailReports>> GetEventDetailReportByEventIdWithoutPage(Guid eventId);
        Task<EventDetailReportDtoViewModel> GetEventDetailReportByEventDetailReportId(Guid eventDetailReportId);
        Task<string> GetCreatedByName(Guid employeeId);
        Task<string> GetAttachmentUrl(Guid attachmentId);
        void UpdateBulkEventDetailReport(List<EventDetailReports> eventDetailReport);
        void CreateEventDetailReport(EventDetailReports eventDetailReport);
        void UpdateEventDetailReport(EventDetailReports eventDetailReport);
        void DeleteEventDetailReport(EventDetailReports eventDetailReport);
    }
}
