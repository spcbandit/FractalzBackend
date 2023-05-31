using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Chat
{
    public class DeleteDialogRequest : IRequest<DeleteDialogResponse>
    {
        public Guid DialogId { get; set; }
    }
}
