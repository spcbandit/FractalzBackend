using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Application.Domains.Responses.Todo;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Todo
{
    public class CreateTaskRequest : IRequest<CreateTaskResponse>
    {
        [Required]
        public Guid TodoListId { get; set; }

        [Required]
        public string Header { get; set; }

        public string About { get; set; }

        [Required]
        public DateTime TimeStart { get; set; }

        [Required]
        public int DurationInMinute { get; set; }

        public DateTime DateCreate { get; set; } = DateTime.Now;

    }
}
