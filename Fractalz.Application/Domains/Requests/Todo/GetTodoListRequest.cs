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
    public class GetTodoListRequest: IRequest<GetTodoListResponse>
    {
        [Required]
        public Guid UserId { get; set; }
        public DateTime? DateFrom { get; set; }
    }
}
