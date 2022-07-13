using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class VisitingTypeRepository : UserBase<VisitingTypes>, IVisitingTypeProfileRepository
    {
        public VisitingTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateVisitingType(VisitingTypes visitingType)
        {
            Create(visitingType);
        }

        public void DeleteVisitingType(VisitingTypes visitingType)
        {
            Update(visitingType);
        }

        public async Task<IEnumerable<VisitingTypes>> GetAllVisitingTypes(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.VisitingTypeName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<VisitingTypes> GetVisitingTypeById(Guid visitingTypeId)
        {
            return await FindByCondition(vt => vt.VisitingTypeId.Equals(visitingTypeId))
            .Where(vt => vt.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<VisitingTypes> GetVisitingTypeByUsername(string visitingTypeName)
        {
            return await FindByCondition(vt => vt.VisitingTypeName.ToLower().Contains(visitingTypeName.ToLower()))
            .Where(vt => vt.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public void UpdateVisitingType(VisitingTypes visitingType)
        {
            Update(visitingType);
        }
    }
}
