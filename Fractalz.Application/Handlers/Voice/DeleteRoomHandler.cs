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
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomRequest, DeleteRoomResponse>
    {
        private readonly IRepository<VoiceRoom> _repositoryServer;

        public DeleteRoomHandler(IRepository<VoiceRoom> repositoryServer)
        {
            _repositoryServer = repositoryServer ?? throw new ArgumentException(nameof(repositoryServer));
        }

        public async Task<DeleteRoomResponse> Handle(DeleteRoomRequest request, CancellationToken cancellationToken)
        {
            var room = _repositoryServer.Get(x => x.Id == request.RoomId).FirstOrDefault();
            if (room != null)
            {
                var result = _repositoryServer.Remove(room);
                if (result != 0)
                {
                    return new DeleteRoomResponse() {Success = true};
                }
                else
                {
                    return new DeleteRoomResponse()
                        {Success = false, Message = "Не удалось удалить комнату в базе данных"};
                }
            }
            else
            {
                return new DeleteRoomResponse()
                    {Success = false, Message = "Не удалось найти комнату или комната уже была удалена"};
            }
        }
    }
}