using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Books.Update;
using Fractalz.Application.Domains.Responses.Books.Update;
using MediatR;

namespace Fractalz.Application.Handlers.Books.Update;

public class UpdateBookHandler:IRequestHandler<UpdateBookRequest, UpdateBookResponse>
{
    public IRepository<Domains.Entities.Documents.Books> _repository;
    public UpdateBookHandler(IRepository<Domains.Entities.Documents.Books> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var check = _repository.GetWithInclude(x => x.Id == request.BookId).FirstOrDefault();
        if (check != null)
        {
            check.BookName = request.BookName;
            var update = _repository.Update(check);
        }

        return new UpdateBookResponse() { Success = true, Message = "Update book"};
    }
}