using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventProfileRepository : IUserProfile<Events>
    {
        Task<IEnumerable<EventDtoViewModel>> GetAllEvents(int pageSize, int pageIndex, string status, string startDate, string endDate, string location, string query);
        Task<IEnumerable<EventDtoViewModel>> GetAllEventByPeopleLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string location, string query);
        Task<IEnumerable<EventDtoViewModel>> GetAllEventByCreateLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string location, string query);
        Task<Events> GetEventById(Guid eventId);
        Task<EventDtoViewDetailModel> GetEventDetailById(Guid eventId);
        Task<bool> GetEventIsOnline(Guid eventId);
        void CreateEvent(Events events);
        void UpdateEvent(Events events);
        void DeleteEvent(Events events);
    }
}
