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
    public class EditRoomHandler : IRequestHandler<EditRoomRequest, EditRoomResponse>
    {
        private readonly IRepository<VoiceRoom> _repositoryRoom;

        public EditRoomHandler(IRepository<VoiceRoom> repositoryRoom)
        {
            _repositoryRoom = repositoryRoom ?? throw new ArgumentException(nameof(repositoryRoom));
        }

        public async Task<EditRoomResponse> Handle(EditRoomRequest request, CancellationToken cancellationToken)
        {
            var room = _repositoryRoom.Get(x => x.Id == request.RoomId).FirstOrDefault();
            if (room != null)
            {
                room.Name = request.NameRoom;
                room.Updated = DateTime.Now;
                var result = _repositoryRoom.Update(room);
                if (result != 0)
                {
                    return new EditRoomResponse() {Success = true, Room = room};
                }
                else
                {
                    return new EditRoomResponse() {Success = false, Message = "Не удалось обновить комнату"};
                }
            }
            else
            {
                return new EditRoomResponse() {Success = false, Message = "Не удалось найти комнату в базе данных"};
            }
        }
    }
}