using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fractalz.Application.Handlers.Voice
{
    public class InsertUserInRoomHandler : IRequestHandler<InsertUserInRoomRequest, InsertUserInRoomResponse>
    {
        private readonly IRepository<VoiceRoom> _repositoryRoom;
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;

        public InsertUserInRoomHandler( IRepository<VoiceRoom> repositoryRoom, IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryRoom = repositoryRoom;
            _repositoryUser = repositoryUser;
        }

        public async Task<InsertUserInRoomResponse> Handle(InsertUserInRoomRequest request, CancellationToken cancellationToken)
        {
            var room = _repositoryRoom.GetWithInclude(x => x.Id == request.RoomId).FirstOrDefault();
            if (room != null)
            {
                if (room.Users.Where(x => x.Id == request.UserId).FirstOrDefault() == null)
                {
                    var user = _repositoryUser.Get(x => x.Id == request.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        user.VoiceRoomId = room.Id;
                        var result = _repositoryUser.Update(user);
                        if (result != 0)
                        {
                            return new InsertUserInRoomResponse() {Success = true};
                        }
                        else
                        {
                            return new InsertUserInRoomResponse() {Success = false, Message = "Не удалось перейти в комнату"};
                        }
                    }
                    else
                    {
                        return new InsertUserInRoomResponse() {Success = false, Message = "Не удалось найти пользователя"};
                    }
                }
                else
                {
                    return new InsertUserInRoomResponse() {Success = false, Message = "Вы уже находитесь в этой комнате"};
                }
            }
            else
            {
                return new InsertUserInRoomResponse() {Success = false, Message = "Не удалось найти комнату"};
            }
        }
    }
}