using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Handlers.Voice
{
    public class GetMyServersHandler : IRequestHandler<GetMyServersRequest, GetMyServersResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;

        public GetMyServersHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentException(nameof(repositoryUser));
        }

        public async Task<GetMyServersResponse> Handle(GetMyServersRequest request, CancellationToken cancellationToken)
        {
            var user = _repositoryUser
                .GetWithInclude(x => x.Id == request.UserId,
                    x => x.MyVoiceServer)
                .FirstOrDefault();
            if (user != null)
            {
                return new GetMyServersResponse() {Success = true, Servers = user.MyVoiceServer};
            }
            else
            {
                return new GetMyServersResponse() {Success = false, Message = "Пользователь не найден"};
            }
        }
    }
}