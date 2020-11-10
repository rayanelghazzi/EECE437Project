using HumanityService.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class NotificationService : INotificationService
    {
        public Task NotifyUser(string recipient, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
