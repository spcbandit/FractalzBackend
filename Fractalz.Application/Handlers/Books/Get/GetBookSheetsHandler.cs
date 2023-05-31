using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Requests.Books.Get;
using Fractalz.Application.Domains.Responses.Books.Get;
using MediatR;

namespace Fractalz.Application.Handlers.Books.Get;

public class GetBookSheetsHandler:IRequestHandler<GetBookSheetsRequest, GetBookSheetsResponse>
{
    public IRepository<BookSheets> _Repository;
    public GetBookSheetsHandler( IRepository<BookSheets> Repository)
    {
        _Repository = Repository ?? throw new ArgumentNullException(nameof(Repository));
    }
    public async Task<GetBookSheetsResponse> Handle(GetBookSheetsRequest request, CancellationToken cancellationToken)
    {
        var get = _Repository.GetWithInclude(x => x.Id == request.Id).FirstOrDefault();
        return new GetBookSheetsResponse() { BookSheets = get};
    }
}