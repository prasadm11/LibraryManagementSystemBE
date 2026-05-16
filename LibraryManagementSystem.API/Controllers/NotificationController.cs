using LibraryManagementSystem.Application.Features.Notification.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotificationsByUserId(int userId)
    {
        var result = await _mediator.Send(new GetMyNotificationsQuery(userId));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> MarkAsRead(int userId)
    {
        var result = await _mediator.Send(new MarkNotificationAsReadCommand(userId));
        return Ok(result);
    }
}