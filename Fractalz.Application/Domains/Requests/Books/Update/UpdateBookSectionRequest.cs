using System;
using Fractalz.Application.Domains.Responses.Books.Update;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Books.Update;

public class UpdateBookSectionRequest:IRequest<UpdateBookSectionResponse>
{
    public Guid SectionId { get; set; }
    public string SectionName { get; set; }
}