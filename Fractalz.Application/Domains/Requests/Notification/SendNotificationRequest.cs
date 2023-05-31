using System;
using System.Collections.Generic;
using Fractalz.Application.Domains.Responses.Notification;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Notification;

public class SendNotificationRequest : IRequest<SendNotificationResponse>
{
    public List<Guid> UsersId { get; set; }
    public string FromUser { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}