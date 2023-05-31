using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Handlers.Voice
{
    public class AddOtherServerHandler : IRequestHandler<AddOtherServerRequest, AddOtherServerResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryuser;
        private readonly IRepository<VoiceServer> _repositoryServer;

        /// <summary>
        /// AddOtherServerHandler
        /// </summary>
        /// <param name="repositoryuser"></param>
        /// <param name="repositoryServer"></param>
        public AddOtherServerHandler(IRepository<Domains.Entities.Profile.User> repositoryuser, IRepository<VoiceServer> repositoryServer)
        {
            _repositoryuser = repositoryuser;
            _repositoryServer = repositoryServer;
        }
        
        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AddOtherServerResponse> Handle(AddOtherServerRequest request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}