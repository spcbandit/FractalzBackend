using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities;
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
    internal class CreateTaskHandler : IRequestHandler<CreateTaskRequest, CreateTaskResponse>
    {
        private readonly IRepository<Domains.Entities.Todo.Task> _repositoryTask;
        public CreateTaskHandler(IRepository<Domains.Entities.Todo.Task> repositoryTask)
        {
            _repositoryTask = repositoryTask ?? throw new ArgumentNullException(nameof(repositoryTask));
        }
        public async Task<CreateTaskResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var task = new Domains.Entities.Todo.Task()
            {
                TimeStart = request.TimeStart,
                IsCompleted = false,
                Header = request.Header,
                DurationInMinute = request.DurationInMinute,
                DateCreate = request.DateCreate,
                About = request.About,
                TodoListId = request.TodoListId
            };
            var result = _repositoryTask.Create(task);

            if (result == 0)
            { return new CreateTaskResponse() { Message = MessageResource.UserNotFound, Success = false }; }
            else 
            { return new CreateTaskResponse() { Success = true, IdTask = task.Id }; }
        }

    }
}