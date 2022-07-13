using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class VisitingDetailReportAttachmentRepository : UserBase<VisitingDetailReportAttachments>, IVisitingDetailReportAttachmentProfileRepository
    {
        public VisitingDetailReportAttachmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingDetailReportAttachment(VisitingDetailReportAttachments visitingDetailReportAttachment)
        {
            Create(visitingDetailReportAttachment);
        }

        public void DeleteVisitingDetailReportAttachmentByVisitingDetailReportId(List<VisitingDetailReportAttachments> listVisitingDetailReportAttachment)
        {
            RepositoryContext.VisitingDetailReportAttachments.RemoveRange(listVisitingDetailReportAttachment);
        }

        public async Task<IEnumerable<VisitingDetailReportAttachments>> GetVisitingDetailReportAttachmentByVisitingDetailReportIdWithoutPage(Guid visitingDetailReportId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingDetailReportId == visitingDetailReportId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
