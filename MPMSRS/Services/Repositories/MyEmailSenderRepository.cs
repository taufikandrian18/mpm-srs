using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using MPMSRS.Services.Interfaces;

namespace MPMSRS.Services.Repositories
{
    public class MyEmailSenderRepository : IMyEmailSender
    {
        public IConfiguration Configuration { get; }

        public MyEmailSenderRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendEmail(string email, string subject, string HtmlMessage)
        {
            try
            {
                using (MailMessage mm = new MailMessage(Configuration["Smtp:SenderName"], email))
                {
                    mm.Subject = subject;
                    string body = HtmlMessage;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = Configuration["Smtp:Host"];
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(Configuration["Smtp:SenderName"], Configuration["Smtp:Password"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
