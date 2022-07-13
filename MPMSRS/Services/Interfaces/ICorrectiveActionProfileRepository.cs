using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface ICorrectiveActionProfileRepository : IUserProfile<CorrectiveActions>
    {
        Task<IEnumerable<CorrectiveActionDtoVMList>> GetListCAByMDCode(string mdCode, Guid divisionId,int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<IEnumerable<CorrectiveActionDtoVMList>> GetListCAByMainDealer(Guid employeeId, Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<CorrectiveActionNetworkDtoViewModel> GetListCAByNetwork(Guid networkId, Guid divisionId, int pageSize, int pageIndex, string status, string sortBy, string sortByDeadline);
        Task<IEnumerable<CorrectiveActionDtoVMList>> GetListCAByPICTagged(Guid employeeId, Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<IEnumerable<CorrectiveActionDtoVMList>> GetListAllUserCA(Guid divisionId ,int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query, string sortByDeadline);
        Task<int> GetCountCAOnProgressCreatedBy(Guid employeeId);
        Task<int> GetPercentageCA();
        Task<CorrectiveActions> GetCorrectiveActionById(Guid correctiveActionId);
        Task<CorrectiveActionDetailDto> GetCorrectiveActionDetailById(Guid correctiveActionId);
        void CreateCorrectiveActions(CorrectiveActions correctiveAction);
        void UpdateCorrectiveActions(CorrectiveActions correctiveAction);
    }
}
