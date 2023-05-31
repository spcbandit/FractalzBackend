using System;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books;

public class CreateBookSectionRequest:IRequest<CreateBookSectionResponse>
{
    public Guid BookId { get; set; }
    public string SectionName { get; set; }
    public Guid OwnerId { get; set; }
}