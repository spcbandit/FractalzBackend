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
    public class GetRoomsHandler : IRequestHandler<GetRoomsRequest, GetRoomsResponse>
    {
        private readonly IRepository<VoiceServer> _repositoryServer;

        public GetRoomsHandler(IRepository<VoiceServer> repositoryServer)
        {
            _repositoryServer = repositoryServer;
        }

        public async Task<GetRoomsResponse> Handle(GetRoomsRequest request, CancellationToken cancellationToken)
        {
            var server = _repositoryServer.GetWithInclude(x => x.Id == request.ServerId, x => x.Rooms).FirstOrDefault();
            if (server != null)
            {
                return new GetRoomsResponse() {Success = true, Rooms = server.Rooms};
            }
            else
            {
                return new GetRoomsResponse() {Success = false, Message = "Не удалось получить комнаты сервера"};
            }
        }
    }
}