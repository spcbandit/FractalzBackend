using System;
using Fractalz.Application.Domains.Responses.Books.Get;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books.Get;

public class GetBookRequest:IRequest<GetBookResponse>
{
    public Guid OwnerId { get; set; }
}