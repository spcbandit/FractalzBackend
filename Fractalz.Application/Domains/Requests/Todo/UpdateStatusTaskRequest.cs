using Fractalz.Application.Domains.Responses.Todo;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Todo
{
    public class UpdateStatusTaskRequest : IRequest<UpdateStatusTaskResponse>
    {
        [Required]
        public Guid TodoId { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}
