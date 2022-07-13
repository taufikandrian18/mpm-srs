using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class VisitingDetailReportPICRepository : UserBase<VisitingDetailReportPICs>, IVisitingDetailReportPICProfileRepository
    {
        public VisitingDetailReportPICRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingDetailReportPIC(VisitingDetailReportPICs visitingDetailReportPIC)
        {
            Create(visitingDetailReportPIC);
        }

        public void DeleteVisitingDetailReportPICByVisitingDetailReportId(List<VisitingDetailReportPICs> listVisitingDetailReportPIC)
        {
            RepositoryContext.VisitingDetailReportPICs.RemoveRange(listVisitingDetailReportPIC);
        }

        public async Task<IEnumerable<VisitingDetailReportPICs>> GetVisitingDetailReportPICByVisitingDetailReportIdWithoutPage(Guid visitingDetailReportId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingDetailReportId == visitingDetailReportId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
