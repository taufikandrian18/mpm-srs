using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces.FCM;

namespace MPMSRS.Services.Repositories.FCM
{
    public class UserFcmTokenRepository : UserBase<UserFcmTokens>, IUserFcmTokenProfileRepository
    {
        public UserFcmTokenRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUserFcmToken(UserFcmTokens userFcmToken)
        {
            Create(userFcmToken);
        }

        public void DeleteUserFcmToken(UserFcmTokens userFcmToken)
        {
            Update(userFcmToken);
        }

        public async Task<IEnumerable<UserFcmTokens>> GetAllUserFcmTokens(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<UserFcmTokens> GetUserFcmTokenByEmployeeId(Guid employeeId)
        {
            return await FindByCondition(user => user.EmployeeId.Equals(employeeId))
            .Where(user => user.IsDeleted == false)
            .FirstOrDefaultAsync();
        }

        public void UpdateUserFcmToken(UserFcmTokens userFcmToken)
        {
            Update(userFcmToken);
        }
    }
}
