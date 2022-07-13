using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces
{
    public interface IEventPeopleProfileRepository : IUserProfile<EventPeoples>
    {
        Task<IEnumerable<EventPeoples>> GetAllEventPeoples(int pageSize, int pageIndex);
        Task<EventPeoples> GetEventPeopleById(Guid eventPeopleId);
        Task<EventPeoples> GetEventPeopleByEmployeeId(Guid employeeId);
        Task<IEnumerable<EventPeopleDtoViewModel>> GetEventPeopleByEventId(Guid eventId, int pageSize, int pageIndex);
        Task<IEnumerable<EventPeoples>> GetVisitingPeopleByEventIdWithoutPage(Guid eventId);
        void CreateEventPeople(EventPeoples eventPeople);
        void UpdateEventPeople(EventPeoples eventPeople);
        void DeleteEventPeople(EventPeoples eventPeople);
        void DeleteEventPeopleByEventId(List<EventPeoples> listEventPeople);
    }
}
