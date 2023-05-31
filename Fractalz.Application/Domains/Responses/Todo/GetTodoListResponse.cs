using Fractalz.Application.Domains.Entities.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Responses.Todo
{
    public class GetTodoListResponse : BasicResponse
    {
        public TodoList TodoList{ get; set; }
    }
}
