using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Application.Domains.Entities;
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
    public class UpdateStatusTaskHandler : IRequestHandler<UpdateStatusTaskRequest, UpdateStatusTaskResponse>
    {
        private readonly IRepository<Domains.Entities.Todo.Task> _repositoryTask;
        public UpdateStatusTaskHandler( IRepository<Domains.Entities.Todo.Task> repositoryTask)
        {
            _repositoryTask = repositoryTask ?? throw new ArgumentNullException(nameof(repositoryTask));
        }
        public async Task<UpdateStatusTaskResponse> Handle(UpdateStatusTaskRequest request, CancellationToken cancellationToken)
        {
            var task = _repositoryTask.Get(x=> x.Id == request.TodoId).FirstOrDefault();
            task.IsCompleted = request.Completed;
            var result = _repositoryTask.Update(task);
            if (result == 0)
            { return new UpdateStatusTaskResponse() { Success = false }; }
            else
            { return new UpdateStatusTaskResponse() { Success = true }; }
        }
    }
}
