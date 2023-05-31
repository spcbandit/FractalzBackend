using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class DeleteRoomRequest : IRequest<DeleteRoomResponse>
    {
        public Guid ServerId { get; set; }
        public Guid RoomId { get; set; }
    }
}