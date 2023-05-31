using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Chat
{
    public class CreateMessageRequest : IRequest<CreateMessageResponse>
    {
        public Guid UserId { get; set; }
        public Guid DialogId { get; set; }
        public string Message { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
