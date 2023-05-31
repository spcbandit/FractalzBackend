using System;
using Fractalz.Application.Domains.Responses.Books.Update;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books.Update;

public class UpdateBookSheetsRequest:IRequest<UpdateBookSheetsResponse>
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}