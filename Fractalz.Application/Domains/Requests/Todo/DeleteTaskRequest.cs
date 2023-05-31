using Fractalz.Application.Domains.Responses.Todo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Todo
{
    public class DeleteTaskRequest : IRequest<DeleteTaskResponse>
    {
        public Guid IdTask { get; set; }
    }
}
