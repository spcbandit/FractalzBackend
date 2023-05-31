using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Documents;

namespace Fractalz.Application.Domains.Responses.Books.Get;

public class GetBookSectionResponse:BasicResponse
{
    public IEnumerable<BookSections> BookSectionsList { get; set; }

}