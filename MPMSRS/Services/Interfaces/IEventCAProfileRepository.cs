using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventCAProfileRepository : IUserProfile<EventCAs>
    {
        Task<IEnumerable<EventCADtoVMList>> GetListCAByEventMasterDataLocation(string eventMasterDataLocation, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<IEnumerable<EventCADtoVMList>> GetListCAByMainDealer(Guid employeeId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<EventCADtoViewModel> GetListCAByEventMasterData(Guid eventMasterDataId, int pageSize, int pageIndex, string status, string sortBy);
        Task<IEnumerable<EventCADtoVMList>> GetListCAByPICTagged(Guid employeeId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<IEnumerable<EventCADtoVMList>> GetListAllUserCA(int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query);
        Task<EventCAs> GetEventCorrectiveActionById(Guid eventCAId);
        Task<EventCADetailDto> GetEventCorrectiveActionDetailById(Guid eventCAId);
        void CreateEventCorrectiveActions(EventCAs eventCA);
        void UpdateEventCorrectiveActions(EventCAs eventCA);
    }
}
