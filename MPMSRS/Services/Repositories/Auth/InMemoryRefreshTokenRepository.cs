using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces.Auth;

namespace MPMSRS.Services.Repositories.Auth
{
    public class InMemoryRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();

        public InMemoryRefreshTokenRepository()
        {
        }

        public Task Create(RefreshToken refreshToken)
        {
            refreshToken.Id = Guid.NewGuid();

            _refreshTokens.Add(refreshToken);

            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            _refreshTokens.RemoveAll(r => r.Id == id);

            return Task.CompletedTask;
        }

        public Task DeleteAll(Guid employeeId)
        {
            _refreshTokens.RemoveAll(r => r.EmployeeId == employeeId);

            return Task.CompletedTask;
        }

        public Task<RefreshToken> GetByEmployeeId(Guid employeeId)
        {
            RefreshToken refreshToken = _refreshTokens.FirstOrDefault(r => r.EmployeeId == employeeId);

            return Task.FromResult(refreshToken);
        }

        public Task<RefreshToken> GetByToken(string token)
        {
            RefreshToken refreshToken = _refreshTokens.FirstOrDefault(r => r.Token == token);

            return Task.FromResult(refreshToken);
        }
    }
}
