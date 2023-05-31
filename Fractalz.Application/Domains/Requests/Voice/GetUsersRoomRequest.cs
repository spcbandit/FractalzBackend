using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class GetUsersRoomRequest : IRequest<GetUsersRoomResponse>
    {
        public Guid RoomId { get; set; }
    }
}