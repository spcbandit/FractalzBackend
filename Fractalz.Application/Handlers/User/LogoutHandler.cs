using System;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Handlers.User
{
    public class LogoutHandler : IRequestHandler<LogoutRequest, LogoutResponse>
    {
        private readonly ILinkedEventService _linkedEventService;
        public LogoutHandler(ILinkedEventService linkedEventService)
        {
            _linkedEventService = linkedEventService ?? throw new ArgumentException(nameof(linkedEventService));
        }

        public async Task<LogoutResponse> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            _linkedEventService.InvokeUserUpdateStatus(null);
            throw new System.NotImplementedException();
        }
    }
}