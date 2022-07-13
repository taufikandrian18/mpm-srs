using System;
namespace MPMSRS.Services.Interfaces
{
    public interface IMyEmailSender
    {
        void SendEmail(string email, string subject, string HtmlMessage);
    }
}
