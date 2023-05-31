using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class EditMyServerRequest: IRequest<EditMyServerResponse>
    {
        public string NameServer { get; set; }
        public Guid ServerId { get; set; }
    }
}