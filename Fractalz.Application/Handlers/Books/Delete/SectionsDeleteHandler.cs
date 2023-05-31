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

public class SectionsDeleteHandler:IRequestHandler<DeleteSectionRequest,DeleteSectionsResponse >
{
    private readonly IRepository<BookSections> _repository;
    private readonly IRepository<BookSheets> _sheetRepository;
    public SectionsDeleteHandler(IRepository<BookSections> repository, IRepository<BookSheets> sheetRepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _sheetRepository = sheetRepository ?? throw new ArgumentNullException(nameof(sheetRepository));
    }

    public async Task<DeleteSectionsResponse> Handle(DeleteSectionRequest request, CancellationToken cancellationToken)
    {
        var res = _repository.GetWithInclude(x=>x.Id==request.Id, x => x.BookSheets).FirstOrDefault();
        if (res!=null)
        {
            var delete = _repository.Remove(res);
        }

        return new DeleteSectionsResponse() { Success = true, Message = "deleted"};
    }
}