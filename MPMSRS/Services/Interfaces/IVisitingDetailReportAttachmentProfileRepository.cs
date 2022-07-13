using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingDetailReportAttachmentProfileRepository : IUserProfile<VisitingDetailReportAttachments>
    {
        void CreateVisitingDetailReportAttachment(VisitingDetailReportAttachments visitingDetailReportAttachment);
        Task<IEnumerable<VisitingDetailReportAttachments>> GetVisitingDetailReportAttachmentByVisitingDetailReportIdWithoutPage(Guid visitingDetailReportId);
        void DeleteVisitingDetailReportAttachmentByVisitingDetailReportId(List<VisitingDetailReportAttachments> listVisitingDetailReportAttachment);
    }
}
