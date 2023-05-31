using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Requests.Books;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Handlers.Books;

public class SectionsCreateHandler:IRequestHandler<CreateBookSectionRequest, CreateBookSectionResponse>
{
    private readonly IRepository<Domains.Entities.Documents.BookSections> _repository;
    private readonly IRepository<Domains.Entities.Documents.Books> _repositoryBooks;

    public SectionsCreateHandler(IRepository<Domains.Entities.Documents.BookSections> repository,IRepository<Domains.Entities.Documents.Books> repositoryBooks )
    {
        _repository = repository;
        _repositoryBooks = repositoryBooks;
    }
    public async Task<CreateBookSectionResponse> Handle(CreateBookSectionRequest request, CancellationToken cancellationToken)
    {
        var check = _repositoryBooks.GetWithInclude(x => x.Id == request.BookId).FirstOrDefault();
        if (check != null)
        {
            var result = _repository.Create(new BookSections() 
                {SectionName = request.SectionName, BooksId = request.BookId, Date = DateTime.Now, OwnerId = request.OwnerId});
        }
        
        return new CreateBookSectionResponse() { Message = "succ", SectionName = request.SectionName, Success = true};
    }
}