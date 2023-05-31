using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fractalz.Application.Handlers.Chat
{
    public class UpdateMessageHandler
    {
        private readonly IRepository<Domains.Entities.Chat.Message> _repositoryMessage;
        public UpdateMessageHandler(IRepository<Domains.Entities.Chat.Message> repositoryMessage)
        {
            _repositoryMessage = repositoryMessage ?? throw new ArgumentNullException(nameof(repositoryMessage));
        }

        public async Task<UpdateMessageResponse> Handle(UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            if (request.MessagId == Guid.Empty)
            { return new UpdateMessageResponse() { Success = false, Message = "Id Message can not be 0" }; }

            var res = _repositoryMessage.Get(x => x.Id == request.MessagId).FirstOrDefault();
            res.Text = request.Text;
            var resultTransaction = _repositoryMessage.Update(res);

            if (resultTransaction != 0)
            { return new UpdateMessageResponse() { Success = true }; }
            else 
            { return new UpdateMessageResponse() { Success = false, Message = "Message not updated" }; }
        }
    }
}
