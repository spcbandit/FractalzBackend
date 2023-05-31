using System;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books;

public class CreateBookRequest:IRequest<CreateBooksResponse>
{
    public string BookName { get; set; }
    public string About { get; set; }
    public string Color { get; set; }

    public Guid OwnerId { get; set;  }
}