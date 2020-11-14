using System.Threading.Tasks;
using HumanityService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HumanityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> NotifyUser() //from body
        {

        }

    }
}
