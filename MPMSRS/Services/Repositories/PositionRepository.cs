using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class PositionRepository : UserBase<Positions>, IPositionProfileRepository
    {
        public PositionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreatePosition(Positions position)
        {
            Create(position);
        }

        public void DeletePosition(Positions position)
        {
            Update(position);
        }

        public async Task<IEnumerable<Positions>> GetAllPositions(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderBy(ow => ow.NamaJabatan)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Positions> GetPositionsById(Guid positionId)
        {
            return await FindByCondition(position => position.PositionId.Equals(positionId))
            .Where(position => position.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<Positions> GetPositionsByKodeJabatan(int kodeJabatan)
        {
            return await FindByCondition(position => position.KodeJabatan.Equals(kodeJabatan))
            .Where(position => position.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public async Task<Positions> GetPositionsByNamaGroupPosition(string namaGroupPosition)
        {
            return await FindByCondition(position => position.NamaGroupPosition.ToLower().Contains(namaGroupPosition.ToLower()))
            .FirstOrDefaultAsync();
        }

        public async Task<Positions> GetPositionsByNamaJabatan(string namaJabatan)
        {
            return await FindByCondition(position => position.NamaJabatan.ToLower().Contains(namaJabatan.ToLower()))
            .FirstOrDefaultAsync();
        }

        public void UpdatePosition(Positions position)
        {
            Update(position);
        }
    }
}
