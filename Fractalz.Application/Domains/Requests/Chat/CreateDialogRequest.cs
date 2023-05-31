using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Chat
{
    public class CreateDialogRequest : IRequest<CreateDialogResponse>
    {
        public List<Guid> UsersId { get; set; }
    }
}
