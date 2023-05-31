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

public class UpdateBookSheetsHandler: IRequestHandler<UpdateBookSheetsRequest, UpdateBookSheetsResponse>
{
    private readonly IRepository<BookSheets> _repository;
    public UpdateBookSheetsHandler(IRepository<BookSheets> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UpdateBookSheetsResponse> Handle(UpdateBookSheetsRequest request, CancellationToken cancellationToken)
    {
        var update = _repository.GetWithInclude(x => x.Id == request.Id).FirstOrDefault();
        if (update != null)
        {
            update.Text = request.Text;
            var result = _repository.Update(update);
        }

        return new UpdateBookSheetsResponse() { Text = request.Text, Success = true};
    }
}