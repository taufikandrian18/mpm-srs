using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces.FCM
{
    public interface IUserFcmTokenProfileRepository : IUserProfile<UserFcmTokens>
    {
        Task<IEnumerable<UserFcmTokens>> GetAllUserFcmTokens(int pageSize, int pageIndex);
        Task<UserFcmTokens> GetUserFcmTokenByEmployeeId(Guid employeeId);
        void CreateUserFcmToken(UserFcmTokens userFcmToken);
        void UpdateUserFcmToken(UserFcmTokens userFcmToken);
        void DeleteUserFcmToken(UserFcmTokens userFcmToken);
    }
}
