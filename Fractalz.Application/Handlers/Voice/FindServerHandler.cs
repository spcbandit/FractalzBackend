using System;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Handlers.Voice
{
    public class FindServerHandler : IRequestHandler<FindServerRequest, FindServerResponse>
    {
        private readonly IRepository<VoiceServer> _repositoryServer;

        public FindServerHandler(IRepository<VoiceServer> repositoryServer)
        {
            _repositoryServer = repositoryServer ?? throw new ArgumentException(nameof(repositoryServer));
        }
        
        public async Task<FindServerResponse> Handle(FindServerRequest request, CancellationToken cancellationToken)
        {
            var server = _repositoryServer.Get(x => x.Name.Contains(request.Name)).FirstOrDefault();
            
            if (server != null)
            {
                return new FindServerResponse() {Success = true, Servers = server};
            }
            else
            {
                return new FindServerResponse() {Success = false, Message = "Сервера не найдены"};
            }
        }
    }
}