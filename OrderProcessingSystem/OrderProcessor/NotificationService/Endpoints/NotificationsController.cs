using Microsoft.AspNetCore.Mvc;
using OrderProcessor.NotificationService.Application;
using OrderProcessor.NotificationService.Application.Dtos;

namespace OrderProcessor.NotificationService.Endpoints;

[ApiController]
[Route("api/notifications")]
public class NotificationsController(INotificationsService notificationsService): ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllNotifications()
    {
        var notifications = notificationsService.GetAllNotifications();
        return Ok(notifications);
    }
}