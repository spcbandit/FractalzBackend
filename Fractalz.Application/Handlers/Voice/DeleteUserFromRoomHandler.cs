using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fractalz.Application.Handlers.Voice
{
    public class DeleteUserFromRoomHandler : IRequestHandler<DeleteUserFromRoomRequest, DeleteUserFromRoomResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly IRepository<VoiceRoom> _repositoryRoom;

        public DeleteUserFromRoomHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, IRepository<VoiceRoom> repositoryRoom)
        {
            _repositoryUser = repositoryUser;
            _repositoryRoom = repositoryRoom;
        }

        public async Task<DeleteUserFromRoomResponse> Handle(DeleteUserFromRoomRequest request, CancellationToken cancellationToken)
        {
            var room = _repositoryRoom.GetWithInclude(x => x.Id == request.RoomId, x => x.Users).FirstOrDefault();
            if (room != null)
            {
                if(room.Users.FirstOrDefault(x=>x.Id == request.UserId) != null)
                {
                    var user = _repositoryUser.Get(x => x.Id == request.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        user.VoiceRoomId = null;
                        var result = _repositoryUser.Update(user);
                        if (result != 0)
                        {
                            return new DeleteUserFromRoomResponse() {Success = true};
                        }
                        else
                        {
                            return new DeleteUserFromRoomResponse()
                                {Success = false, Message = "Не удалось удалить пользователя из комнаты"};
                        }
                    }
                    else
                    {
                        return new DeleteUserFromRoomResponse() {Success = false, Message = "Пользователь не найден"};
                    }
                }
                else
                {
                    return new DeleteUserFromRoomResponse()
                        {Success = false, Message = "Пользователя уже нету в этой комнате"};
                }
            }
            else
            {
                return new DeleteUserFromRoomResponse()
                    {Success = false, Message = "Не удалось найти комнату"};
                
            }
        }
    }
}