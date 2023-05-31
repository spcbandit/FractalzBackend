using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class EditRoomRequest : IRequest<EditRoomResponse>
    {
        public Guid RoomId { get; set; }
        public string NameRoom { get; set; }
        public int CountMax { get; set; }
    }
}