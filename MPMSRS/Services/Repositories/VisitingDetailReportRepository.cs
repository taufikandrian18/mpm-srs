using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Repositories.Command;

namespace MPMSRS.Services.Repositories
{
    public class VisitingDetailReportRepository : UserBase<VisitingDetailReports>, IVisitingDetailReportProfileRepository
    {
        public VisitingDetailReportRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingDetailReport(VisitingDetailReports visitingDetailReport)
        {
            Create(visitingDetailReport);
        }

        public void DeleteVisitingDetailReport(VisitingDetailReports visitingDetailReport)
        {
            Update(visitingDetailReport);
        }

        public async Task<VisitingDetailDtoLoginViewModel> GetAllVisitingDetailReport(Guid visitingId, int pageSize, int pageIndex)
        {
            var detailReportList = await (from vdr in RepositoryContext.VisitingDetailReports
                                          where vdr.IsDeleted == false && vdr.VisitingId == visitingId && vdr.VisitingDetailReportStatus.ToLower().Trim() != "checklist"
                                          orderby vdr.CreatedAt descending
                                          select new VisitingDetailReportViewModel
                                          {
                                              VisitingDetailReportId = vdr.VisitingDetailReportId,
                                              VisitingDetailReportPC = vdr.VisitingDetailReportProblemCategories.ToList(),
                                              VisitingDetailReportPIC = RepositoryContext.VisitingDetailReportPICs.Where(c => c.VisitingDetailReportId == vdr.VisitingDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName } ).ToList(),
                                              VisitingDetailReportAttachment = vdr.VisitingDetailReportAttachments.ToList(),
                                              Division = vdr.Division,
                                              Network = vdr.Network,
                                              VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                              VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                              CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                              VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                              VisitingDetailReportFlagging = vdr.VisitingDetailReportFlagging,
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
                for (int j = 0; j < detailReportList[i].VisitingDetailReportAttachment.Count(); j++)
                {
                    var imageList = new ImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(detailReportList[i].VisitingDetailReportAttachment[j].AttachmentId);

                    imageList.VisitingDetailReportId = detailReportList[i].VisitingDetailReportAttachment[j].VisitingDetailReportId;
                    imageList.VisitingDetailReportAttachmentId = detailReportList[i].VisitingDetailReportAttachment[j].VisitingDetailReportAttachmentId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                detailReportList[i].VisitingDetailAttachmentDetail = detailImageList;
            }

            var index = 0;
            foreach (var datum in detailReportList)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));

                detailReportList[index].CreatedByName = createdName;
                index++;
            }

