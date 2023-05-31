using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class GetRoomsRequest : IRequest<GetRoomsResponse>
    {
        public Guid ServerId { get; set; }
    }
}