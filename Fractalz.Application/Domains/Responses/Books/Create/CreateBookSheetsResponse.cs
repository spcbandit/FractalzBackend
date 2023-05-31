using Fractalz.Application.Domains.Entities.Documents;

namespace Fractalz.Application.Domains.Responses.Books;

public class CreateBookSheetsResponse:BasicResponse
{
    public BookSheets BookSheets { get; set; }
}