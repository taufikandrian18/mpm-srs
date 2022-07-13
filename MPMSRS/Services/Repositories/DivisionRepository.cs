using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class DivisionRepository : UserBase<Divisions>, IDivisionProfileRepository
    {
        public DivisionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateDivision(Divisions division)
        {
            Create(division);
        }

        public void DeleteDivision(Divisions division)
        {
            Update(division);
        }

        public async Task<IEnumerable<Divisions>> GetAllDivisions(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.DivisionName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Divisions> GetDivisionById(Guid divisionId)
        {
            return await FindByCondition(div => div.DivisionId.Equals(divisionId))
            .Where(div => div.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<Divisions> GetDivisionByUsername(string divisionName)
        {
            return await FindByCondition(div => div.DivisionName.ToLower().Contains(divisionName.ToLower()))
            .Where(div => div.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public void UpdateDivision(Divisions division)
        {
            Update(division);
        }
    }
}
