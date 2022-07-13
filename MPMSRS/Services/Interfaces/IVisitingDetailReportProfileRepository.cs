using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingDetailReportProfileRepository : IUserProfile<VisitingDetailReports>
    {
        Task<VisitingDetailDtoLoginViewModel> GetAllVisitingDetailReport(Guid visitingId,int pageSize, int pageIndex);
        Task<VisitingDetailReportIdentificationModel> GetVisitingDetailReportIdentification(Guid visitingId, int pageSize, int pageIndex);
        Task<VisitingDetailReports> GetVisitingDetailReportById(Guid visitingDetailReportId);
        Task<IEnumerable<VisitingDetailReports>> GetVisitingDetailReportByVisitingIdWithoutPage(Guid visitingId);
        Task<VisitingDetailReportViewModel> GetVisitingDetailReportByVisitingDetailReportId(Guid visitingDetailReportId);
        Task<IEnumerable<VisitingDetailReportViewExcelModel>> GetAllVisitingDetailReportExportExcel(string status, string area, string query);
        Task<string> GetCreatedByName(Guid employeeId);
        Task<string> GetAttachmentUrl(Guid attachmentId);
        void UpdateBulkVisitingDetailReport(List<VisitingDetailReports> visitingDetailReport);
        void CreateVisitingDetailReport(VisitingDetailReports visitingDetailReport);
        void UpdateVisitingDetailReport(VisitingDetailReports visitingDetailReport);
        void DeleteVisitingDetailReport(VisitingDetailReports visitingDetailReport);
    }
}
