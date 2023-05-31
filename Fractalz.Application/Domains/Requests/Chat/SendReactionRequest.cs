using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.Chat;

public class SendReactionRequest : IRequest<SendReactionResponse>
{
    public Guid IdMessage { get; set; }
    public Guid IdUser { get; set; }
    public EmojiType EmojiType { get; set; }
}