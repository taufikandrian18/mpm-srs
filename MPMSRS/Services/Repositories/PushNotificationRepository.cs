using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class PushNotificationRepository : UserBase<PushNotifications>, IPushNotificationProfileRepository
    {
        public PushNotificationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreatePushNotification(PushNotifications pushNotification)
        {
            Create(pushNotification);
        }

        public void DeletePushNotification(PushNotifications pushNotification)
        {
            Update(pushNotification);
        }

        public async Task<IEnumerable<PushNotifications>> GetAllPushNotifications(int pageSize, int pageIndex)
        {
            return await FindAll()
                .Where(ow => ow.IsDeleted == false)
                .OrderByDescending(ow => ow.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<PushNotifications> GetPushNotificationById(Guid pushNotificationId)
        {
            return await FindByCondition(user => user.PushNotificationId.Equals(pushNotificationId))
                .Where(user => user.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<PushNotifications> GetPushNotificationBySenderId(Guid employeeId)
        {
            return await FindByCondition(user => user.EmployeeId.Equals(employeeId))
                .Where(user => user.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PushNotifications>> GetAllPushNotificationByRecipientId(Guid recipientId, int pageSize, int pageIndex)
        {
            try
            {
                return await FindByCondition(user => user.RecipientId.Equals(recipientId))
                    .Where(user => user.IsDeleted == false)
                    .OrderByDescending(user => user.CreatedAt)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PushNotifications>> GetPushNotificationByRecipientId(Guid recipientId, bool isRead)
        {
            var lstContext =  FindByCondition(user => user.RecipientId.Equals(recipientId))
                .Where(user => user.IsDeleted == false)
                .AsNoTracking()
                .AsQueryable();

            if (isRead)
            {
                lstContext = lstContext.Where(Q => Q.IsRead == isRead);
            }
            else
            {
                lstContext = lstContext.Where(Q => Q.IsRead == isRead);
            }

            var data = await lstContext.OrderByDescending(Q => Q.CreatedAt).ToListAsync();

            return data;
        }

        public void UpdatePushNotification(PushNotifications pushNotification)
        {
            Update(pushNotification);
        }
    }
}
