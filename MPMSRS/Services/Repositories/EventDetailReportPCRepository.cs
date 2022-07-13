using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class EventDetailReportPCRepository : UserBase<EventDetailReportProblemCategories>, IEventDetailReportPCProfileRepository
    {
        public EventDetailReportPCRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventDetailReportProblemCategories(EventDetailReportProblemCategories eventDetailReportProblemCategory)
        {
            Create(eventDetailReportProblemCategory);
        }

        public void DeleteEventDetailReportPCByEventDetailReportId(List<EventDetailReportProblemCategories> listEventDetailReportPC)
        {
            RepositoryContext.EventDetailReportProblemCategories.RemoveRange(listEventDetailReportPC);
        }

        public async Task<IEnumerable<EventDetailReportProblemCategories>> GetEventDetailReportPCByEventDetailReportIdWithoutPage(Guid eventDetailReportId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventDetailReportId == eventDetailReportId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
