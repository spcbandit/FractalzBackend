using System;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books;

public class CreateBookSheetsRequest:IRequest<CreateBookSheetsResponse>
{
    public Guid SectionId { get; set; }
}