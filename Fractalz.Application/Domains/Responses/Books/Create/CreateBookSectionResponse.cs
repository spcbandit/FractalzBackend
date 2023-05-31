using Fractalz.Application.Domains.Entities.Documents;

namespace Fractalz.Application.Domains.Responses.Books;

public class CreateBookSectionResponse:BasicResponse
{
    public string SectionName { get; set; }
    public Entities.Documents.Books Books { get; set; }
}