            var mstLst = await (from v in RepositoryContext.Visitings
                          join vp in RepositoryContext.VisitingPeoples on v.VisitingId equals vp.VisitingId into vvp
                          from vp in vvp.DefaultIfEmpty()
                          join vnm in RepositoryContext.VisitingNoteMappings on v.VisitingId equals vnm.VisitingId into vvnm
                          from vnm in vvnm.DefaultIfEmpty()
                          where v.IsDeleted == false && v.VisitingId == visitingId
                          select new VisitingDetailDtoLoginViewModel
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
                              VisitingCommentByManager = v.VisitingCommentByManager,
                              VisitingStatus = v.VisitingStatus,
                              ApprovedByManager = v.ApprovedByManager,
                              ApprovedByGM = v.ApprovedByGM,
                              IsOnline = v.IsOnline,
                              StartDate = v.StartDate,
                              EndDate = v.EndDate,
                              VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == v.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                              VisitingNoteMapping = v.VisitingNoteMappings.ToList(),
                              VisitingDetailReport = detailReportList,
                              CreatedAt = v.CreatedAt,
                              CreatedBy = v.CreatedBy,
                              UpdatedAt = v.UpdatedAt,
                              UpdatedBy = v.UpdatedBy,
                          })
                          .FirstOrDefaultAsync();

            var createdNameMst = await GetCreatedByName(Guid.Parse(mstLst.CreatedBy));

            mstLst.CreatedByName = createdNameMst;

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

        public async Task<VisitingDetailReports> GetVisitingDetailReportById(Guid visitingDetailReportId)
        {
            return await FindByCondition(vt => vt.VisitingDetailReportId.Equals(visitingDetailReportId))
                .Where(vt => vt.IsDeleted == false && vt.VisitingDetailReportStatus.ToLower().Trim() != "checklist")
                .FirstOrDefaultAsync();
        }

        public async Task<VisitingDetailReportViewModel> GetVisitingDetailReportByVisitingDetailReportId(Guid visitingDetailReportId)
        {
            var lst = await(from vdr in RepositoryContext.VisitingDetailReports
                            where vdr.VisitingDetailReportId == visitingDetailReportId && vdr.IsDeleted == false && vdr.VisitingDetailReportStatus.ToLower().Trim() != "checklist"
                            orderby vdr.CreatedAt descending
                            select new VisitingDetailReportViewModel
                            {
                                VisitingDetailReportId = vdr.VisitingDetailReportId,
                                VisitingDetailReportPC = vdr.VisitingDetailReportProblemCategories.ToList(),
                                VisitingDetailReportPIC = RepositoryContext.VisitingDetailReportPICs.Where(c => c.VisitingDetailReportId == vdr.VisitingDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName } ).ToList(),
                                VisitingDetailReportAttachment = vdr.VisitingDetailReportAttachments.ToList(),
                                Division = vdr.Division,
                                Network = vdr.Network,
                                VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                VisitingDetailReportFlagging = vdr.VisitingDetailReportFlagging,
                                CreatedAt = vdr.CreatedAt,
                                CreatedBy = vdr.CreatedBy,
                                UpdatedAt = vdr.UpdatedAt,
                                UpdatedBy = vdr.UpdatedBy
                            })
                            .FirstOrDefaultAsync();


            var detailImageList = new List<ImageUrl>();
            if (lst != null)
            {
                for (int j = 0; j < lst.VisitingDetailReportAttachment.Count(); j++)
                {
                    var imageList = new ImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(lst.VisitingDetailReportAttachment[j].AttachmentId);

                    imageList.VisitingDetailReportId = lst.VisitingDetailReportAttachment[j].VisitingDetailReportId;
                    imageList.VisitingDetailReportAttachmentId = lst.VisitingDetailReportAttachment[j].VisitingDetailReportAttachmentId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                lst.VisitingDetailAttachmentDetail = detailImageList;


                var createdName = await GetCreatedByName(Guid.Parse(lst.CreatedBy));

                lst.CreatedByName = createdName;
            }

            return lst;
        }

        public async Task<IEnumerable<VisitingDetailReports>> GetVisitingDetailReportByVisitingIdWithoutPage(Guid visitingId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingId == visitingId && ow.VisitingDetailReportStatus.ToLower().Trim() != "checklist")
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }

        public async Task<VisitingDetailReportIdentificationModel> GetVisitingDetailReportIdentification(Guid visitingId, int pageSize, int pageIndex)
        {
            var lst =    await (from vdr in RepositoryContext.VisitingDetailReports
                                where vdr.IsDeleted == false && vdr.VisitingId == visitingId && vdr.VisitingDetailReportStatus.ToLower().Trim() != "checklist"
                                orderby vdr.CreatedAt descending
                                select new VisitingDetailReportViewModel {
                                    VisitingDetailReportId = vdr.VisitingDetailReportId,
                                    VisitingDetailReportPC = vdr.VisitingDetailReportProblemCategories.ToList(),
                                    VisitingDetailReportPIC = RepositoryContext.VisitingDetailReportPICs.Where(c => c.VisitingDetailReportId == vdr.VisitingDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName } ).ToList(),
                                    VisitingDetailReportAttachment = vdr.VisitingDetailReportAttachments.ToList(),
                                    Division = vdr.Division,
                                    Network = vdr.Network,
                                    VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                    VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                    CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                    VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                    VisitingDetailReportFlagging = vdr.VisitingDetailReportFlagging,
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
                for (int j = 0; j < lst[i].VisitingDetailReportAttachment.Count(); j++)
                {
                    var imageList = new ImageUrl();
                    var attachmentUrl = await GetAttachmentUrl(lst[i].VisitingDetailReportAttachment[j].AttachmentId);

                    imageList.VisitingDetailReportId = lst[i].VisitingDetailReportAttachment[j].VisitingDetailReportId;
                    imageList.VisitingDetailReportAttachmentId = lst[i].VisitingDetailReportAttachment[j].VisitingDetailReportAttachmentId;
                    imageList.imageUrl = attachmentUrl;

                    detailImageList.Add(imageList);
                }

                lst[i].VisitingDetailAttachmentDetail = detailImageList;
            }

            var index = 0;
            foreach (var datum in lst)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));

                lst[index].CreatedByName = createdName;
                index++;
            }

            return new VisitingDetailReportIdentificationModel
            {
                VisitingDetailReport = lst
            };
        }

        public void UpdateBulkVisitingDetailReport(List<VisitingDetailReports> visitingDetailReport)
        {
            var tempVdr = RepositoryContext.VisitingDetailReports
                .Where(te => visitingDetailReport[0].VisitingId.Equals(te.VisitingId) && te.VisitingDetailReportStatus.ToLower().Trim() != "checklist")
                .ToList();

            var index = 0;
            foreach(var item in tempVdr)
            {
                tempVdr[index].VisitingDetailReportStatus = "on progress";
                Update(tempVdr[index]);
                index++;
            }
        }

        public void UpdateVisitingDetailReport(VisitingDetailReports visitingDetailReport)
        {
            Update(visitingDetailReport);
        }

        public async Task<IEnumerable<VisitingDetailReportViewExcelModel>> GetAllVisitingDetailReportExportExcel(string status, string area, string query)
        {
            try
            {
                var detailReportList = (from vdr in RepositoryContext.VisitingDetailReports
                                        join vpc in RepositoryContext.VisitingDetailReportProblemCategories on vdr.VisitingDetailReportId equals vpc.VisitingDetailReportId into grppc
                                        from vpc in grppc.DefaultIfEmpty()
                                        where vdr.IsDeleted == false && vpc.IsDeleted == false && vdr.VisitingDetailReportStatus.ToLower().Trim() != "checklist" && vpc.VisitingDetailReportPCId == (from vdri in RepositoryContext.VisitingDetailReportProblemCategories where vdri.VisitingDetailReportId == vdr.VisitingDetailReportId select vdri.VisitingDetailReportPCId).Max()
                                        orderby vdr.CreatedAt descending
                                        select new VisitingDetailReportViewExcelModel
                                        {
                                            VisitingDetailReportId = vdr.VisitingDetailReportId,
                                            VisitingDetailReportPC = vdr.VisitingDetailReportProblemCategories.ToList(),
                                            VisitingDetailReportPIC = RepositoryContext.VisitingDetailReportPICs.Where(c => c.VisitingDetailReportId == vdr.VisitingDetailReportId).Include(c => c.User).Where(c => c.User.EmployeeId == c.EmployeeId).Select(c => new VisitingDetailReportPICVMViewModel { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                                            VisitingDetailReportAttachment = vdr.VisitingDetailReportAttachments.ToList(),
                                            Division = vdr.Division,
                                            NetworkId = vdr.NetworkId,
                                            AhmCode = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.AhmCode).FirstOrDefault(),
                                            MDCode = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.MDCode).FirstOrDefault(),
                                            DealerName = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.DealerName).FirstOrDefault(),
                                            NetworkAddress = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.Address).FirstOrDefault(),
                                            NetworkCity = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.City).FirstOrDefault(),
                                            NetworkLatitude = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.NetworkLatitude).FirstOrDefault(),
                                            NetworkLongitude = RepositoryContext.Networks.Where(x => x.NetworkId == vdr.NetworkId).Select(x => x.NetworkLongitude).FirstOrDefault(),
                                            VisitingTypeName = RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.VisitingType.VisitingTypeName).FirstOrDefault(),
                                            VisitingStartDate = RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.StartDate).FirstOrDefault(),
                                            VisitingEndDate = RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.EndDate).FirstOrDefault(),
                                            VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                            VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                            CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                            VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                            VisitingDetailReportFlagging = vdr.VisitingDetailReportFlagging,
                                            CreatedAt = vdr.CreatedAt,
                                            CreatedBy = vdr.CreatedBy,
                                            UpdatedAt = vdr.UpdatedAt,
                                            UpdatedBy = vdr.UpdatedBy
                                        })
                                        .AsNoTracking()
                                        .AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    detailReportList = detailReportList.Where(Q => Q.VisitingDetailReportStatus.ToLower() == status.ToLower());
                }

                if (!string.IsNullOrEmpty(area))
                {
                    detailReportList = detailReportList.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
                }

                if (!string.IsNullOrEmpty(query))
                {
                    detailReportList = detailReportList.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
                }

                var dataResult = await detailReportList.OrderByDescending(Q => Q.CreatedAt).ToListAsync();

                for (int i = 0; i < dataResult.Count(); i++)
                {
                    var detailImageList = new List<ImageUrl>();
                    for (int j = 0; j < dataResult[i].VisitingDetailReportAttachment.Count(); j++)
                    {
                        var imageList = new ImageUrl();
                        var attachmentUrl = await GetAttachmentUrl(dataResult[i].VisitingDetailReportAttachment[j].AttachmentId);

                        imageList.VisitingDetailReportId = dataResult[i].VisitingDetailReportAttachment[j].VisitingDetailReportId;
                        imageList.VisitingDetailReportAttachmentId = dataResult[i].VisitingDetailReportAttachment[j].VisitingDetailReportAttachmentId;
                        imageList.imageUrl = attachmentUrl;

                        detailImageList.Add(imageList);
                    }

                    dataResult[i].VisitingDetailAttachmentDetail = detailImageList;
                }

                int index = 0;
                foreach (var datum in dataResult)
                {
                    if (!string.IsNullOrEmpty(datum.CreatedBy))
                    {
                        if (datum.CreatedBy.Trim() == "SYSTEM")
                        {
                            dataResult[index].CreatedByName = "SYSTEM";
                        }
                        else
                        {
                            dataResult[index].CreatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(datum.CreatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
                        }
                    }

                    if (!string.IsNullOrEmpty(datum.UpdatedBy))
                    {
                        if (datum.UpdatedBy.Trim() == "SYSTEM")
                        {
                            dataResult[index].UpdatedByName = "SYSTEM";
                        }
                        else
                        {
                            dataResult[index].UpdatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(datum.UpdatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
                        }
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
    }
}
