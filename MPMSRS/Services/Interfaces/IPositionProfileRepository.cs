using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IPositionProfileRepository : IUserProfile<Positions>
    {
        Task<IEnumerable<Positions>> GetAllPositions(int pageSize, int pageIndex);
        Task<Positions> GetPositionsById(Guid positionId);
        Task<Positions> GetPositionsByKodeJabatan(int kodeJabatan);
        Task<Positions> GetPositionsByNamaJabatan(string namaJabatan);
        Task<Positions> GetPositionsByNamaGroupPosition(string namaGroupPosition);
        void CreatePosition(Positions position);
        void UpdatePosition(Positions position);
        void DeletePosition(Positions position);
    }
}
