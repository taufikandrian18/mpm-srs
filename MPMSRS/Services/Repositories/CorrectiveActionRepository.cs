using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Repositories
{
    public class CorrectiveActionRepository : UserBase<CorrectiveActions>, ICorrectiveActionProfileRepository
    {
        public CorrectiveActionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCorrectiveActions(CorrectiveActions correctiveAction)
        {
            Create(correctiveAction);
        }

        public async Task<CorrectiveActions> GetCorrectiveActionById(Guid correctiveActionId)
        {
            return await FindByCondition(ca => ca.CorrectiveActionId.Equals(correctiveActionId))
            .Where(ca => ca.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<CorrectiveActionDetailDto> GetCorrectiveActionDetailById(Guid correctiveActionId)
        {
            var detailReportList = await (from ca in RepositoryContext.CorrectiveActions
                          join vdr in RepositoryContext.VisitingDetailReports on ca.VisitingDetailReportId equals vdr.VisitingDetailReportId
                          join caAtt in RepositoryContext.CorrectiveActionAttachments on ca.CorrectiveActionId equals caAtt.CorrectiveActionId into grpAtt
                          from caAtt in grpAtt.DefaultIfEmpty()
                          join v in RepositoryContext.Visitings on vdr.VisitingId equals v.VisitingId
                          where ca.CorrectiveActionId == correctiveActionId && ca.IsDeleted == false
                          select new CorrectiveActionDetailDto
                          {
                              CorrectiveActionId = ca.CorrectiveActionId,
                              VisitingId = vdr.VisitingId,
                              VisitingTypeId = v.VisitingTypeId,
                              VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == v.VisitingTypeId).Select(c => c.VisitingTypeName).FirstOrDefault(),
                              Network = vdr.Network,
                              VisitingDetailReportId = ca.VisitingDetailReportId,
                              VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                              CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                              VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                              VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                              AttachmentDetail = RepositoryContext.CorrectiveActionAttachments.Where(c => c.CorrectiveActionId == ca.CorrectiveActionId).Select(c => new CorrectiveActionAttachmentDetail { CorrectiveActionAttachmentId = c.CorrectiveActionAttachmentId, AttachmentId = c.AttachmentId, ImageUrl = RepositoryContext.Attachments.Where(x => x.AttachmentId == c.AttachmentId).Select(x => x.AttachmentUrl).FirstOrDefault() }).ToList(),
                              CorrectiveActionName = ca.CorrectiveActionName,
                              ProgressBy = ca.ProgressBy,
                              ValidateBy = ca.ValidateBy,
                              CorrectiveActionDetail = ca.CorrectiveActionDetail,
                              CreatedAt = ca.CreatedAt,
                              CreatedBy = ca.CreatedBy,
                              UpdatedAt = ca.UpdatedAt,
                              UpdatedBy = ca.UpdatedBy
                          })
                          .AsNoTracking()
                          .FirstOrDefaultAsync();

            if(!string.IsNullOrEmpty(detailReportList.ProgressBy))
            {
                detailReportList.ProgressBy = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(detailReportList.ProgressBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
            }

            if (!string.IsNullOrEmpty(detailReportList.ValidateBy))
            {
                detailReportList.ValidateBy = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(detailReportList.ValidateBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
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

        public async Task<string> GetCreatedByName(Guid employeeId)
        {
            return await (from usr in RepositoryContext.Users
                          where usr.EmployeeId == employeeId
                          select usr.DisplayName).FirstOrDefaultAsync();
        }

        public async Task<CorrectiveActionNetworkDtoViewModel> GetListCAByNetwork(Guid networkId, Guid divisionId, int pageSize, int pageIndex, string status, string sortBy, string sortByDeadline)
        {
            var lstContext = (from ca in RepositoryContext.CorrectiveActions
                              join vdr in RepositoryContext.VisitingDetailReports on ca.VisitingDetailReportId equals vdr.VisitingDetailReportId
                              where vdr.IsDeleted == false && ca.IsDeleted == false && vdr.NetworkId == networkId
                              select new CorrectiveActionDtoVMList
                              {
                                  VisitingId = vdr.VisitingId,
                                  VisitingTypeId = RepositoryContext.Visitings.Where(c => c.VisitingId == vdr.VisitingId).Select(c => c.VisitingTypeId).FirstOrDefault(),
                                  VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.VisitingTypeId).FirstOrDefault()).Select(c => c.VisitingTypeName).FirstOrDefault(),
                                  NetworkId = vdr.NetworkId,
                                  DivisionId = vdr.DivisionId,
                                  DivisionName = RepositoryContext.Divisions.Where(c => c.DivisionId == vdr.DivisionId).Select(c => c.DivisionName).FirstOrDefault(),
                                  AhmCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.AhmCode).FirstOrDefault(),
                                  MDCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.MDCode).FirstOrDefault(),
                                  DealerName = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.DealerName).FirstOrDefault(),
                                  NetworkAddress = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.Address).FirstOrDefault(),
                                  NetworkCity = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.City).FirstOrDefault(),
                                  CorrectiveActionId = ca.CorrectiveActionId,
                                  VisitingDetailReportId = ca.VisitingDetailReportId,
                                  VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                  CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                  VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                  VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                  CorrectiveActionName = ca.CorrectiveActionName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  CorrectiveActionDetail = ca.CorrectiveActionDetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingDetailReportStatus.ToLower() == status.ToLower());
            }

            if (divisionId != Guid.Empty)
            {
                lstContext = lstContext.Where(Q => Q.DivisionId.Equals(divisionId));
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
                    lstContext = lstContext.OrderBy(Q => Q.CreatedAt);
                    break;
            }

            switch (sortByDeadline)
            {
                case "old":
                    lstContext = lstContext.OrderByDescending(Q => Q.VisitingDetailReportDeadline);
                    break;
                case "new":
                    lstContext = lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline);
                    break;
                default:
                    lstContext = lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline);
                    break;
            }

            PagesViewModel pages = new PagesViewModel();
            pages.Total = lstContext.Count();
            pages.PerPage = pageSize;
            pages.Page = pageIndex;
            pages.LstPage = pages.Total / pageSize;

            var data = await lstContext.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var index = 0;
            foreach (var datum in data)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                data[index].CreatedByName = createdName;
                data[index].UpdatedByName = updatedName;
                index++;
            }

            return new CorrectiveActionNetworkDtoViewModel
            {
                Data = data,
                Pages = pages
            };
        }

        public async Task<IEnumerable<CorrectiveActionDtoVMList>> GetListCAByPICTagged(Guid employeeId, Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            var picList = await (from emp in RepositoryContext.VisitingDetailReportPICs
                                 where emp.EmployeeId == employeeId
                                 select emp.VisitingDetailReportId).Distinct().ToListAsync();

            var lstContext = (from ca in RepositoryContext.CorrectiveActions
                              join vdr in RepositoryContext.VisitingDetailReports on ca.VisitingDetailReportId equals vdr.VisitingDetailReportId
                              where vdr.CreatedBy == employeeId.ToString().Trim() && vdr.IsDeleted == false && ca.IsDeleted == false && vdr.VisitingDetailReportStatus.Trim() != "draft" && picList.Contains(vdr.VisitingDetailReportId)
                              select new CorrectiveActionDtoVMList
                              {
                                  VisitingId = vdr.VisitingId,
                                  VisitingTypeId = RepositoryContext.Visitings.Where(c => c.VisitingId == vdr.VisitingId).Select(c => c.VisitingTypeId).FirstOrDefault(),
                                  VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.VisitingTypeId).FirstOrDefault()).Select(c => c.VisitingTypeName).FirstOrDefault(),
                                  NetworkId = vdr.NetworkId,
                                  DivisionId = vdr.DivisionId,
                                  DivisionName = RepositoryContext.Divisions.Where(c => c.DivisionId == vdr.DivisionId).Select(c => c.DivisionName).FirstOrDefault(),
                                  AhmCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.AhmCode).FirstOrDefault(),
                                  MDCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.MDCode).FirstOrDefault(),
                                  DealerName = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.DealerName).FirstOrDefault(),
                                  NetworkAddress = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.Address).FirstOrDefault(),
                                  NetworkCity = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.City).FirstOrDefault(),
                                  CorrectiveActionId = ca.CorrectiveActionId,
                                  VisitingDetailReportId = ca.VisitingDetailReportId,
                                  VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                  CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                  VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                  VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                  CorrectiveActionName = ca.CorrectiveActionName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  CorrectiveActionDetail = ca.CorrectiveActionDetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.VisitingDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            if (divisionId != Guid.Empty)
            {
                lstContext = lstContext.Where(Q => Q.DivisionId.Equals(divisionId));
            }

            var data = await lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var index = 0;
            foreach (var datum in data)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                data[index].CreatedByName = createdName;
                data[index].UpdatedByName = updatedName;
                index++;
            }

            return data;
        }

        public async Task<IEnumerable<CorrectiveActionDtoVMList>> GetListCAByMainDealer(Guid employeeId, Guid divisionId, int pageSize, int pageIndex, string status , string startDate, string endDate, string area, string query)
        {
            var lstContext = (from ca in RepositoryContext.CorrectiveActions
                             join vdr in RepositoryContext.VisitingDetailReports on ca.VisitingDetailReportId equals vdr.VisitingDetailReportId
                             where vdr.CreatedBy == employeeId.ToString().Trim() && vdr.IsDeleted == false && ca.IsDeleted == false && vdr.VisitingDetailReportStatus.Trim() != "draft"
                             select new CorrectiveActionDtoVMList
                             {
                                 VisitingId = vdr.VisitingId,
                                 VisitingTypeId = RepositoryContext.Visitings.Where(c => c.VisitingId == vdr.VisitingId).Select(c => c.VisitingTypeId).FirstOrDefault(),
                                 VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.VisitingTypeId).FirstOrDefault()).Select(c => c.VisitingTypeName).FirstOrDefault(),
                                 NetworkId = vdr.NetworkId,
                                 DivisionId = vdr.DivisionId,
                                 DivisionName = RepositoryContext.Divisions.Where(c => c.DivisionId == vdr.DivisionId).Select(c => c.DivisionName).FirstOrDefault(),
                                 AhmCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.AhmCode).FirstOrDefault(),
                                 MDCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.MDCode).FirstOrDefault(),
                                 DealerName = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.DealerName).FirstOrDefault(),
                                 NetworkAddress = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.Address).FirstOrDefault(),
                                 NetworkCity = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.City).FirstOrDefault(),
                                 CorrectiveActionId = ca.CorrectiveActionId,
                                 VisitingDetailReportId = ca.VisitingDetailReportId,
                                 VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                 CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                 VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                 VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                 CorrectiveActionName = ca.CorrectiveActionName,
                                 ProgressBy = ca.ProgressBy,
                                 ValidateBy = ca.ValidateBy,
                                 CorrectiveActionDetail = ca.CorrectiveActionDetail,
                                 CreatedAt = ca.CreatedAt,
                                 CreatedBy = ca.CreatedBy,
                                 UpdatedAt = ca.UpdatedAt,
                                 UpdatedBy = ca.UpdatedBy
                             }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.VisitingDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            if (divisionId != Guid.Empty)
            {
                lstContext = lstContext.Where(Q => Q.DivisionId.Equals(divisionId));
            }

            var data = await lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var index = 0;
            foreach (var datum in data)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                data[index].CreatedByName = createdName;
                data[index].UpdatedByName = updatedName;
                index++;
            }

            return data;
        }

        public async Task<IEnumerable<CorrectiveActionDtoVMList>> GetListCAByMDCode(string mdCode, Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            var lstContext = (from ca in RepositoryContext.CorrectiveActions
                            join vdr in RepositoryContext.VisitingDetailReports on ca.VisitingDetailReportId equals vdr.VisitingDetailReportId
                            join nt in RepositoryContext.Networks on vdr.NetworkId equals nt.NetworkId
                            where nt.MDCode == mdCode && vdr.IsDeleted == false && ca.IsDeleted == false && vdr.VisitingDetailReportStatus.Trim() != "draft"
                            select new CorrectiveActionDtoVMList
                            {
                                VisitingId = vdr.VisitingId,
                                VisitingTypeId = RepositoryContext.Visitings.Where(c => c.VisitingId == vdr.VisitingId).Select(c => c.VisitingTypeId).FirstOrDefault(),
                                VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.VisitingTypeId).FirstOrDefault()).Select(c => c.VisitingTypeName).FirstOrDefault(),
                                NetworkId = vdr.NetworkId,
                                DivisionId = vdr.DivisionId,
                                DivisionName = RepositoryContext.Divisions.Where(c => c.DivisionId == vdr.DivisionId).Select(c => c.DivisionName).FirstOrDefault(),
                                AhmCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.AhmCode).FirstOrDefault(),
                                MDCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.MDCode).FirstOrDefault(),
                                DealerName = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.DealerName).FirstOrDefault(),
                                NetworkAddress = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.Address).FirstOrDefault(),
                                NetworkCity = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.City).FirstOrDefault(),
                                CorrectiveActionId = ca.CorrectiveActionId,
                                VisitingDetailReportId = ca.VisitingDetailReportId,
                                VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                CorrectiveActionName = ca.CorrectiveActionName,
                                ProgressBy = ca.ProgressBy,
                                ValidateBy = ca.ValidateBy,
                                CorrectiveActionDetail = ca.CorrectiveActionDetail,
                                CreatedAt = ca.CreatedAt,
                                CreatedBy = ca.CreatedBy,
                                UpdatedAt = ca.UpdatedAt,
                                UpdatedBy = ca.UpdatedBy
                            }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.VisitingDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }

            if (divisionId != Guid.Empty)
            {
                lstContext = lstContext.Where(Q => Q.DivisionId.Equals(divisionId));
            }

            var data = await lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var index = 0;
            foreach (var datum in data)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                data[index].CreatedByName = createdName;
                data[index].UpdatedByName = updatedName;
                index++;
            }

            return data;
        }

        public async Task<int> GetCountCAOnProgressCreatedBy(Guid employeeId)
        {
            var visitingDetailList = await (from emp in RepositoryContext.VisitingDetailReports
                                      where emp.CreatedBy == employeeId.ToString() && emp.IsDeleted == false && emp.VisitingDetailReportStatus.Trim() == "on progress"
                                      select emp.VisitingDetailReportId).Distinct().ToListAsync();

            var lstContext = await (from ca in RepositoryContext.CorrectiveActions
                                    where ca.IsDeleted == false && ca.CreatedBy == employeeId.ToString() && visitingDetailList.Contains(ca.VisitingDetailReportId)
                                    select ca).ToListAsync();

            return lstContext.Count();
        }

        public async Task<int> GetPercentageCA()
        {
            var vdrApproved = await (from vdr in RepositoryContext.VisitingDetailReports
                                     where vdr.IsDeleted == false && vdr.VisitingDetailReportStatus.Trim() == "approved"
                                     select vdr.VisitingDetailReportId).Distinct().ToListAsync();

            var caApproved = await (from ca in RepositoryContext.CorrectiveActions
                                    where ca.IsDeleted == false && vdrApproved.Contains(ca.VisitingDetailReportId)
                                    select ca).ToListAsync();

            var caTotal = await (from ca in RepositoryContext.CorrectiveActions
                                 where ca.IsDeleted == false
                                 select ca).ToListAsync();

            var percentage = (int)Math.Round((double)(100 * caApproved.Count()) / caTotal.Count());

            return percentage;
        }

        public void UpdateCorrectiveActions(CorrectiveActions correctiveAction)
        {
            Update(correctiveAction);
        }

        public async Task<IEnumerable<CorrectiveActionDtoVMList>> GetListAllUserCA(Guid divisionId ,int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query, string sortByDeadline)
        {
            var lstContext = (from ca in RepositoryContext.CorrectiveActions
                              join vdr in RepositoryContext.VisitingDetailReports on ca.VisitingDetailReportId equals vdr.VisitingDetailReportId
                              where vdr.IsDeleted == false && ca.IsDeleted == false && vdr.VisitingDetailReportStatus.Trim() != "draft"
                              select new CorrectiveActionDtoVMList
                              {
                                  VisitingId = vdr.VisitingId,
                                  VisitingTypeId = RepositoryContext.Visitings.Where(c => c.VisitingId == vdr.VisitingId).Select(c => c.VisitingTypeId).FirstOrDefault(),
                                  VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == RepositoryContext.Visitings.Where(x => x.VisitingId == vdr.VisitingId).Select(x => x.VisitingTypeId).FirstOrDefault()).Select(c => c.VisitingTypeName).FirstOrDefault(),
                                  NetworkId = vdr.NetworkId,
                                  DivisionId = vdr.DivisionId,
                                  DivisionName = RepositoryContext.Divisions.Where(c => c.DivisionId == vdr.DivisionId).Select(c => c.DivisionName).FirstOrDefault(),
                                  AhmCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.AhmCode).FirstOrDefault(),
                                  MDCode = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.MDCode).FirstOrDefault(),
                                  DealerName = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.DealerName).FirstOrDefault(),
                                  NetworkAddress = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.Address).FirstOrDefault(),
                                  NetworkCity = RepositoryContext.Networks.Where(c => c.NetworkId == vdr.NetworkId).Select(c => c.City).FirstOrDefault(),
                                  CorrectiveActionId = ca.CorrectiveActionId,
                                  VisitingDetailReportId = ca.VisitingDetailReportId,
                                  VisitingDetailReportProblemIdentification = vdr.VisitingDetailReportProblemIdentification,
                                  CorrectiveActionProblemIdentification = vdr.CorrectiveActionProblemIdentification,
                                  VisitingDetailReportStatus = vdr.VisitingDetailReportStatus,
                                  VisitingDetailReportDeadline = vdr.VisitingDetailReportDeadline.HasValue ? vdr.VisitingDetailReportDeadline.Value : DateTime.MinValue,
                                  CorrectiveActionName = ca.CorrectiveActionName,
                                  ProgressBy = ca.ProgressBy,
                                  ValidateBy = ca.ValidateBy,
                                  CorrectiveActionDetail = ca.CorrectiveActionDetail,
                                  CreatedAt = ca.CreatedAt,
                                  CreatedBy = ca.CreatedBy,
                                  UpdatedAt = ca.UpdatedAt,
                                  UpdatedBy = ca.UpdatedBy
                              }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingDetailReportStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                lstContext = lstContext.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                lstContext = lstContext.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    lstContext = lstContext.Where(Q => Q.VisitingDetailReportDeadline.Date >= DateTime.Parse(startDate).Date && Q.VisitingDetailReportDeadline <= DateTime.Parse(endDate).Date);
                }
            }
            if (divisionId != Guid.Empty)
            {
                lstContext = lstContext.Where(Q => Q.DivisionId.Equals(divisionId));
            }

            switch (sortByDeadline)
            {
                case "old":
                    lstContext = lstContext.OrderByDescending(Q => Q.VisitingDetailReportDeadline);
                    break;
                case "new":
                    lstContext = lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline);
                    break;
                default:
                    lstContext = lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline);
                    break;
            }

            var data = await lstContext.OrderBy(Q => Q.VisitingDetailReportDeadline).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var index = 0;
            foreach (var datum in data)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
                var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

                data[index].CreatedByName = createdName;
                data[index].UpdatedByName = updatedName;
                index++;
            }

            return data;
        }
    }
}
