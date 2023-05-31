using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Application.Domains.Requests.Todo;
using Fractalz.Application.Domains.Responses.Todo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fractalz.Application.Handlers.Todo
{
    public class GetTodoListHandler : IRequestHandler<GetTodoListRequest, GetTodoListResponse>
    {
        private readonly IRepository<Domains.Entities.Todo.TodoList> _repositoryTodo;
        public GetTodoListHandler(IRepository<Domains.Entities.Todo.TodoList> repositoryTodo)
        {
            _repositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
        }
        public async Task<GetTodoListResponse> Handle(GetTodoListRequest request, CancellationToken cancellationToken)
        {
            if (!request.DateFrom.HasValue)
            { request.DateFrom = DateTime.MinValue; }

            var todo = _repositoryTodo
                .GetWithInclude(x => x.UserId == request.UserId, x => x.Tasks.Where(x=>x.DateCreate >= request.DateFrom))
                .FirstOrDefault();

            if (todo != null)
                return new GetTodoListResponse() { Success = true, TodoList = todo };
            else
                return new GetTodoListResponse() { Success = false };
        }

    }
}
