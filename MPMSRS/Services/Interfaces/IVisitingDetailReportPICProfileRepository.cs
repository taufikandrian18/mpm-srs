using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingDetailReportPICProfileRepository : IUserProfile<VisitingDetailReportPICs>
    {
        void CreateVisitingDetailReportPIC(VisitingDetailReportPICs visitingDetailReportPIC);
        Task<IEnumerable<VisitingDetailReportPICs>> GetVisitingDetailReportPICByVisitingDetailReportIdWithoutPage(Guid visitingDetailReportId);
        void DeleteVisitingDetailReportPICByVisitingDetailReportId(List<VisitingDetailReportPICs> listVisitingDetailReportPIC);
    }
}
