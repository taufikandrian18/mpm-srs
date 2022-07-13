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
    public class ChecklistRepository : UserBase<Checklists>, IChecklistProfileRepository
    {
        public ChecklistRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateChecklist(Checklists checklist)
        {
            Create(checklist);
        }

        public void DeleteChecklist(Checklists checklist)
        {
            Update(checklist);
        }

        public async Task<ChecklistDtoLoginViewModel> GetAllChecklists(Guid visitingId, int pageSize, int pageIndex)
        {
            var detailReportList = await(from vdr in RepositoryContext.Checklists
                                         where vdr.IsDeleted == false && vdr.VisitingId == visitingId
                                         orderby vdr.CreatedAt descending
                                         select new ChecklistViewModel
                                         {
                                             ChecklistId = vdr.ChecklistId,
                                             ChecklistPICs = RepositoryContext.ChecklistPICs.Where(c => c.ChecklistId == vdr.ChecklistId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                             ChecklistEvidences = vdr.ChecklistEvidences.ToList(),
                                             Network = vdr.Network,
                                             ChecklistFotoStandardId = vdr.AttachmentId,
                                             ChecklistItem = vdr.ChecklistItem,
                                             ChecklistIdentification = vdr.ChecklistIdentification,
                                             ChecklistActualCondition = vdr.ChecklistActualCondition,
                                             ChecklistActualDetail = vdr.ChecklistActualDetail,
                                             ChecklistFix = vdr.ChecklistFix,
                                             ChecklistStatus = vdr.ChecklistStatus,
                                             ChecklistDeadline = vdr.ChecklistDeadline.HasValue ? vdr.ChecklistDeadline.Value : DateTime.MinValue,
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
                var detailImageList = new List<ChecklistImageUrl>();

                var attachmentUrlFotoStandard = await GetAttachmentUrl(detailReportList[i].ChecklistFotoStandardId);
                detailReportList[i].ChecklistFotoStandardUrl = attachmentUrlFotoStandard;

                for (int j = 0; j < detailReportList[i].ChecklistEvidences.Count(); j++)
                {
                    var imageList = new ChecklistImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(detailReportList[i].ChecklistEvidences[j].AttachmentId);

                    imageList.ChecklistId = detailReportList[i].ChecklistEvidences[j].ChecklistId;
                    imageList.ChecklistEvidenceId = detailReportList[i].ChecklistEvidences[j].ChecklistEvidenceId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                detailReportList[i].ChecklistEvidenceDetail = detailImageList;
            }

            var index = 0;
            foreach (var datum in detailReportList)
            {
                if (Guid.TryParse(datum.CreatedBy, out Guid createdNameGuid) && Guid.TryParse(datum.UpdatedBy, out Guid updatedNameGuid))
                {
                    var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                    var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                    detailReportList[index].CreatedByName = createdName;
                    detailReportList[index].UpdatedByName = updatedName;
                    index++;
                }
                else
                {
                    detailReportList[index].CreatedByName = datum.CreatedBy;
                    detailReportList[index].UpdatedByName = datum.UpdatedBy;
                    index++;
                }
            }

            var mstLst = await(from v in RepositoryContext.Visitings
                               join vp in RepositoryContext.VisitingPeoples on v.VisitingId equals vp.VisitingId into vvp
                               from vp in vvp.DefaultIfEmpty()
                               join vnm in RepositoryContext.VisitingNoteMappings on v.VisitingId equals vnm.VisitingId into vvnm
                               from vnm in vvnm.DefaultIfEmpty()
                               where v.IsDeleted == false && v.VisitingId == visitingId
                               select new ChecklistDtoLoginViewModel
                               {
                                   VisitingId = v.VisitingId,
                                   VisitingTypeId = v.VisitingTypeId,
                                   VisitingTypeName = RepositoryContext.VisitingTypes.Where(x => x.VisitingTypeId == v.VisitingTypeId).Select(x => x.VisitingTypeName).FirstOrDefault(),
                                   NetworkId = v.NetworkId,
                                   AhmCode = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.AhmCode).FirstOrDefault(),
                                   MDCode = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.MDCode).FirstOrDefault(),
                                   DealerName = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.DealerName).FirstOrDefault(),
                                   NetworkAddress = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.Address).FirstOrDefault(),
                                   NetworkCity = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.City).FirstOrDefault(),
                                   NetworkLatitude = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.NetworkLatitude).FirstOrDefault(),
                                   NetworkLongitude = RepositoryContext.Networks.Where(x => x.NetworkId == v.NetworkId).Select(x => x.NetworkLongitude).FirstOrDefault(),
                                   VisitingComment = v.VisitingComment,
                                   VisitingStatus = v.VisitingStatus,
                                   ApprovedByManager = v.ApprovedByManager,
                                   ApprovedByGM = v.ApprovedByGM,
                                   IsOnline = v.IsOnline,
                                   StartDate = v.StartDate,
                                   EndDate = v.EndDate,
                                   Checklists = detailReportList,
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

        public async Task<ChecklistViewModel> GetChecklistByChecklistId(Guid checklistId)
        {
            var lst = await(from vdr in RepositoryContext.Checklists
                            where vdr.IsDeleted == false && vdr.ChecklistId == checklistId
                            orderby vdr.CreatedAt descending
                            select new ChecklistViewModel
                            {
                                ChecklistId = vdr.ChecklistId,
                                ChecklistPICs = RepositoryContext.ChecklistPICs.Where(c => c.ChecklistId == vdr.ChecklistId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                ChecklistEvidences = vdr.ChecklistEvidences.ToList(),
                                Network = vdr.Network,
                                ChecklistFotoStandardId = vdr.AttachmentId,
                                ChecklistItem = vdr.ChecklistItem,
                                ChecklistIdentification = vdr.ChecklistIdentification,
                                ChecklistActualCondition = vdr.ChecklistActualCondition,
                                ChecklistActualDetail = vdr.ChecklistActualDetail,
                                ChecklistFix = vdr.ChecklistFix,
                                ChecklistStatus = vdr.ChecklistStatus,
                                ChecklistDeadline = vdr.ChecklistDeadline.HasValue ? vdr.ChecklistDeadline.Value : DateTime.MinValue,
                                CreatedAt = vdr.CreatedAt,
                                CreatedBy = vdr.CreatedBy,
                                UpdatedAt = vdr.UpdatedAt,
                                UpdatedBy = vdr.UpdatedBy
                            })
                            .FirstOrDefaultAsync();


            var detailImageList = new List<ChecklistImageUrl>();

            var attachmentUrlFotoStandard = await GetAttachmentUrl(lst.ChecklistFotoStandardId);
            lst.ChecklistFotoStandardUrl = attachmentUrlFotoStandard;

            for (int j = 0; j < lst.ChecklistEvidences.Count(); j++)
            {
                var imageList = new ChecklistImageUrl();
                var attachmentUrl = await GetAttachmentUrl(lst.ChecklistEvidences[j].AttachmentId);

                imageList.ChecklistId = lst.ChecklistEvidences[j].ChecklistId;
                imageList.ChecklistEvidenceId = lst.ChecklistEvidences[j].ChecklistEvidenceId;
                imageList.imageUrl = attachmentUrl;

                detailImageList.Add(imageList);
            }

            lst.ChecklistEvidenceDetail = detailImageList;

            var createdName = await GetCreatedByName(Guid.Parse(lst.CreatedBy));
            var updatedName = await GetCreatedByName(Guid.Parse(lst.UpdatedBy));

            lst.CreatedByName = createdName;
            lst.UpdatedByName = updatedName;

            return lst;
        }

        public async Task<Checklists> GetChecklistById(Guid checklistId)
        {
            return await FindByCondition(vt => vt.ChecklistId.Equals(checklistId))
                .Where(vt => vt.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Checklists>> GetChecklistByVisitingIdWithoutPage(Guid visitingId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingId == visitingId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }

        public async Task<ChecklistIdentificationModel> GetChecklistIdentification(Guid visitingId, int pageSize, int pageIndex)
        {
            var lst = await(from vdr in RepositoryContext.Checklists
                            where vdr.IsDeleted == false && vdr.VisitingId == visitingId
                            orderby vdr.CreatedAt descending
                            select new ChecklistViewModel
                            {
                                ChecklistId = vdr.ChecklistId,
                                ChecklistPICs = RepositoryContext.ChecklistPICs.Where(c => c.ChecklistId == vdr.ChecklistId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                ChecklistEvidences = vdr.ChecklistEvidences.ToList(),
                                Network = vdr.Network,
                                ChecklistFotoStandardId = vdr.AttachmentId,
                                ChecklistItem = vdr.ChecklistItem,
                                ChecklistIdentification = vdr.ChecklistIdentification,
                                ChecklistActualCondition = vdr.ChecklistActualCondition,
                                ChecklistActualDetail = vdr.ChecklistActualDetail,
                                ChecklistFix = vdr.ChecklistFix,
                                ChecklistStatus = vdr.ChecklistStatus,
                                ChecklistDeadline = vdr.ChecklistDeadline.HasValue ? vdr.ChecklistDeadline.Value : DateTime.MinValue,
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
                var detailImageList = new List<ChecklistImageUrl>();

                var attachmentUrlFotoStandard = await GetAttachmentUrl(lst[i].ChecklistFotoStandardId);
                lst[i].ChecklistFotoStandardUrl = attachmentUrlFotoStandard;

                for (int j = 0; j < lst[i].ChecklistEvidences.Count(); j++)
                {
                    var imageList = new ChecklistImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(lst[i].ChecklistEvidences[j].AttachmentId);

                    imageList.ChecklistId = lst[i].ChecklistEvidences[j].ChecklistId;
                    imageList.ChecklistEvidenceId = lst[i].ChecklistEvidences[j].ChecklistEvidenceId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                lst[i].ChecklistEvidenceDetail = detailImageList;
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

            return new ChecklistIdentificationModel
            {
                Checklists = lst
            };
        }

        public async Task<string> GetCreatedByName(Guid employeeId)
        {
            return await(from usr in RepositoryContext.Users
                         where usr.EmployeeId == employeeId
                         select usr.DisplayName).FirstOrDefaultAsync();
        }

        public void UpdateBulkChecklist(List<Checklists> checklists)
        {
            var tempVdr = RepositoryContext.Checklists
                .Where(te => checklists[0].VisitingId.Equals(te.VisitingId))
                .ToList();

                var index = 0;
                foreach (var item in tempVdr)
                {
                    tempVdr[index].ChecklistStatus = "on progress";
                    Update(tempVdr[index]);
                    index++;
                }
        }

        public void UpdateChecklist(Checklists checklist)
        {
            Update(checklist);
        }
    }
}
