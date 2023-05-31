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
    public class DeleteMessageHandler
    {
        private readonly IRepository<Domains.Entities.Chat.Message> _repositoryMessage;
        public DeleteMessageHandler(IRepository<Domains.Entities.Chat.Message> repositoryMessage)
        {
            _repositoryMessage = repositoryMessage ?? throw new ArgumentNullException(nameof(repositoryMessage));
        }

        public async Task<DeleteMessageResponse> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            if (request.MessageId == Guid.Empty)
            { return new DeleteMessageResponse() { Success = false, Message = "Id Message can not be 0" }; }

            var res = _repositoryMessage.GetWithInclude(x => x.Id == request.MessageId, x=>x.File, x=>x.Reactions).FirstOrDefault();
            var resultTransaction = _repositoryMessage.Remove(res);

            if (resultTransaction != 0)
            { return new DeleteMessageResponse() { Success = true }; }
            else
            { return new DeleteMessageResponse() { Success = false, Message = "Message not delete" }; }
        }
    }
}
