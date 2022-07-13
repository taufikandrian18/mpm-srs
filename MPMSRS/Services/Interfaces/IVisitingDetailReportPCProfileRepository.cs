using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingDetailReportPCProfileRepository : IUserProfile<VisitingDetailReportProblemCategories>
    {
        void CreateVisitingDetailReportProblemCategories(VisitingDetailReportProblemCategories visitingDetailReportProblemCategory);
        Task<IEnumerable<VisitingDetailReportProblemCategories>> GetVisitingDetailReportPCByVisitingDetailReportIdWithoutPage(Guid visitingDetailReportId);
        void DeleteVisitingDetailReportPCByVisitingDetailReportId(List<VisitingDetailReportProblemCategories> listVisitingDetailReportPC);
    }
}
