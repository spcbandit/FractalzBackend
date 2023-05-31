using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Extentions;

namespace Fractalz.Application.Handlers.Chat
{
    public class GetDialogsHandler : IRequestHandler<GetListDialogsRequest, GetListDialogsResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly IRepository<Dialog> _repositoryDialog;
        private readonly IRepository<Message> _repositoryMessage;
        public GetDialogsHandler(IRepository<Message> repositoryMessage,
            IRepository<Domains.Entities.Profile.User> repositoryUser, IRepository<Dialog> repositoryDialog)
        {
            _repositoryMessage = repositoryMessage ?? throw new ArgumentNullException(nameof(repositoryMessage));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _repositoryDialog = repositoryDialog ?? throw new ArgumentNullException(nameof(repositoryDialog));
        }

        public async Task<GetListDialogsResponse> Handle(GetListDialogsRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            { return new GetListDialogsResponse() { Success = false, Message = "UserId can not be 0" }; }

            var user = _repositoryUser.GetWithInclude(user => user.Id == request.UserId, 
                    y => y.Dialogs)
                .FirstOrDefault();
                
            var dialogs = new List<DialogsMappedDto>();
            
            foreach (var dialogMapped in user.Dialogs)
            {
                var dialog = new DialogsMappedDto();
                
                //поиск диалога в бд
                var findDialog = _repositoryDialog.GetWithInclude(x => x.Id == dialogMapped.Id, 
                        x => x.Users)
                    .FirstOrDefault();
                dialog.Id = findDialog.Id;
                var dialogUser = findDialog.Users.LastOrDefault(x => x.Id != request.UserId);
                
                dialog.Name = "Закладки";
                dialog.UserId = request.UserId;
                
                if (dialogUser != null)
                {
                    dialog.Name = dialogUser.Login;
                    dialog.UserId = dialogUser.Id;
                    if (dialogUser.Name != null)
                    {
                        dialog.Name = $"{dialogUser.Patro} {dialogUser.Name} {dialogUser.Surname}";
                    } 
                }

                dialog.LastMessage = findDialog.LastMessage;
                dialog.DateSend = findDialog.DateSend;
                
                //поиск не прочитанных сообщений в бд
                var newMessade = _repositoryMessage.Get(x => x.DialogId == dialogMapped.Id).Where(x=>x.IsOnRead == false && x.IdSender != request.UserId).ToList();
                dialog.CountUnReadMessage = newMessade.Count;
                if (dialog.LastMessage == null)
                {
                    dialog.LastMessage = "У вас еще нет сообщений!";
                    dialog.DateSend = null;
                }
                else
                {
                    var lastMessade = _repositoryMessage.Get(x => x.DialogId == dialogMapped.Id).OrderByDescending(x=>x.Created).FirstOrDefault();
                    dialog.LastMessage = lastMessade.Text;
                    dialog.DateSend = lastMessade.Created.ToBeautyTime();
                }
                dialogs.Add(dialog);
            }
            
            if (dialogs.Count != 0)
                return new GetListDialogsResponse() { Success = true, Dialogs = dialogs };
            else
                return new GetListDialogsResponse() { Success = false, Message = "Message list was null or count this list was equal 0" };
        }
    }
}
