using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventDetailReportPCProfileRepository : IUserProfile<EventDetailReportProblemCategories>
    {
        void CreateEventDetailReportProblemCategories(EventDetailReportProblemCategories eventDetailReportProblemCategory);
        Task<IEnumerable<EventDetailReportProblemCategories>> GetEventDetailReportPCByEventDetailReportIdWithoutPage(Guid eventDetailReportId);
        void DeleteEventDetailReportPCByEventDetailReportId(List<EventDetailReportProblemCategories> listEventDetailReportPC);
    }
}
