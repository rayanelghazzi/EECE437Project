using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using HumanityService.Services.Interfaces;

namespace HumanityService.Services
{
    public class NotificationService : INotificationService
    {
        public void NotifyUser(string recipient, string subject, string body)
        {
            var fromAddress = new MailAddress("humanityservice.ece437@gmail.com", "HumanityService");
            var toAddress = new MailAddress(recipient);
            const string fromPassword = "Abcdefg1234";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
