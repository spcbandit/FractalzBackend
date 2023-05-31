using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Responses.Todo
{
    public class CreateTaskResponse : BasicResponse
    {
        public Guid IdTask { get; set; }
    }
}
