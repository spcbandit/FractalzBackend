using System;
using Fractalz.Application.Domains.Responses.Books.Update;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books.Update;

public class UpdateBookRequest: IRequest<UpdateBookResponse>
{
    public Guid BookId { get; set; }
    public string BookName { get; set; }
}