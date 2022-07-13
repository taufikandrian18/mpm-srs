using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class RankRepository : UserBase<Ranks>, IRankProfileRepository
    {
        public RankRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateRank(Ranks rank)
        {
            Create(rank);
        }

        public void DeleteRank(Ranks rank)
        {
            Update(rank);
        }

        public async Task<IEnumerable<Ranks>> GetAllRanks(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.UserStaffName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Ranks> GetRankById(Guid rankId)
        {
            return await FindByCondition(rank => rank.RankId.Equals(rankId))
                .Where(role => role.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<Ranks> GetRankByUserStaff(string userStaff)
        {
            return await FindByCondition(rank => rank.UserStaff.ToLower().Trim() == userStaff.ToLower().Trim())
                .Where(role => role.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public void UpdateRank(Ranks rank)
        {
            Update(rank);
        }
    }
}
