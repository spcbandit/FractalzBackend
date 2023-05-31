using System;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Requests.Books.Get;
using Fractalz.Application.Domains.Responses.Books.Get;
using MediatR;

namespace Fractalz.Application.Handlers.Books.Get;

public class GetBookSectionHandler:IRequestHandler<GetBookSectionRequest, GetBookSectionResponse>
{
    private readonly IRepository<BookSections> _repository;
    public GetBookSectionHandler(IRepository<BookSections> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetBookSectionResponse> Handle(GetBookSectionRequest request, CancellationToken cancellationToken)
    {
        var getOwner = _repository.GetWithInclude(x => x.OwnerId == request.OwnerId && x.BooksId == request.BookId);
        if (getOwner == null)
        {
            return new GetBookSectionResponse() { Message = "SectGet == false!", Success = false};
        }

        return new GetBookSectionResponse() { Message = "SectGet", Success = true, BookSectionsList = getOwner};

    }
}