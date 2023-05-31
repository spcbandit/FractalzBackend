using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Requests.Books.Update;
using Fractalz.Application.Domains.Responses.Books.Update;
using MediatR;

namespace Fractalz.Application.Handlers.Books.Update;

public class UpdateBookSectionHandler:IRequestHandler<UpdateBookSectionRequest, UpdateBookSectionResponse>
{
    private readonly IRepository<BookSections> _repository;
    public UpdateBookSectionHandler(IRepository<BookSections> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UpdateBookSectionResponse> Handle(UpdateBookSectionRequest request, CancellationToken cancellationToken)
    {
        var check = _repository.GetWithInclude(x => x.Id == request.SectionId).FirstOrDefault();
        if (check != null)
        {
            check.SectionName = request.SectionName;
            var update = _repository.Update(check);
        }

        return new UpdateBookSectionResponse() { Success = true, Message = "Update success"};
    }
}