using System;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Requests.Books;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Handlers.Books;

public class BooksCreateHandler:IRequestHandler<CreateBookRequest, CreateBooksResponse>
{
    private readonly IRepository<Domains.Entities.Documents.Books> _repository;

    public BooksCreateHandler(IRepository<Domains.Entities.Documents.Books> repository)
    {
        _repository = repository;
    }

    public async Task<CreateBooksResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var result = _repository.Create(new Domains.Entities.Documents.Books()
            {DateTime = DateTime.Now.ToLocalTime(), BookName = request.BookName, About = request.About, Color = request.Color , OwnerId = request.OwnerId});
        
            return new CreateBooksResponse() 
                { Success = true, Message = "message", BookName = request.BookName, About = request.About, Color = request.Color, OwnerId = request.OwnerId};
    }
}