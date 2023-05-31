using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class CreateMyServerRequest : IRequest<CreateMyServerResponse>
    {
        public string NameServer { get; set; }
        public Guid UserId { get; set; }
    }
}