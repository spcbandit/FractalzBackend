using Fractalz.Application.Domains.Responses.Chat;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Chat
{
    public class GetMessageHistoryRequest : IRequest<GetMessageHistoryResponse>
    {
        public Guid IdDialog { get; set; }
        public DateTime DateFrom { get; set; }
        public int CountMessage { get; set; }
        public Guid IdUser { get; set; }
    }
}
