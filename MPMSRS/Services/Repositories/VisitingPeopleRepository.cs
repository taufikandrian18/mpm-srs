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
    public class VisitingPeopleRepository : UserBase<VisitingPeoples>, IVisitingPeopleProfileRepository
    {
        public VisitingPeopleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingPeople(VisitingPeoples visitingPeople)
        {
            Create(visitingPeople);
        }

        public void CreateVisitingPeopleBulk(List<VisitingPeoples> visitingPeople)
        {
            CreateBulk(visitingPeople);
        }

        public void DeleteVisitingPeople(VisitingPeoples visitingPeople)
        {
            Update(visitingPeople);
        }

        public async Task<IEnumerable<VisitingPeoples>> GetAllVisitingPeoples(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<VisitingPeoples> GetVisitingPeopleById(Guid visitingPeopleId)
        {
            return await FindByCondition(un => un.VisitingPeopleId.Equals(visitingPeopleId))
                .Where(un => un.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VisitingPeopleViewModel>> GetVisitingPeopleByVisitingId(Guid visitingId, int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingId == visitingId)
                .Join(RepositoryContext.Users,
                v => v.EmployeeId,
                u => u.EmployeeId,
                (v, u) => new { v, u })
                .Select((x) => new VisitingPeopleViewModel
                {
                    VisitingPeopleId = x.v.VisitingPeopleId,
                    VisitingId = x.v.VisitingId,
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

        public async Task<VisitingPeoples> GetVisitingPeopleByEmployeeId(Guid employeeId)
        {
            return await FindByCondition(un => un.EmployeeId.Equals(employeeId))
                .Where(un => un.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public void UpdateVisitingPeople(VisitingPeoples visitingPeople)
        {
            Update(visitingPeople);
        }

        public void DeleteVisitingPeopleByVisitingId(List<VisitingPeoples> listVisitingPeoples)
        {
            RepositoryContext.VisitingPeoples.RemoveRange(listVisitingPeoples);
        }

        public async Task<IEnumerable<VisitingPeoples>> GetVisitingPeopleByVisitingIdWithoutPage(Guid visitingId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingId == visitingId)
                .OrderByDescending(ow => ow.CreatedAt)
                .ToListAsync();
        }
    }
}
