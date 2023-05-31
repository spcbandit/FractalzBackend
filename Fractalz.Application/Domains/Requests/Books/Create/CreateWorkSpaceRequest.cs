using System;
using System.Collections.Generic;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books;

public class CreateWorkSpaceRequest:IRequest<CreateWorkSpaceResponse>
{
    public Guid OwnerId { get; set; }
    public Guid AllowedUserId { get; set; }
    public Guid BookId { get; set; }
}