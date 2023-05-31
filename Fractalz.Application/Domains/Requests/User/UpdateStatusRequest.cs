using System;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Domains.Requests.User
{
    public class UpdateStatusRequest : IRequest<UpdateStatusResponse>
    {
        public Guid UserId { get; set; }
        public Status Status { get; set; }
    }
}