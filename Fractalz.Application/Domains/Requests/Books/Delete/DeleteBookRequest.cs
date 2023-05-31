using System;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books;

public class DeleteBookRequest: IRequest<DeleteBookResponse>
{
    public Guid BookId { get; set; }
}