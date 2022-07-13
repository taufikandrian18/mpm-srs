using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IRankProfileRepository : IUserProfile<Ranks>
    {
        Task<IEnumerable<Ranks>> GetAllRanks(int pageSize, int pageIndex);
        Task<Ranks> GetRankById(Guid rankId);
        Task<Ranks> GetRankByUserStaff(string userStaff);
        void CreateRank(Ranks rank);
        void UpdateRank(Ranks rank);
        void DeleteRank(Ranks rank);
    }
}
