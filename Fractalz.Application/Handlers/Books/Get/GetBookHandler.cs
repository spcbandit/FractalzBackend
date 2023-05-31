using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Books.Get;
using Fractalz.Application.Domains.Responses.Books.Get;
using MediatR;

namespace Fractalz.Application.Handlers.Books.Get;

public class GetBookHandler:IRequestHandler<GetBookRequest, GetBookResponse>
{
    private readonly IRepository<Domains.Entities.Documents.Books> _repository;
    public GetBookHandler(IRepository<Domains.Entities.Documents.Books> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetBookResponse> Handle(GetBookRequest request, CancellationToken cancellationToken)
    {
        var check = _repository.GetWithInclude(x => x.OwnerId == request.OwnerId);
        if (check != null)
        {
            return new GetBookResponse() {Success = true, Message = "Book get success", Book = check};
        }
        else
        {
            return new GetBookResponse() { Success = false, Message = "book Not Getted " };
        }
    }
}