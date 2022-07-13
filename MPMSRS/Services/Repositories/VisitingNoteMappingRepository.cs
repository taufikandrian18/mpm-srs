using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class VisitingNoteMappingRepository : UserBase<VisitingNoteMappings>, IVisitingNoteMappingProfileRepository
    {
        public VisitingNoteMappingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingNoteMapping(VisitingNoteMappings VisitingNoteMapping)
        {
            Create(VisitingNoteMapping);
        }

        public void DeleteVisitingNoteMapping(VisitingNoteMappings VisitingNoteMapping)
        {
            Update(VisitingNoteMapping);
        }

        public async Task<IEnumerable<VisitingNoteMappings>> GetAllVisitingNoteMappings(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<VisitingNoteMappings> GetVisitingNoteMappingById(Guid visitingNoteMappingId)
        {
            return await FindByCondition(ow => ow.VisitingNoteMappingId.Equals(visitingNoteMappingId))
                .Where(un => un.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<VisitingNoteMappings> GetVisitingNoteMappingByUserId(Guid employeeId)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.EmployeeId == employeeId)
                .OrderByDescending(ow => ow.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VisitingNoteMappings>> GetVisitingNoteMappingByVisitingId(Guid visitingId, int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false && ow.VisitingId == visitingId)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void UpdateVisitingNoteMapping(VisitingNoteMappings VisitingNoteMapping)
        {
            Update(VisitingNoteMapping);
        }
    }
}
