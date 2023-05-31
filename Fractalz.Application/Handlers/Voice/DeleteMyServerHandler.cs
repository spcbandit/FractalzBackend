using System;
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
    public class DeleteMyServerHandler : IRequestHandler<DeleteMyServerRequest, DeleteMyServerResponse>
    {
        private readonly IRepository<VoiceServer> _repositoryServer;

        public DeleteMyServerHandler(IRepository<VoiceServer> repositoryServer)
        {
            _repositoryServer = repositoryServer ?? throw new ArgumentException(nameof(repositoryServer));
        }

        public async Task<DeleteMyServerResponse> Handle(DeleteMyServerRequest request, CancellationToken cancellationToken)
        {
            var server = _repositoryServer.Get(x => x.Id == request.ServerId).FirstOrDefault();
            if (server != null)
            {
                var reuslt  = _repositoryServer.Remove(server);
                if (reuslt != 0)
                {
                    return new DeleteMyServerResponse() {Success = true};
                }
                else
                {
                    return new DeleteMyServerResponse() {Success = false, Message = "Не удалось удалить сервер"};
                }
            }
            else
            {
                return new DeleteMyServerResponse()
                    {Success = false, Message = "Голосовой сервер не найдет или уже был удален"};
            }
        }
    }
}