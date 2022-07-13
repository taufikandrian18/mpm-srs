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
    public class EventCARepository : UserBase<EventCAs>, IEventCAProfileRepository
    {
        public EventCARepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventCorrectiveActions(EventCAs eventCA)
        {
            Create(eventCA);
        }

        public async Task<EventCAs> GetEventCorrectiveActionById(Guid eventCAId)
        {
            return await FindByCondition(ca => ca.EventCAId.Equals(eventCAId))
                .Where(ca => ca.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<EventCADetailDto> GetEventCorrectiveActionDetailById(Guid eventCAId)
        {
            var detailReportList = await(from ca in RepositoryContext.EventCAs
                                         join vdr in RepositoryContext.EventDetailReports on ca.EventDetailReportId equals vdr.EventDetailReportId
                                         join caAtt in RepositoryContext.EventCAAttachments on ca.EventCAId equals caAtt.EventCAId into grpAtt
                                         from caAtt in grpAtt.DefaultIfEmpty()
                                         join v in RepositoryContext.Events on vdr.EventId equals v.EventId
                                         where ca.EventCAId == eventCAId && ca.IsDeleted == false
                                         select new EventCADetailDto
                                         {
                                             EventCAId = ca.EventCAId,
                                             EventId = vdr.EventId,
                                             EventMasterDataId = v.EventMasterDataId,
                                             EventMasterDataName = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == v.EventMasterDataId).Select(c => c.EventMasterDataName).FirstOrDefault(),
                                             EventDetailReportId = ca.EventDetailReportId,
                                             EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                             EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                             EventDetailReportStatus = vdr.EventDetailReportStatus,
                                             EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                             AttachmentDetail = RepositoryContext.EventCAAttachments.Where(c => c.EventCAId == ca.EventCAId).Select(c => new EventCAAttachmentDetail { EventCAAttachmentId = c.EventCAAttachmentId, AttachmentId = c.AttachmentId, ImageUrl = RepositoryContext.Attachments.Where(x => x.AttachmentId == c.AttachmentId).Select(x => x.AttachmentUrl).FirstOrDefault() }).ToList(),
                                             EventCAName = ca.EventCAName,
                                             ProgressBy = ca.ProgressBy,
                                             ValidateBy = ca.ValidateBy,
                                             EventCADetail = ca.EventCADetail,
                                             CreatedAt = ca.CreatedAt,
                                             CreatedBy = ca.CreatedBy,
                                             UpdatedAt = ca.UpdatedAt,
                                             UpdatedBy = ca.UpdatedBy
                                         })
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(detailReportList.ProgressBy))
            {
                detailReportList.ProgressByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(detailReportList.ProgressBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
            }

            if (!string.IsNullOrEmpty(detailReportList.ValidateBy))
            {
                detailReportList.ValidateByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(detailReportList.ValidateBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
            }

            if (!string.IsNullOrEmpty(detailReportList.CreatedBy))
            {
                detailReportList.CreatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(detailReportList.CreatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
            }

            if (!string.IsNullOrEmpty(detailReportList.UpdatedBy))
            {
                detailReportList.UpdatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(detailReportList.UpdatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
            }

            return detailReportList;
        }

        public async Task<IEnumerable<EventCADtoVMList>> GetListAllUserCA(int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            var lstContext = (from ca in RepositoryContext.EventCAs
                              join vdr in RepositoryContext.EventDetailReports on ca.EventDetailReportId equals vdr.EventDetailReportId
                              where vdr.IsDeleted == false && ca.IsDeleted == false && vdr.EventDetailReportStatus.Trim() != "draft"
                              select new EventCADtoVMList
                              {
                                  EventId = vdr.EventId,
                                  EventMasterDataId = RepositoryContext.Events.Where(c => c.EventId == vdr.EventId).Select(c => c.EventMasterDataId).FirstOrDefault(),
                                  EventMasterDataName = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataName).FirstOrDefault(),
                                  EventMasterDataLocation = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLocation).FirstOrDefault(),
                                  EventMasterDataLatitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLatitude).FirstOrDefault(),
                                  EventMasterDataLongitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLongitude).FirstOrDefault(),
                                  CorrectiveActionId = ca.EventCAId,
                                  EventDetailReportId = ca.EventDetailReportId,
                                  EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                  EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                  EventDetailReportStatus = vdr.EventDetailReportStatus,
                                  EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                  EventCAName = ca.EventCAName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  EventCADetail = ca.EventCADetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.EventCAName.ToLower().Contains(query.ToLower()) || Q.EventCADetail.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()) || Q.EventMasterDataName.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.EventDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            var data = await lstContext.OrderBy(Q => Q.EventDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public async Task<EventCADtoViewModel> GetListCAByEventMasterData(Guid eventMasterDataId, int pageSize, int pageIndex, string status, string sortBy)
        {
            var lstContext = (from ca in RepositoryContext.EventCAs
                              join vdr in RepositoryContext.EventDetailReports on ca.EventDetailReportId equals vdr.EventDetailReportId
                              where vdr.IsDeleted == false && ca.IsDeleted == false && vdr.EventMasterDataId == eventMasterDataId
                              select new EventCADtoVMList
                              {
                                  EventId = vdr.EventId,
                                  EventMasterDataId = RepositoryContext.Events.Where(c => c.EventId == vdr.EventId).Select(c => c.EventMasterDataId).FirstOrDefault(),
                                  EventMasterDataName = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataName).FirstOrDefault(),
                                  EventMasterDataLocation = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLocation).FirstOrDefault(),
                                  EventMasterDataLatitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLatitude).FirstOrDefault(),
                                  EventMasterDataLongitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLongitude).FirstOrDefault(),
                                  CorrectiveActionId = ca.EventCAId,
                                  EventDetailReportId = ca.EventDetailReportId,
                                  EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                  EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                  EventDetailReportStatus = vdr.EventDetailReportStatus,
                                  EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                  EventCAName = ca.EventCAName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  EventCADetail = ca.EventCADetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventDetailReportStatus.ToLower() == status.ToLower());
            }

            switch (sortBy)
            {
                case "old":
                    lstContext = lstContext.OrderBy(Q => Q.CreatedAt);
                    break;
                case "new":
                    lstContext = lstContext.OrderByDescending(Q => Q.CreatedAt);
                    break;
                default:
                    lstContext = lstContext.OrderBy(Q => Q.EventDetailReportDeadline);
                    break;
            }

            PagesViewModel pages = new PagesViewModel();
            pages.Total = lstContext.Count();
            pages.PerPage = pageSize;
            pages.Page = pageIndex;
            pages.LstPage = pages.Total / pageSize;

            var data = await lstContext.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new EventCADtoViewModel
            {
                Data = data,
                Pages = pages
            };
        }

        public async Task<IEnumerable<EventCADtoVMList>> GetListCAByEventMasterDataLocation(string eventMasterDataLocation, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {

            var eventList = await (from emp in RepositoryContext.EventMasterDatas
                                   where emp.EventMasterDataLocation.Trim().ToLower() == eventMasterDataLocation.Trim().ToLower()
                                   select emp.EventMasterDataId).ToListAsync();

            var lstContext = (from ca in RepositoryContext.EventCAs
                              join vdr in RepositoryContext.EventDetailReports on ca.EventDetailReportId equals vdr.EventDetailReportId
                              join nt in RepositoryContext.Events on vdr.EventDetailReportId equals nt.EventMasterDataId
                              where eventList.Contains(nt.EventMasterDataId) && vdr.IsDeleted == false && ca.IsDeleted == false && vdr.EventDetailReportStatus.Trim() != "draft"
                              select new EventCADtoVMList
                              {
                                  EventId = vdr.EventId,
                                  EventMasterDataId = RepositoryContext.Events.Where(c => c.EventId == vdr.EventId).Select(c => c.EventMasterDataId).FirstOrDefault(),
                                  EventMasterDataName = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataName).FirstOrDefault(),
                                  EventMasterDataLocation = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLocation).FirstOrDefault(),
                                  EventMasterDataLatitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLatitude).FirstOrDefault(),
                                  EventMasterDataLongitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLongitude).FirstOrDefault(),
                                  CorrectiveActionId = ca.EventCAId,
                                  EventDetailReportId = ca.EventDetailReportId,
                                  EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                  EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                  EventDetailReportStatus = vdr.EventDetailReportStatus,
                                  EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                  EventCAName = ca.EventCAName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  EventCADetail = ca.EventCADetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.EventCAName.ToLower().Contains(query.ToLower()) || Q.EventCADetail.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()) || Q.EventMasterDataName.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.EventDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            var data = await lstContext.OrderBy(Q => Q.EventDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<EventCADtoVMList>> GetListCAByMainDealer(Guid employeeId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            var lstContext = (from ca in RepositoryContext.EventCAs
                              join vdr in RepositoryContext.EventDetailReports on ca.EventDetailReportId equals vdr.EventDetailReportId
                              where vdr.CreatedBy == employeeId.ToString().Trim() && vdr.IsDeleted == false && ca.IsDeleted == false && vdr.EventDetailReportStatus.Trim() != "draft"
                              select new EventCADtoVMList
                              {
                                  EventId = vdr.EventId,
                                  EventMasterDataId = RepositoryContext.Events.Where(c => c.EventId == vdr.EventId).Select(c => c.EventMasterDataId).FirstOrDefault(),
                                  EventMasterDataName = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataName).FirstOrDefault(),
                                  EventMasterDataLocation = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLocation).FirstOrDefault(),
                                  EventMasterDataLatitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLatitude).FirstOrDefault(),
                                  EventMasterDataLongitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLongitude).FirstOrDefault(),
                                  CorrectiveActionId = ca.EventCAId,
                                  EventDetailReportId = ca.EventDetailReportId,
                                  EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                  EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                  EventDetailReportStatus = vdr.EventDetailReportStatus,
                                  EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                  EventCAName = ca.EventCAName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  EventCADetail = ca.EventCADetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.EventCAName.ToLower().Contains(query.ToLower()) || Q.EventCADetail.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()) || Q.EventMasterDataName.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.EventDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            var data = await lstContext.OrderBy(Q => Q.EventDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<EventCADtoVMList>> GetListCAByPICTagged(Guid employeeId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            var picList = await(from emp in RepositoryContext.EventDetailReportPICs
                                where emp.EmployeeId == employeeId
                                select emp.EventDetailReportId).Distinct().ToListAsync();

            var lstContext = (from ca in RepositoryContext.EventCAs
                              join vdr in RepositoryContext.EventDetailReports on ca.EventDetailReportId equals vdr.EventDetailReportId
                              where vdr.CreatedBy == employeeId.ToString().Trim() && vdr.IsDeleted == false && ca.IsDeleted == false && vdr.EventDetailReportStatus.Trim() != "draft" && picList.Contains(vdr.EventDetailReportId)
                              select new EventCADtoVMList
                              {
                                  EventId = vdr.EventId,
                                  EventMasterDataId = RepositoryContext.Events.Where(c => c.EventId == vdr.EventId).Select(c => c.EventMasterDataId).FirstOrDefault(),
                                  EventMasterDataName = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataName).FirstOrDefault(),
                                  EventMasterDataLocation = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLocation).FirstOrDefault(),
                                  EventMasterDataLatitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLatitude).FirstOrDefault(),
                                  EventMasterDataLongitude = RepositoryContext.EventMasterDatas.Where(c => c.EventMasterDataId == RepositoryContext.Events.Where(x => x.EventId == vdr.EventId).Select(x => x.EventMasterDataId).FirstOrDefault()).Select(c => c.EventMasterDataLongitude).FirstOrDefault(),
                                  CorrectiveActionId = ca.EventCAId,
                                  EventDetailReportId = ca.EventDetailReportId,
                                  EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                  EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                  EventDetailReportStatus = vdr.EventDetailReportStatus,
                                  EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                  EventCAName = ca.EventCAName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  EventCADetail = ca.EventCADetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.EventDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.EventMasterDataLocation.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.EventCAName.ToLower().Contains(query.ToLower()) || Q.EventCADetail.ToLower().Contains(query.ToLower()) || Q.EventMasterDataLocation.ToLower().Contains(query.ToLower()) || Q.EventMasterDataName.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.EventDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.EventDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            var data = await lstContext.OrderBy(Q => Q.EventDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return data;
        }

        public void UpdateEventCorrectiveActions(EventCAs eventCA)
        {
            Update(eventCA);
        }
    }
}
