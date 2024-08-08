using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.Controllers
{
    /*[ApiController]
    [Route("api/[controller]/")]*/
    public class SendNotificationsController
    {
        private readonly SendNotificationsService _sendNotificationsService;

        public SendNotificationsController(SendNotificationsService sendNotificationsService)
        {
            _sendNotificationsService = sendNotificationsService;
        }

       /* [HttpPost("SendNoti")]
        public async Task SendNoti(string token, string title, string mess)
        {
            await _sendNotificationsService.SendNoti(token, title, mess);
        }*/

        [HttpPut("UpdateStatus")]
        public async Task UpdateStatus(string notificationId, string status) {
            Console.WriteLine("Update ne");
            await _sendNotificationsService.UpdateNotificationStatusAsync(notificationId, status);
        }
    }
}
