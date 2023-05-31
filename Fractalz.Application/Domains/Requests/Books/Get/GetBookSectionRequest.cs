using System;
using Fractalz.Application.Domains.Responses.Books.Get;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books.Get;

public class GetBookSectionRequest: IRequest<GetBookSectionResponse>
{
    public Guid OwnerId { get; set; }
    public Guid BookId { get; set; }
}