using System;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books;

public class DeleteSectionRequest : IRequest<DeleteSectionsResponse>
{
    public Guid Id{ get; set; }
}