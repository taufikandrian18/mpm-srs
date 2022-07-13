using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MPMSRS.Services.Repositories
{
    public class EventRepository : UserBase<Events>, IEventProfileRepository
    {
        public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEvent(Events events)
        {
            Create(events);
        }

        public void DeleteEvent(Events events)
        {
            Update(events);
        }

        public async Task<IEnumerable<EventDtoViewModel>> GetAllEventByCreateLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string location, string query)
        {
            var eventList = await(from emp in RepositoryContext.EventPeoples
                                where emp.EmployeeId == EmployeeId
                                select emp.EventId).ToListAsync();

            var lstContext = (from vs in RepositoryContext.Events
                              join nt in RepositoryContext.EventMasterDatas on vs.EventMasterDataId equals nt.EventMasterDataId
                              join vt in RepositoryContext.VisitingTypes on vs.VisitingTypeId equals vt.VisitingTypeId
                              where vs.IsDeleted == false && eventList.Contains(vs.EventId)
                              select new EventDtoViewModel
                              {
                                  EventId = vs.EventId,
                                  VisitingTypeId = vs.VisitingTypeId,
                                  VisitingTypeName = vt.VisitingTypeName,
                                  EventMasterDataId = vs.EventMasterDataId,
                                  EventMasterDataName = nt.EventMasterDataName,
                                  EventMasterDataLocation = nt.EventMasterDataLocation,
                                  EventMasterDataLatitude = nt.EventMasterDataLatitude,
                                  EventMasterDataLongitude = nt.EventMasterDataLongitude,
                                  EventPeoples = RepositoryContext.EventPeoples.Where(c => c.EventId == vs.EventId).Select(c => new EventPeopleDtoViewModel { EventPeopleId = c.EventPeopleId, EmployeeId = c.EmployeeId, EmployeeName = c.User.Username, CreatedAt = c.CreatedAt, CreatedBy = c.CreatedBy, UpdatedAt = c.UpdatedAt, UpdatedBy = c.UpdatedBy }).ToList(),
                                  EventComment = vs.EventComment,
                                  EventStatus = vs.EventStatus,
                                  ApprovedByGM = vs.ApprovedByGM,
                                  ApprovedByManager = vs.ApprovedByManager,
                                  IsOnline = vs.IsOnline,
                                  StartDate = vs.StartDate,
                                  EndDate = vs.EndDate,
                                  CreatedAt = vs.CreatedAt,
                                  CreatedBy = vs.CreatedBy,
                                  UpdatedAt = vs.UpdatedAt,
                                  UpdatedBy = vs.UpdatedBy,
                              })
                            .AsNoTracking()
                            .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(location))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == location.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataName.ToLower().Contains(query.ToLower()) || Q.EventStatus.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.StartDate.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.StartDate.Date >= DateTime.Parse(startDate).Date && Q.EndDate <= DateTime.Parse(endDate).Date);
                }
            }

            var data = await lstContext.OrderByDescending(Q => Q.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<EventDtoViewModel>> GetAllEventByPeopleLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string location, string query)
        {
            var lstContext = (from vs in RepositoryContext.Events
                              join nt in RepositoryContext.EventMasterDatas on vs.EventMasterDataId equals nt.EventMasterDataId
                              join vt in RepositoryContext.VisitingTypes on vs.VisitingTypeId equals vt.VisitingTypeId
                              where vs.CreatedBy == EmployeeId.ToString() && vs.IsDeleted == false
                              select new EventDtoViewModel
                              {
                                  EventId = vs.EventId,
                                  VisitingTypeId = vs.VisitingTypeId,
                                  VisitingTypeName = vt.VisitingTypeName,
                                  EventMasterDataId = vs.EventMasterDataId,
                                  EventMasterDataName = nt.EventMasterDataName,
                                  EventMasterDataLocation = nt.EventMasterDataLocation,
                                  EventMasterDataLatitude = nt.EventMasterDataLatitude,
                                  EventMasterDataLongitude = nt.EventMasterDataLongitude,
                                  EventPeoples = RepositoryContext.EventPeoples.Where(c => c.EventId == vs.EventId).Select(c => new EventPeopleDtoViewModel { EventPeopleId = c.EventPeopleId, EmployeeId = c.EmployeeId, EmployeeName = c.User.Username, CreatedAt = c.CreatedAt, CreatedBy = c.CreatedBy, UpdatedAt = c.UpdatedAt, UpdatedBy = c.UpdatedBy }).ToList(),
                                  EventComment = vs.EventComment,
                                  EventStatus = vs.EventStatus,
                                  ApprovedByGM = vs.ApprovedByGM,
                                  ApprovedByManager = vs.ApprovedByManager,
                                  IsOnline = vs.IsOnline,
                                  StartDate = vs.StartDate,
                                  EndDate = vs.EndDate,
                                  CreatedAt = vs.CreatedAt,
                                  CreatedBy = vs.CreatedBy,
                                  UpdatedAt = vs.UpdatedAt,
                                  UpdatedBy = vs.UpdatedBy,
                              })
                            .AsNoTracking()
                            .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(location))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == location.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataName.ToLower().Contains(query.ToLower()) || Q.EventStatus.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.StartDate.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.StartDate.Date >= DateTime.Parse(startDate).Date && Q.EndDate <= DateTime.Parse(endDate).Date);
                }
            }

            var data = await lstContext.OrderByDescending(Q => Q.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<EventDtoViewModel>> GetAllEvents(int pageSize, int pageIndex, string status, string startDate, string endDate, string location, string query)
        {
            try
            {
                var lstContext = RepositoryContext.Events
                    .Where(ow => ow.IsDeleted == false)
                    .Join(RepositoryContext.EventMasterDatas,
                    u => u.EventMasterDataId,
                    n => n.EventMasterDataId,
                    (u, n) => new { u, n })
                    .Select((x) => new EventDtoViewModel
                    {
                        EventId = x.u.EventId,
                        VisitingTypeId = x.u.VisitingTypeId,
                        VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == x.u.VisitingTypeId).Select(c => c.VisitingTypeName).FirstOrDefault(),
                        EventMasterDataId = x.u.EventMasterDataId,
                        EventMasterDataName = x.n.EventMasterDataName,
                        EventMasterDataLocation = x.n.EventMasterDataLocation,
                        EventMasterDataLatitude = x.n.EventMasterDataLatitude,
                        EventMasterDataLongitude = x.n.EventMasterDataLongitude,
                        EventPeoples = RepositoryContext.EventPeoples.Where(c => c.EventId == x.u.EventId).Select(c => new EventPeopleDtoViewModel { EventPeopleId = c.EventPeopleId, EmployeeId = c.EmployeeId, EmployeeName = c.User.Username, CreatedAt = c.CreatedAt, CreatedBy = c.CreatedBy, UpdatedAt = c.UpdatedAt, UpdatedBy = c.UpdatedBy }).ToList(),
                        EventComment = x.u.EventComment,
                        EventStatus = x.u.EventStatus,
                        ApprovedByGM = x.u.ApprovedByGM,
                        ApprovedByManager = x.u.ApprovedByManager,
                        IsOnline = x.u.IsOnline,
                        StartDate = x.u.StartDate,
                        EndDate = x.u.EndDate,
                        CreatedAt = x.u.CreatedAt,
                        CreatedBy = x.u.CreatedBy,
                        UpdatedAt = x.u.UpdatedAt,
                        UpdatedBy = x.u.UpdatedBy,
                    })
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    lstContext = lstContext.Where(Q => Q.EventStatus.ToLower() == status.ToLower());
                }

                if (!string.IsNullOrEmpty(location))
                {
                    lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == location.ToLower());
                }

                if (!string.IsNullOrEmpty(query))
                {
                    lstContext = lstContext.Where(Q => Q.EventMasterDataName.ToLower().Contains(query.ToLower()) || Q.EventStatus.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()));
                }

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    if (startDate.Trim() == endDate.Trim())
                    {
                        lstContext = lstContext.Where(Q => Q.StartDate.Date == DateTime.Parse(startDate).Date);
                    }
                    else
                    {
                        lstContext = lstContext.Where(Q => Q.StartDate.Date >= DateTime.Parse(startDate).Date && Q.EndDate <= DateTime.Parse(endDate).Date);
                    }
                }

                var dataResult = await lstContext.OrderByDescending(Q => Q.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

                int index = 0;
                foreach (var datum in dataResult)
                {
                    if (!string.IsNullOrEmpty(datum.CreatedBy))
                    {
                        dataResult[index].CreatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(datum.CreatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
                    }

                    if (!string.IsNullOrEmpty(datum.UpdatedBy))
                    {
                        dataResult[index].UpdatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(datum.UpdatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
                    }

                    index++;
                }

                return dataResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Events> GetEventById(Guid eventId)
        {
            return await FindByCondition(vt => vt.EventId.Equals(eventId))
                .Where(vt => vt.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<EventDtoViewDetailModel> GetEventDetailById(Guid eventId)
        {
            return await RepositoryContext.Events
                .Where(ow => ow.IsDeleted == false && ow.EventId.Equals(eventId))
                .GroupJoin(RepositoryContext.EventMasterDatas,
                u => u.EventMasterDataId,
                n => n.EventMasterDataId,
                (u, n) => new { u, n })
                .SelectMany(x => x.n.DefaultIfEmpty(), (x, c) => new EventDtoViewDetailModel
                {
                    EventId = x.u.EventId,
                    VisitingTypeId = x.u.VisitingTypeId,
                    VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == x.u.VisitingTypeId).Select(c => c.VisitingTypeName).FirstOrDefault(),
                    EventMasterDataId = x.u.EventMasterDataId,
                    EventMasterDataName = c.EventMasterDataName,
                    EventMasterDataLocation = c.EventMasterDataLocation,
                    EventMasterDataLatitude = c.EventMasterDataLatitude,
                    EventMasterDataLongitude = c.EventMasterDataLongitude,
                    EventPeoples = RepositoryContext.EventPeoples.Where(c => c.EventId == x.u.EventId).Select(c => new EventPeopleDtoViewModel { EventPeopleId = c.EventPeopleId, EmployeeId = c.EmployeeId, EmployeeName = c.User.Username, CreatedAt = c.CreatedAt, CreatedBy = c.CreatedBy, UpdatedAt = c.UpdatedAt, UpdatedBy = c.UpdatedBy }).ToList(),
                    EventComment = x.u.EventComment,
                    EventStatus = x.u.EventStatus,
                    ApprovedByGM = x.u.ApprovedByGM,
                    ApprovedByManager = x.u.ApprovedByManager,
                    IsOnline = x.u.IsOnline,
                    StartDate = x.u.StartDate,
                    EndDate = x.u.EndDate,
                    CreatedAt = x.u.CreatedAt,
                    CreatedBy = x.u.CreatedBy,
                    UpdatedAt = x.u.UpdatedAt,
                    UpdatedBy = x.u.UpdatedBy,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> GetEventIsOnline(Guid eventId)
        {
            return await FindByCondition(vt => vt.EventId.Equals(eventId))
                .Where(vt => vt.IsDeleted == false)
                .Select(vt => vt.IsOnline)
                .FirstOrDefaultAsync();
        }

        public void UpdateEvent(Events events)
        {
            Update(events);
        }

        public async Task<string> GetCreatedByName(Guid employeeId)
        {
            return await (from usr in RepositoryContext.Users
                          where usr.EmployeeId == employeeId
                          select usr.DisplayName).FirstOrDefaultAsync();
        }
    }
}
