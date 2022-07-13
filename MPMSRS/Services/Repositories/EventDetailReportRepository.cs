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
    public class EventDetailReportRepository : UserBase<EventDetailReports>, IEventDetailReportProfileRepository
    {
        public EventDetailReportRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEventDetailReport(EventDetailReports eventDetailReport)
        {
            Create(eventDetailReport);
        }

        public void DeleteEventDetailReport(EventDetailReports eventDetailReport)
        {
            Update(eventDetailReport);
        }

        public async Task<EventDetailReportDto> GetAllEventDetailReport(Guid eventId, int pageSize, int pageIndex)
        {
            var detailReportList = await(from vdr in RepositoryContext.EventDetailReports
                                         where vdr.IsDeleted == false && vdr.EventId == eventId
                                         orderby vdr.CreatedAt descending
                                         select new EventDetailReportDtoViewModel
                                         {
                                             EventDetailReportId = vdr.EventDetailReportId,
                                             EventDetailReportPC = vdr.EventDetailReportProblemCategories.ToList(),
                                             EventDetailReportPIC = RepositoryContext.EventDetailReportPICs.Where(c => c.EventDetailReportId == vdr.EventDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                             EventDetailReportAttachment = vdr.EventDetailReportAttachments.ToList(),
                                             Division = vdr.Division,
                                             EventDetailReportStatus = vdr.EventDetailReportStatus,
                                             EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                             EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                             EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                             EventDetailReportFlagging = vdr.EventDetailReportFlagging,
                                             CreatedAt = vdr.CreatedAt,
                                             CreatedBy = vdr.CreatedBy,
                                             UpdatedAt = vdr.UpdatedAt,
                                             UpdatedBy = vdr.UpdatedBy
                                         })
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            for (int i = 0; i < detailReportList.Count(); i++)
            {
                var detailImageList = new List<ImageUrl>();
                for (int j = 0; j < detailReportList[i].EventDetailReportAttachment.Count(); j++)
                {
                    var imageList = new ImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(detailReportList[i].EventDetailReportAttachment[j].AttachmentId);

                    imageList.VisitingDetailReportId = detailReportList[i].EventDetailReportAttachment[j].EventDetailReportId;
                    imageList.VisitingDetailReportAttachmentId = detailReportList[i].EventDetailReportAttachment[j].EventDetailReportAttachmentId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                detailReportList[i].EventDetailAttachmentDetail = detailImageList;
            }

            var index = 0;
            foreach (var datum in detailReportList)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));

                detailReportList[index].CreatedByName = createdName;
                index++;
            }

            var mstLst = await(from v in RepositoryContext.Events
                               join vp in RepositoryContext.EventPeoples on v.EventId equals vp.EventId into vvp
                               from vp in vvp.DefaultIfEmpty()
                               where v.IsDeleted == false && v.EventId == eventId
                               select new EventDetailReportDto
                               {
                                   EventId = v.EventId,
                                   EventMasterDataId = v.EventMasterDataId,
                                   EventMasterDataName = RepositoryContext.EventMasterDatas.Where(x => x.EventMasterDataId == v.EventMasterDataId).Select(x => x.EventMasterDataName).FirstOrDefault(),
                                   EventMasterDataLocation = RepositoryContext.EventMasterDatas.Where(x => x.EventMasterDataId == v.EventMasterDataId).Select(x => x.EventMasterDataLocation).FirstOrDefault(),
                                   EventMasterDataLatitude = RepositoryContext.EventMasterDatas.Where(x => x.EventMasterDataId == v.EventMasterDataId).Select(x => x.EventMasterDataLatitude).FirstOrDefault(),
                                   EventMasterDataLongitude = RepositoryContext.EventMasterDatas.Where(x => x.EventMasterDataId == v.EventMasterDataId).Select(x => x.EventMasterDataLongitude).FirstOrDefault(),
                                   EventComment = v.EventComment,
                                   EventStatus = v.EventStatus,
                                   ApprovedByManager = v.ApprovedByManager,
                                   ApprovedByGM = v.ApprovedByGM,
                                   IsOnline = v.IsOnline,
                                   StartDate = v.StartDate,
                                   EndDate = v.EndDate,
                                   EventPeoples = RepositoryContext.EventPeoples.Where(c => c.EventId == v.EventId).Select(c => new EmployeeListInEventPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.Username }).ToList(),
                                   EventDetailReport = detailReportList,
                                   CreatedAt = v.CreatedAt,
                                   CreatedBy = v.CreatedBy,
                                   UpdatedAt = v.UpdatedAt,
                                   UpdatedBy = v.UpdatedBy,
                               })
                          .FirstOrDefaultAsync();

            var createdNameMst = await GetCreatedByName(Guid.Parse(mstLst.CreatedBy));
            var updatedNameMst = await GetCreatedByName(Guid.Parse(mstLst.UpdatedBy));

            mstLst.CreatedByName = createdNameMst;
            mstLst.UpdatedByName = updatedNameMst;

            return mstLst;
        }

        public async Task<string> GetAttachmentUrl(Guid attachmentId)
        {
            return await(from usr in RepositoryContext.Attachments
                         where usr.AttachmentId == attachmentId
                         select usr.AttachmentUrl).FirstOrDefaultAsync();
        }

        public async Task<string> GetCreatedByName(Guid employeeId)
        {
            return await(from usr in RepositoryContext.Users
                         where usr.EmployeeId == employeeId
                         select usr.DisplayName).FirstOrDefaultAsync();
        }

        public async Task<EventDetailReportDtoViewModel> GetEventDetailReportByEventDetailReportId(Guid eventDetailReportId)
        {
            var lst = await(from vdr in RepositoryContext.EventDetailReports
                            where vdr.IsDeleted == false && vdr.EventDetailReportId == eventDetailReportId
                            orderby vdr.CreatedAt descending
                            select new EventDetailReportDtoViewModel
                            {
                                EventDetailReportId = vdr.EventDetailReportId,
                                EventDetailReportPC = vdr.EventDetailReportProblemCategories.ToList(),
                                EventDetailReportPIC = RepositoryContext.EventDetailReportPICs.Where(c => c.EventDetailReportId == vdr.EventDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                EventDetailReportAttachment = vdr.EventDetailReportAttachments.ToList(),
                                Division = vdr.Division,
                                EventDetailReportStatus = vdr.EventDetailReportStatus,
                                EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                EventDetailReportFlagging = vdr.EventDetailReportFlagging,
                                CreatedAt = vdr.CreatedAt,
                                CreatedBy = vdr.CreatedBy,
                                UpdatedAt = vdr.UpdatedAt,
                                UpdatedBy = vdr.UpdatedBy
                            })
                            .FirstOrDefaultAsync();


            var detailImageList = new List<ImageUrl>();
            for (int j = 0; j < lst.EventDetailReportAttachment.Count(); j++)
            {
                var imageList = new ImageUrl();
                var attachmentUrl = await GetAttachmentUrl(lst.EventDetailReportAttachment[j].AttachmentId);

                imageList.VisitingDetailReportId = lst.EventDetailReportAttachment[j].EventDetailReportId;
                imageList.VisitingDetailReportAttachmentId = lst.EventDetailReportAttachment[j].EventDetailReportAttachmentId;
                imageList.imageUrl = attachmentUrl;

                detailImageList.Add(imageList);
            }

            lst.EventDetailAttachmentDetail = detailImageList;

            var createdName = await GetCreatedByName(Guid.Parse(lst.CreatedBy));
            var updatedName = await GetCreatedByName(Guid.Parse(lst.UpdatedBy));

            lst.CreatedByName = createdName;
            lst.UpdatedByName = updatedName;

            return lst;
        }

        public async Task<IEnumerable<EventDetailReports>> GetEventDetailReportByEventIdWithoutPage(Guid eventId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EventId == eventId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }

        public async Task<EventDetailReports> GetEventDetailReportById(Guid eventDetailReportId)
        {
            return await FindByCondition(vt => vt.EventDetailReportId.Equals(eventDetailReportId))
                .Where(vt => vt.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EventDetailReportDtoViewModel>> GetEventDetailReportIdentification(Guid eventId, int pageSize, int pageIndex)
        {
            var lst = await(from vdr in RepositoryContext.EventDetailReports
                            where vdr.IsDeleted == false && vdr.EventId == eventId
                            orderby vdr.CreatedAt descending
                            select new EventDetailReportDtoViewModel
                            {
                                EventDetailReportId = vdr.EventDetailReportId,
                                EventDetailReportPC = vdr.EventDetailReportProblemCategories.ToList(),
                                EventDetailReportPIC = RepositoryContext.EventDetailReportPICs.Where(c => c.EventDetailReportId == vdr.EventDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                EventDetailReportAttachment = vdr.EventDetailReportAttachments.ToList(),
                                Division = vdr.Division,
                                EventDetailReportStatus = vdr.EventDetailReportStatus,
                                EventDetailReportProblemIdentification = vdr.EventDetailReportProblemIdentification,
                                EventCAProblemIdentification = vdr.EventCAProblemIdentification,
                                EventDetailReportDeadline = vdr.EventDetailReportDeadline.HasValue ? vdr.EventDetailReportDeadline.Value : DateTime.MinValue,
                                EventDetailReportFlagging = vdr.EventDetailReportFlagging,
                                CreatedAt = vdr.CreatedAt,
                                CreatedBy = vdr.CreatedBy,
                                UpdatedAt = vdr.UpdatedAt,
                                UpdatedBy = vdr.UpdatedBy
                            })
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();


            for (int i = 0; i < lst.Count(); i++)
            {
                var detailImageList = new List<ImageUrl>();
                for (int j = 0; j < lst[i].EventDetailReportAttachment.Count(); j++)
                {
                    var imageList = new ImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(lst[i].EventDetailReportAttachment[j].AttachmentId);

                    imageList.VisitingDetailReportId = lst[i].EventDetailReportAttachment[j].EventDetailReportId;
                    imageList.VisitingDetailReportAttachmentId = lst[i].EventDetailReportAttachment[j].EventDetailReportAttachmentId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                lst[i].EventDetailAttachmentDetail = detailImageList;
            }

            var index = 0;
            foreach (var datum in lst)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                lst[index].CreatedByName = createdName;
                lst[index].UpdatedByName = updatedName;
                index++;
            }

            return lst;
        }

        public void UpdateBulkEventDetailReport(List<EventDetailReports> eventDetailReport)
        {
            var tempVdr = RepositoryContext.EventDetailReports
                .Where(te => eventDetailReport[0].EventId.Equals(te.EventId))
                .ToList();

            var index = 0;
            foreach (var item in tempVdr)
            {
                tempVdr[index].EventDetailReportStatus = "on progress";
                Update(tempVdr[index]);
                index++;
            }
        }

        public void UpdateEventDetailReport(EventDetailReports eventDetailReport)
        {
            Update(eventDetailReport);
        }
    }
}
