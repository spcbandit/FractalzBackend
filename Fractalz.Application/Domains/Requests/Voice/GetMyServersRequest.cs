using System;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class GetMyServersRequest: IRequest<GetMyServersResponse>
    {
        public Guid UserId { get; set; }
    }
}