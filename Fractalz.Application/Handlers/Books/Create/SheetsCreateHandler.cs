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

public class SheetsCreateHandler:IRequestHandler<CreateBookSheetsRequest, CreateBookSheetsResponse>
{
    private readonly IRepository<BookSheets> _repository;
    private readonly IRepository<BookSections> _repositorySection;
    public SheetsCreateHandler(IRepository<BookSheets> repository,IRepository<BookSections> repositorySection)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _repositorySection = repositorySection ?? throw new ArgumentNullException(nameof(repositorySection));
    }

    public async Task<CreateBookSheetsResponse> Handle(CreateBookSheetsRequest request, CancellationToken cancellationToken)
    {
        var check = _repositorySection.GetWithInclude(x => x.Id == request.SectionId).FirstOrDefault();
        if (check != null)
        {
            var get = _repository.GetWithInclude(x => x.BookSectionsId == request.SectionId).FirstOrDefault();
            if (get != null)
            {
                return new CreateBookSheetsResponse() {Success = true, Message = "sheet already created", BookSheets = get};
            }
            else
            {
                var result = _repository.Create(new BookSheets(){BookSectionsId = request.SectionId, Date = DateTime.Now});
                var getAfterCreate = _repository.GetWithInclude(x => x.BookSectionsId == request.SectionId).FirstOrDefault();
                return new CreateBookSheetsResponse() {Success = true, Message = "sheet created", BookSheets = getAfterCreate};
            }
        }
        
        return new CreateBookSheetsResponse() {Success = false, Message = "sheet not created"};
    }
}