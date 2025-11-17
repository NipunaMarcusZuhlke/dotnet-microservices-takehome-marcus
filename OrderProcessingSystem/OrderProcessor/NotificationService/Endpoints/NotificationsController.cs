using Microsoft.AspNetCore.Mvc;
using OrderProcessor.Middleware;
using OrderProcessor.NotificationService.Application.Dtos;
using OrderProcessor.NotificationService.Application.Services;

namespace OrderProcessor.NotificationService.Endpoints;

[ApiController]
[Route("api/notifications")]
public class NotificationsController(INotificationsService notificationsService): ControllerBase
{
    [HttpGet]
    [EndpointDescription("Get all notifications which are successfully processed")]
    [ProducesResponseType(typeof(NotificationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications = await notificationsService.GetAllNotificationsAsync();
        return Ok(notifications);
    }
}