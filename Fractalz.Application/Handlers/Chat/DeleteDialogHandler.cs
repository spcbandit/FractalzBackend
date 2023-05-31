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
    public class DeleteDialogHandler
    {
        private readonly IRepository<Domains.Entities.Chat.Dialog> _repositoryDialog;
        public DeleteDialogHandler(IRepository<Domains.Entities.Chat.Dialog> repositoryDialog)
        {
            _repositoryDialog = repositoryDialog ?? throw new ArgumentNullException(nameof(repositoryDialog));
        }

        public async Task<DeleteDialogResponse> Handle(DeleteDialogRequest request, CancellationToken cancellationToken)
        {
            if (request.DialogId == Guid.Empty)
            { return new DeleteDialogResponse() { Success = false, Message = "Id Message can not be 0" }; }

            var res = _repositoryDialog.GetWithInclude(x => x.Id == request.DialogId, x=>x.Messages).FirstOrDefault();
            var resultTransaction = _repositoryDialog.Remove(res);

            if (resultTransaction != 0)
            { return new DeleteDialogResponse() { Success = true }; }
            else
            { return new DeleteDialogResponse() { Success = false, Message = "Dialog not delete" }; }
        }
    }
}
