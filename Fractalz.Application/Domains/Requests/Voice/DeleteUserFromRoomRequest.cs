using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class DeleteUserFromRoomRequest : IRequest<DeleteUserFromRoomResponse>
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
    }
}