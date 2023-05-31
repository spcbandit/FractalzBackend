using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Requests.Books;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Handlers.Books;

public class BookDeleteHandler:IRequestHandler<DeleteBookRequest, DeleteBookResponse>
{
    private readonly IRepository<Domains.Entities.Documents.Books> _repository;
    private readonly IRepository<BookSections> _sectionRepository;
    public BookDeleteHandler(IRepository<Domains.Entities.Documents.Books> repository,
        IRepository<BookSections> sectionRepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _sectionRepository = sectionRepository ?? throw new ArgumentNullException(nameof(sectionRepository));
    }

    public async Task<DeleteBookResponse> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        var res = _repository.GetWithInclude(x=>x.Id==request.BookId, x =>x.Sections).FirstOrDefault();
        //var ser = _sectionRepository.GetWithInclude(x => x.BookId == request.BookId);
        if (res!=null)
        {
            var delete = _repository.Remove(res);
        }

        return new DeleteBookResponse() { Success = true, Message = "deleted"};
    }
}