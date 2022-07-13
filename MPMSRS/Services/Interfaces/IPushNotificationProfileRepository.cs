using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPMSRS.Models.Entities;

namespace MPMSRS.Services.Interfaces
{
    public interface IPushNotificationProfileRepository : IUserProfile<PushNotifications>
    {
        Task<IEnumerable<PushNotifications>> GetAllPushNotifications(int pageSize, int pageIndex);
        Task<PushNotifications> GetPushNotificationBySenderId(Guid employeeId);
        Task<PushNotifications> GetPushNotificationById(Guid pushNotificationId);
        Task<IEnumerable<PushNotifications>> GetPushNotificationByRecipientId(Guid recipientId, bool isRead);
        Task<IEnumerable<PushNotifications>> GetAllPushNotificationByRecipientId(Guid recipientId, int pageSize, int pageIndex);
        void CreatePushNotification(PushNotifications pushNotification);
        void UpdatePushNotification(PushNotifications pushNotification);
        void DeletePushNotification(PushNotifications pushNotification);
    }
}
