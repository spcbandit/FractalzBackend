using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Handlers.Voice
{
    public class GetOtherServersHandler : IRequestHandler<GetOtherServersRequest, GetOtherServersResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;

        public GetOtherServersHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task<GetOtherServersResponse> Handle(GetOtherServersRequest request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}