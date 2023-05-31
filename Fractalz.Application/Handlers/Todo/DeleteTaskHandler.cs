using Fractalz.Application.Abstractions;
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
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskRequest, DeleteTaskResponse>
    {
        private readonly IRepository<Domains.Entities.Todo.Task> _repositoryTask;
        public DeleteTaskHandler(IRepository<Domains.Entities.Todo.Task> repositoryTask)
        {
            _repositoryTask = repositoryTask ?? throw new ArgumentNullException(nameof(repositoryTask));
        }
        public async Task<DeleteTaskResponse> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            var task = _repositoryTask.Get(x=> x.Id == request.IdTask).FirstOrDefault();
            if (task != null)
            {
                var result = _repositoryTask.Remove(task);
                if (result == 0)
                { return new DeleteTaskResponse() { Success = false }; }
                else
                { return new DeleteTaskResponse() { Success = true }; }
            }
            else 
            {
                return new DeleteTaskResponse() { Success = true };
            }
        }

    }
}
