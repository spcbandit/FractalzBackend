using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Requests.Notification;
using Fractalz.Application.Domains.Responses.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fractalz.Api.Controllers;

[ApiController]
[Route("/notification/")]
[DisplayName("Уведомления")]
[Produces("application/json")]
public class NotificationController: ControllerBase
{
    private readonly IMediator _mediator;
    public NotificationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [Authorize]
    [Route("sendNotification")]
    [SwaggerResponse(StatusCodes.Status200OK, "Отправить уведомление всем пользователям", typeof(SendNotificationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Отправить уведомление всем пользователям", typeof(SendNotificationResponse))]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
    {
        var resp = await _mediator.Send(request);

        if(resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }

}