using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Chat
{
    public class UpdateMessageRequest : IRequest<UpdateMessageResponse>
    {
        public Guid MessagId { get; set; }
        public string Text { get; set; }
    }
}
