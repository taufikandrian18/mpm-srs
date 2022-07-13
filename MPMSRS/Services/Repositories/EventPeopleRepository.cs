using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class EventPeopleRepository : UserBase<EventPeoples>, IEventPeopleProfileRepository
    {
        public EventPeopleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventPeople(EventPeoples eventPeople)
        {
            Create(eventPeople);
        }

        public void DeleteEventPeople(EventPeoples eventPeople)
        {
            Update(eventPeople);
        }

        public void CreateEventPeopleBulk(List<EventPeoples> eventPeople)
        {
            CreateBulk(eventPeople);
        }

        public void DeleteEventPeopleByEventId(List<EventPeoples> listEventPeople)
        {
            RepositoryContext.EventPeoples.RemoveRange(listEventPeople);
        }

        public async Task<IEnumerable<EventPeoples>> GetAllEventPeoples(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<EventPeoples> GetEventPeopleByEmployeeId(Guid employeeId)
        {
            return await FindByCondition(un => un.EmployeeId.Equals(employeeId))
                .Where(un => un.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<EventPeoples> GetEventPeopleById(Guid eventPeopleId)
        {
            return await FindByCondition(un => un.EventPeopleId.Equals(eventPeopleId))
                .Where(un => un.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EventPeopleDtoViewModel>> GetEventPeopleByEventId(Guid eventId, int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventId == eventId)
                .Join(RepositoryContext.Users,
                v => v.EmployeeId,
                u => u.EmployeeId,
                (v, u) => new { v, u })
                .Select((x) => new EventPeopleDtoViewModel
                {
                    EventPeopleId = x.v.EventPeopleId,
                    EventId = x.v.EventId,
                    EmployeeId = x.v.EmployeeId,
                    EmployeeName = x.u.DisplayName,
                    CreatedAt = x.v.CreatedAt,
                    CreatedBy = x.v.CreatedBy,
                    UpdatedAt = x.v.UpdatedAt,
                    UpdatedBy = x.v.UpdatedBy
                })
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventPeoples>> GetVisitingPeopleByEventIdWithoutPage(Guid eventId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventId == eventId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }

        public void UpdateEventPeople(EventPeoples eventPeople)
        {
            Update(eventPeople);
        }
    }
}
