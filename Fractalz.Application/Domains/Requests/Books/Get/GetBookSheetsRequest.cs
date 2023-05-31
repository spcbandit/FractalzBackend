using System;
using Fractalz.Application.Domains.Responses.Books.Get;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books.Get;

public class GetBookSheetsRequest:IRequest<GetBookSheetsResponse>
{
    public Guid Id { get; set; }
}