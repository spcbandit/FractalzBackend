using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Handlers.User
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusRequest, UpdateStatusResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly ILinkedEventService _linkedEventService;
        public UpdateStatusHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, ILinkedEventService linkedEventService)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentException(nameof(repositoryUser));
            _linkedEventService = linkedEventService ?? throw new ArgumentException(nameof(linkedEventService));
        }

        public async Task<UpdateStatusResponse> Handle(UpdateStatusRequest request, CancellationToken cancellationToken)
        {
            var user = _repositoryUser.Get(x=>x.Id == request.UserId).FirstOrDefault();
            user.OnlineStatus = request.Status;
            var result = _repositoryUser.Update(user);
            if (result != 0)
            {
                _linkedEventService.InvokeUserUpdateStatus(user);
                return new UpdateStatusResponse() {Success = true};
            }
            else
            {
                return new UpdateStatusResponse {Success = false, Message = MessageResource.User_UpdatestatusError};
            }
        }
    }
}