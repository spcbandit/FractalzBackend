using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Notification;
using Fractalz.Application.Domains.Responses.Notification;
using MediatR;

namespace Fractalz.Application.Handlers.Notification;

public class SendNotificationHandler : IRequestHandler<SendNotificationRequest, SendNotificationResponse>
{
    private readonly ILinkedEventService _linkedEventService;
    private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;

    public SendNotificationHandler(ILinkedEventService linkedEventService, IRepository<Domains.Entities.Profile.User> repositoryUser)
    {
        _linkedEventService = linkedEventService ?? throw new ArgumentException(nameof(linkedEventService));
        _repositoryUser = repositoryUser;
    }

    public async Task<SendNotificationResponse> Handle(SendNotificationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.Message += $" ({request.FromUser})";
            request.Title =  request.Title;
            _linkedEventService.InvokeSendNotification(request);
            return new SendNotificationResponse() {Success = true};
        }
        catch
        {
            return new SendNotificationResponse() {Success = false, Message = "Упс.. что то пошло не так"};
        }
    }
}