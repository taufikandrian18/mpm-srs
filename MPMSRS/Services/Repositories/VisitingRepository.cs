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
    public class VisitingRepository : UserBase<Visitings>, IVisitingProfileRepository
    {
        public VisitingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisiting(Visitings visiting)
        {
            Create(visiting);
        }

        public void DeleteVisiting(Visitings visiting)
        {
            Update(visiting);
        }

        //kunjungan yang di tag
        public async Task<IEnumerable<VisitingDtoLoginViewModel>> GetAllVisitingByPeopleLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {

            var visitingList = await (from emp in RepositoryContext.VisitingPeoples
                                      where emp.EmployeeId == EmployeeId
                                      select emp.VisitingId).ToListAsync();

            var lstContext = (from vs in RepositoryContext.Visitings
                              join nt in RepositoryContext.Networks on vs.NetworkId equals nt.NetworkId
                              join vt in RepositoryContext.VisitingTypes on vs.VisitingTypeId equals vt.VisitingTypeId
                              where vs.IsDeleted == false && vs.VisitingStatus.Contains(status) && visitingList.Contains(vs.VisitingId)
                              select new VisitingDtoLoginViewModel
                              {
                                  VisitingId = vs.VisitingId,
                                  VisitingTypeId = vs.VisitingTypeId,
                                  VisitingTypeName = vt.VisitingTypeName,
                                  NetworkId = vs.NetworkId,
                                  AhmCode = nt.AhmCode,
                                  MDCode = nt.MDCode,
                                  DealerName = nt.DealerName,
                                  NetworkAddress = nt.Address,
                                  NetworkCity = nt.City,
                                  VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.Username }).ToList(),
                                  VisitingComment = vs.VisitingComment,
                                  VisitingCommentByManager = vs.VisitingCommentByManager,
                                  VisitingStatus = vs.VisitingStatus,
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
                lstContext = lstContext.Where(Q => Q.VisitingStatus.ToLower() == status.ToLower());
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
                if(startDate.Trim() == endDate.Trim())
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

        //kunjungan saya
        public async Task<IEnumerable<VisitingReportDtoLoginViewModel>> GetAllVisitingByCreateLoginId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
           var lstContext = (from vs in RepositoryContext.Visitings
                            join nt in RepositoryContext.Networks on vs.NetworkId equals nt.NetworkId
                            join vt in RepositoryContext.VisitingTypes on vs.VisitingTypeId equals vt.VisitingTypeId
                            where vs.CreatedBy == EmployeeId.ToString() && vs.VisitingStatus.Contains(status) && vs.IsDeleted == false
                            select new VisitingReportDtoLoginViewModel
                            {
                                VisitingId = vs.VisitingId,
                                VisitingTypeId = vs.VisitingTypeId,
                                VisitingTypeName = vt.VisitingTypeName,
                                NetworkId = vs.NetworkId,
                                AhmCode = nt.AhmCode,
                                MDCode = nt.MDCode,
                                DealerName = nt.DealerName,
                                NetworkAddress = nt.Address,
                                NetworkCity = nt.City,
                                VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.Username }).ToList(),
                                VisitingComment = vs.VisitingComment,
                                VisitingCommentByManager = vs.VisitingCommentByManager,
                                VisitingStatus = vs.VisitingStatus,
                                ApprovedByGM = vs.ApprovedByGM,
                                ApprovedByManager = vs.ApprovedByManager,
                                IsOnline = vs.IsOnline,
                                StartDate = vs.StartDate,
                                EndDate = vs.EndDate,
                                CreatedAt = vs.CreatedAt,
                                CreatedBy = vs.CreatedBy,
                                UpdatedAt = vs.UpdatedAt,
                                UpdatedBy = vs.UpdatedBy,
                                JenisVisitingSingleOrJoin = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Count() > 0 ? "Join" : "Single"
                            })
                            .AsNoTracking()
                            .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingStatus.ToLower() == status.ToLower());
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

        public async Task<IEnumerable<VisitingDtoLoginViewAllModel>> GetAllVisitings(int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query, string createdName, string visitingPeopleName)
        {
            try
            {
                var lstContext = RepositoryContext.Visitings
                    .Where(ow => ow.IsDeleted == false)
                    .Join(RepositoryContext.Networks,
                    u => u.NetworkId,
                    n => n.NetworkId,
                    (u, n) => new { u, n })
                    .Select((x) => new VisitingDtoLoginViewAllModel
                    {
                        VisitingId = x.u.VisitingId,
                        VisitingTypeId = x.u.VisitingTypeId,
                        VisitingTypeName = RepositoryContext.VisitingTypes.Where(c => c.VisitingTypeId == x.u.VisitingTypeId).Select(c => c.VisitingTypeName).FirstOrDefault(),
                        NetworkId = x.u.NetworkId,
                        AhmCode = x.n.AhmCode,
                        MDCode = x.n.MDCode,
                        DealerName = x.n.DealerName,
                        NetworkAddress = x.n.Address,
                        NetworkCity = x.n.City,
                        VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == x.u.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.Username }).ToList(),
                        VisitingComment = x.u.VisitingComment,
                        VisitingCommentByManager = x.u.VisitingCommentByManager,
                        VisitingStatus = x.u.VisitingStatus,
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
                    lstContext = lstContext.Where(Q => Q.VisitingStatus.ToLower() == status.ToLower());
                }

                if (!string.IsNullOrEmpty(area))
                {
                    lstContext = lstContext.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
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

                if (!string.IsNullOrEmpty(query))
                {
                    lstContext = lstContext.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
                }

                var dataResult = await lstContext.OrderByDescending(Q => Q.CreatedAt).ToListAsync();

                int index = 0;
                foreach (var datum in dataResult)
                {
                    bool isValidCreatedBy = Guid.TryParse(datum.CreatedBy, out Guid create);
                    bool isValidUpdatedBy = Guid.TryParse(datum.UpdatedBy, out Guid update);
                    if (isValidCreatedBy && datum.CreatedBy != "string")
                    {
                        dataResult[index].CreatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(datum.CreatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
                    }
                    else
                    {
                        dataResult[index].CreatedByName = datum.CreatedBy;
                    }

                    if (isValidUpdatedBy && datum.UpdatedBy != "string")
                    {
                        dataResult[index].UpdatedByName = await RepositoryContext.Users.Where(c => c.EmployeeId.Equals(Guid.Parse(datum.UpdatedBy))).Select(c => c.DisplayName).FirstOrDefaultAsync();
                    }
                    else
                    {
                        dataResult[index].UpdatedByName = datum.UpdatedBy;
                    }

                    index++;
                }

                if (!string.IsNullOrEmpty(createdName))
                {
                    dataResult = dataResult.Where(x => x.CreatedByName != null).Where(x => x.CreatedByName.ToLower().Contains(createdName.ToLower())).ToList();
                }

                if(!string.IsNullOrEmpty(visitingPeopleName))
                {
                    dataResult = dataResult.Where(x => x.VisitingPeoples.Count() > 0).Where(x => x.VisitingPeoples.Select(c => c.EmployeeName.ToLower()).Contains(visitingPeopleName.ToLower())).ToList();
                }

                dataResult = dataResult.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                return dataResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetAllListCountVisitingOnGoingByDate(Guid employeeId, DateTime date)
        {
            var month = date.Month;
            var year = date.Year;

            var lstContext = (from v in RepositoryContext.Visitings
                              where v.CreatedBy == employeeId.ToString() && v.IsDeleted == false
                              select v).AsNoTracking().AsQueryable();

            var totalVisiting = await lstContext.Where(w => w.StartDate.Year == year && w.EndDate.Year == year && w.StartDate.Month == month && w.EndDate.Month == month).ToListAsync();

            return totalVisiting.Count();
        }

        public async Task<Visitings> GetVisitingById(Guid visitingId)
        {
            return await FindByCondition(vt => vt.VisitingId.Equals(visitingId))
                .Where(vt => vt.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<VisitingDtoViewDetailModel> GetVisitingDetailById(Guid visitingId)
        {
            var datum = await RepositoryContext.Visitings
                .Where(ow => ow.IsDeleted == false && ow.VisitingId.Equals(visitingId))
                .GroupJoin(RepositoryContext.Networks,
                u => u.NetworkId,
                n => n.NetworkId,
                (u, n) => new { u, n })
                .SelectMany(y => y.n.DefaultIfEmpty(), (vs, vp) => new { vs, vp })
                .GroupJoin(RepositoryContext.VisitingTypes,
                v => v.vs.u.VisitingTypeId,
                vt => vt.VisitingTypeId,
                (v, vt) => new { v, vt })
                .SelectMany(x => x.vt.DefaultIfEmpty(), (x, c) => new VisitingDtoViewDetailModel
                {
                    VisitingId = x.v.vs.u.VisitingId,
                    Network = x.v.vs.u.Network,
                    VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == x.v.vs.u.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.DisplayName }).ToList(),
                    VisitingTypeId = x.v.vs.u.VisitingTypeId,
                    VisitingTypeName = c.VisitingTypeName,
                    VisitingComment = x.v.vs.u.VisitingComment,
                    VisitingCommentByManager = x.v.vs.u.VisitingCommentByManager,
                    VisitingStatus = x.v.vs.u.VisitingStatus,
                    ApprovedByGM = x.v.vs.u.ApprovedByGM,
                    ApprovedByManager = x.v.vs.u.ApprovedByManager,
                    IsOnline = x.v.vs.u.IsOnline,
                    StartDate = x.v.vs.u.StartDate,
                    EndDate = x.v.vs.u.EndDate,
                    CreatedAt = x.v.vs.u.CreatedAt,
                    CreatedBy = x.v.vs.u.CreatedBy,
                    UpdatedAt = x.v.vs.u.UpdatedAt,
                    UpdatedBy = x.v.vs.u.UpdatedBy,
                })
                .FirstOrDefaultAsync();

            var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));
            var updatedName = await GetCreatedByName(Guid.Parse(datum.UpdatedBy));

            datum.CreatedByName = createdName;
            datum.UpdatedByName = updatedName;

            return datum;
        }

        public async Task<bool> GetVisitingIsOnline(Guid visitingId)
        {
            return await FindByCondition(vt => vt.VisitingId.Equals(visitingId))
                .Where(vt => vt.IsDeleted == false)
                .Select(vt => vt.IsOnline)
                .FirstOrDefaultAsync();
        }

        public void UpdateVisiting(Visitings visiting)
        {
            Update(visiting);
        }

        public async Task<string> GetCreatedByName(Guid employeeId)
        {
            return await (from usr in RepositoryContext.Users
                          where usr.EmployeeId == employeeId
                          select usr.DisplayName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VisitingDetailExportExcelDtoViewModel>> GetAllVisitingExportExcel(string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            var detailReportList = await(from vdr in RepositoryContext.VisitingDetailReports
                                         join vpc in RepositoryContext.VisitingDetailReportProblemCategories on vdr.VisitingDetailReportId equals vpc.VisitingDetailReportId into grppc
                                         from vpc in grppc.DefaultIfEmpty()
                                         where vdr.IsDeleted == false && vpc.IsDeleted == false && vpc.VisitingDetailReportPCId == (from vdri in RepositoryContext.VisitingDetailReportProblemCategories where vdri.VisitingDetailReportId == vdr.VisitingDetailReportId select vdri.VisitingDetailReportPCId).Max()
                                         orderby vdr.CreatedAt descending
                                         select new VisitingDetailReportExportExcelDto
                                         {
                                             VisitingDetailReportId = vdr.VisitingDetailReportId,
                                             VisitingId = vdr.VisitingId,
                                             VisitingDetailReportPC = vdr.VisitingDetailReportProblemCategories.Where(c => c.VisitingDetailReportId == vdr.VisitingDetailReportId).Select(c => c.VisitingDetailReportPCName).ToList(),
                                         })
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            var mstLst =  (from v in RepositoryContext.Visitings
                                join vp in RepositoryContext.VisitingPeoples on v.VisitingId equals vp.VisitingId into vvp
                                from vp in vvp.DefaultIfEmpty()
                                join vnm in RepositoryContext.VisitingNoteMappings on v.VisitingId equals vnm.VisitingId into vvnm
                                from vnm in vvnm.DefaultIfEmpty()
                                where v.IsDeleted == false
                                select new VisitingDetailExportExcelDtoViewModel
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
                                    VisitingComment = v.VisitingComment,
                                    VisitingCommentByManager = v.VisitingCommentByManager,
                                    VisitingStatus = v.VisitingStatus,
                                    ApprovedByManager = v.ApprovedByManager,
                                    ApprovedByGM = v.ApprovedByGM,
                                    IsOnline = v.IsOnline,
                                    StartDate = v.StartDate,
                                    EndDate = v.EndDate,
                                    VisitingDetailReport = detailReportList,
                                    CreatedAt = v.CreatedAt,
                                    CreatedBy = v.CreatedBy,
                                    UpdatedAt = v.UpdatedAt,
                                    UpdatedBy = v.UpdatedBy,
                                })
                               .AsNoTracking()
                               .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                mstLst = mstLst.Where(Q => Q.VisitingStatus.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(area))
            {
                mstLst = mstLst.Where(Q => Q.NetworkCity.ToLower() == area.ToLower());
            }

            if (!string.IsNullOrEmpty(query))
            {
                mstLst = mstLst.Where(Q => Q.AhmCode.ToLower().Contains(query.ToLower()) || Q.MDCode.ToLower().Contains(query.ToLower()) || Q.DealerName.ToLower().Contains(query.ToLower()) || Q.NetworkAddress.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (startDate.Trim() == endDate.Trim())
                {
                    mstLst = mstLst.Where(Q => Q.StartDate.Date == DateTime.Parse(startDate).Date);
                }
                else
                {
                    mstLst = mstLst.Where(Q => Q.StartDate.Date >= DateTime.Parse(startDate).Date && Q.EndDate <= DateTime.Parse(endDate).Date);
                }
            }

            var dataResult = await mstLst.OrderByDescending(Q => Q.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var index = 0;
            foreach (var datum in dataResult)
            {

                var createdName = await GetCreatedByName(Guid.Parse(datum.CreatedBy));

                dataResult[index].CreatedByName = createdName;
                index++;
            }

            return dataResult;
        }

        public async Task<IEnumerable<VisitingReportDtoLoginViewModel>> GetAllVisitingReports(string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            var lstContext = (from vs in RepositoryContext.Visitings
                              join nt in RepositoryContext.Networks on vs.NetworkId equals nt.NetworkId
                              join vt in RepositoryContext.VisitingTypes on vs.VisitingTypeId equals vt.VisitingTypeId
                              join vp in RepositoryContext.VisitingPeoples on vs.VisitingId equals vp.VisitingId into grppeople
                              from vp in grppeople.DefaultIfEmpty()
                              where vs.VisitingStatus.Contains(status) && vs.IsDeleted == false
                              select new VisitingReportDtoLoginViewModel
                              {
                                  VisitingId = vs.VisitingId,
                                  VisitingTypeId = vs.VisitingTypeId,
                                  VisitingTypeName = vt.VisitingTypeName,
                                  NetworkId = vs.NetworkId,
                                  AhmCode = nt.AhmCode,
                                  MDCode = nt.MDCode,
                                  DealerName = nt.DealerName,
                                  NetworkAddress = nt.Address,
                                  NetworkCity = nt.City,
                                  VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.Username }).ToList(),
                                  VisitingComment = vs.VisitingComment,
                                  VisitingCommentByManager = vs.VisitingCommentByManager,
                                  VisitingStatus = vs.VisitingStatus,
                                  ApprovedByGM = vs.ApprovedByGM,
                                  ApprovedByManager = vs.ApprovedByManager,
                                  IsOnline = vs.IsOnline,
                                  StartDate = vs.StartDate,
                                  EndDate = vs.EndDate,
                                  CreatedAt = vs.CreatedAt,
                                  CreatedBy = vs.CreatedBy,
                                  UpdatedAt = vs.UpdatedAt,
                                  UpdatedBy = vs.UpdatedBy,
                                  JenisVisitingSingleOrJoin = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Count() > 0 ? "Join" : "Single"
                              })
                            .AsNoTracking()
                            .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                lstContext = lstContext.Where(Q => Q.VisitingStatus.ToLower() == status.ToLower());
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

        public async Task<IEnumerable<VisitingReportDtoLoginViewModel>> GetAllVisitingByManagerDivisionId(Guid EmployeeId, string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                var divisionId = await (from usr in RepositoryContext.Users
                                        where usr.EmployeeId == EmployeeId
                                        select usr.DivisionId).FirstOrDefaultAsync();

                var userList = await (from usr in RepositoryContext.Users
                                      join vst in RepositoryContext.Visitings on usr.EmployeeId.ToString() equals vst.CreatedBy
                                      where usr.EmployeeId != EmployeeId && usr.DivisionId == divisionId
                                      select vst.VisitingId).Distinct().ToListAsync();

                var lstContext = (from vs in RepositoryContext.Visitings
                                  join nt in RepositoryContext.Networks on vs.NetworkId equals nt.NetworkId
                                  join vt in RepositoryContext.VisitingTypes on vs.VisitingTypeId equals vt.VisitingTypeId
                                  where userList.Contains(vs.VisitingId) && vs.IsDeleted == false
                                  select new VisitingReportDtoLoginViewModel
                                  {
                                      VisitingId = vs.VisitingId,
                                      VisitingTypeId = vs.VisitingTypeId,
                                      VisitingTypeName = vt.VisitingTypeName,
                                      NetworkId = vs.NetworkId,
                                      AhmCode = nt.AhmCode,
                                      MDCode = nt.MDCode,
                                      DealerName = nt.DealerName,
                                      NetworkAddress = nt.Address,
                                      NetworkCity = nt.City,
                                      VisitingPeoples = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Select(c => new EmployeeListInVisitingPeople { EmployeeId = c.EmployeeId, EmployeeName = c.User.Username }).ToList(),
                                      VisitingComment = vs.VisitingComment,
                                      VisitingCommentByManager = vs.VisitingCommentByManager,
                                      VisitingStatus = vs.VisitingStatus,
                                      ApprovedByGM = vs.ApprovedByGM,
                                      ApprovedByManager = vs.ApprovedByManager,
                                      IsOnline = vs.IsOnline,
                                      StartDate = vs.StartDate,
                                      EndDate = vs.EndDate,
                                      CreatedAt = vs.CreatedAt,
                                      CreatedBy = vs.CreatedBy,
                                      UpdatedAt = vs.UpdatedAt,
                                      UpdatedBy = vs.UpdatedBy,
                                      JenisVisitingSingleOrJoin = RepositoryContext.VisitingPeoples.Where(c => c.VisitingId == vs.VisitingId).Count() > 0 ? "Join" : "Single"
                                  })
                                .AsNoTracking()
                                .AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    lstContext = lstContext.Where(Q => Q.VisitingStatus.ToLower() == status.ToLower());
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
