using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class EventDetailReportPICRepository : UserBase<EventDetailReportPICs>, IEventDetailReportPICProfileRepository
    {
        public EventDetailReportPICRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventDetailReportPIC(EventDetailReportPICs eventDetailReportPIC)
        {
            Create(eventDetailReportPIC);
        }

        public void DeleteEventDetailReportPICByEventDetailReportId(List<EventDetailReportPICs> listEventDetailReportPIC)
        {
            RepositoryContext.EventDetailReportPICs.RemoveRange(listEventDetailReportPIC);
        }

        public async Task<IEnumerable<EventDetailReportPICs>> GetEventDetailReportPICByEventDetailReportIdWithoutPage(Guid eventDetailReportId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventDetailReportId == eventDetailReportId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
