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
    public class CreateRoomHandler: IRequestHandler<CreateRoomRequest, CreateRoomResponse>
    {
        private readonly IRepository<VoiceServer> _repositoryServer;
        private readonly IRepository<VoiceRoom> _repositoryRoom;

        public CreateRoomHandler(IRepository<VoiceServer> repositoryServer, IRepository<VoiceRoom> repositoryRoom)
        {
            _repositoryServer = repositoryServer ?? throw new ArgumentException(nameof(repositoryServer));
            _repositoryRoom = repositoryRoom;
        }
        

        public async Task<CreateRoomResponse> Handle(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var server = _repositoryServer.Get(x => x.Id == request.ServerId).FirstOrDefault();
            var room = new VoiceRoom()
            {
                Name = request.Name
            };
            var resultRoom = _repositoryRoom.Create(room);
            if (resultRoom != 0)
            {
                server.Rooms.Add(room);

                var result = _repositoryServer.Update(server);
                if (result != 0)
                {
                    return new CreateRoomResponse()
                    {
                        Success = true, Room = server.Rooms.FirstOrDefault()
                    };
                }
                else
                {
                    return new CreateRoomResponse() {Success = false, Message = "Не получилось создать комнату"};
                }
            }
            else
            {
                return new CreateRoomResponse() {Success = false, Message = "Не получилось создать комнату"};
            }
        }
    }
}