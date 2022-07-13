using System;
using System.Threading.Tasks;
using MPMSRS.Models.VM;

namespace MPMSRS.Services.Interfaces.FCM
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
