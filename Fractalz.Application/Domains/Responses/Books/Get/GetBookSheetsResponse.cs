using Fractalz.Application.Domains.Entities.Documents;

namespace Fractalz.Application.Domains.Responses.Books.Get;

public class GetBookSheetsResponse: BasicResponse
{
    public BookSheets BookSheets { get; set; }
}