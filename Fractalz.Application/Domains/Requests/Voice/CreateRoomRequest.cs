using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class CreateRoomRequest : IRequest<CreateRoomResponse>
    {
        public string Name { get; set; }
        
        public Guid ServerId { get; set; }
        
    }
}