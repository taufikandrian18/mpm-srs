using System;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces.Auth
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByToken(string token);

        Task<RefreshToken> GetByEmployeeId(Guid employeeId);

        Task Create(RefreshToken refreshToken);

        Task Delete(Guid id);

        Task DeleteAll(Guid employeeId);
    }
}
