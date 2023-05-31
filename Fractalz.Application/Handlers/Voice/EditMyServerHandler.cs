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
    public class EditMyServerHandler : IRequestHandler<EditMyServerRequest, EditMyServerResponse>
    {
        private readonly IRepository<VoiceServer> _repositoryServer;

        public EditMyServerHandler(IRepository<VoiceServer> repositoryServer)
        {
            _repositoryServer = repositoryServer ?? throw new ArgumentException(nameof(repositoryServer));
        }
        

        public async Task<EditMyServerResponse> Handle(EditMyServerRequest request, CancellationToken cancellationToken)
        {
            var server = _repositoryServer.Get(x => x.Id == request.ServerId).FirstOrDefault();
            if (server != null)
            {
                server.Name = request.NameServer;
                var result = _repositoryServer.Update(server);
                if (result != 0)
                {
                    return new EditMyServerResponse() {Success = true, Server = server};
                }
                else
                {
                    return new EditMyServerResponse()
                        {Success = false, Message = "не удлось удалить сервер в базе данных"};
                }
            }
            else
            {
                return new EditMyServerResponse() {Success = false, Message = "Сервер не найден в базе данных либо бы удален"};
            }
        }
    }
}