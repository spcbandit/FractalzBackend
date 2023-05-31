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
    public class GetUsersRoomHandler : IRequestHandler<GetUsersRoomRequest, GetUsersRoomResponse>
    {
        private readonly IRepository<VoiceRoom> _repositoryRoom;

        public GetUsersRoomHandler(IRepository<VoiceRoom> repositoryRoom)
        {
            _repositoryRoom = repositoryRoom;
        }

        public async Task<GetUsersRoomResponse> Handle(GetUsersRoomRequest request, CancellationToken cancellationToken)
        {
            var room = _repositoryRoom.GetWithInclude(x => x.Id == request.RoomId, x => x.Users).FirstOrDefault();
            if (room != null)
            {
                return new GetUsersRoomResponse() {Success = true, Users = room.Users.ToList()};
            }
            else
            {
                return new GetUsersRoomResponse() {Success = false, Message = "Проблемы с получением пользователей"};
            }
        }
    }
}