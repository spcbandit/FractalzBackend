using System;

namespace Fractalz.Application.Domains.Responses.Books;

public class CreateBooksResponse:BasicResponse
{
    public string BookName { get; set; }
    public string About { get; set; }
    public string Color { get; set; }
    public Guid OwnerId { get; set; }
}