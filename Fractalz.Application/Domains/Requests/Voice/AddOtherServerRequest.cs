using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class AddOtherServerRequest : IRequest<AddOtherServerResponse>
    {
        public Guid UserId { get; set; }
        public Guid ServerId { get; set; }
    }
}