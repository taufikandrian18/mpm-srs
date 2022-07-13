using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IVisitingProfileRepository : IUserProfile<Visitings>
    {
        Task<IEnumerable<VisitingDtoLoginViewAllModel>> GetAllVisitings(int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query, string createdName, string visitingPeopleName);
        Task<IEnumerable<VisitingDtoLoginViewModel>> GetAllVisitingByPeopleLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query);
        Task<IEnumerable<VisitingReportDtoLoginViewModel>> GetAllVisitingByManagerDivisionId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query);
        Task<IEnumerable<VisitingReportDtoLoginViewModel>> GetAllVisitingByCreateLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query);
        Task<IEnumerable<VisitingReportDtoLoginViewModel>> GetAllVisitingReports(string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query);
        Task<IEnumerable<VisitingDetailExportExcelDtoViewModel>> GetAllVisitingExportExcel(string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query);
        Task<int> GetAllListCountVisitingOnGoingByDate(Guid employeeId, DateTime date);
        Task<Visitings> GetVisitingById(Guid visitingId);
        Task<VisitingDtoViewDetailModel> GetVisitingDetailById(Guid visitingId);
        Task<bool> GetVisitingIsOnline(Guid visitingId);
        void CreateVisiting(Visitings visiting);
        void UpdateVisiting(Visitings visiting);
        void DeleteVisiting(Visitings visiting);
    }
}
