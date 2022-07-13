using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventDetailReportPICProfileRepository : IUserProfile<EventDetailReportPICs>
    {
        void CreateEventDetailReportPIC(EventDetailReportPICs eventDetailReportPIC);
        Task<IEnumerable<EventDetailReportPICs>> GetEventDetailReportPICByEventDetailReportIdWithoutPage(Guid eventDetailReportId);
        void DeleteEventDetailReportPICByEventDetailReportId(List<EventDetailReportPICs> listEventDetailReportPIC);
    }
}
