using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class VisitingDetailReportPCRepository : UserBase<VisitingDetailReportProblemCategories>, IVisitingDetailReportPCProfileRepository
    {
        public VisitingDetailReportPCRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingDetailReportProblemCategories(VisitingDetailReportProblemCategories visitingDetailReportProblemCategory)
        {
            Create(visitingDetailReportProblemCategory);
        }

        public void DeleteVisitingDetailReportPCByVisitingDetailReportId(List<VisitingDetailReportProblemCategories> listVisitingDetailReportPC)
        {
            RepositoryContext.VisitingDetailReportProblemCategories.RemoveRange(listVisitingDetailReportPC);
        }

        public async Task<IEnumerable<VisitingDetailReportProblemCategories>> GetVisitingDetailReportPCByVisitingDetailReportIdWithoutPage(Guid visitingDetailReportId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingDetailReportId == visitingDetailReportId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
