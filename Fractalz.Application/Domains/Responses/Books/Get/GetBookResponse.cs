using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.Books.Get;

public class GetBookResponse:BasicResponse
{
    public IEnumerable<Entities.Documents.Books> Book { get; set; }
    
}