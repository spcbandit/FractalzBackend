using Fractalz.Application.Abstractions;
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
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Extentions;

namespace Fractalz.Application.Handlers.Chat
{
    public class GetMessageHistoryHandler : IRequestHandler<GetMessageHistoryRequest, GetMessageHistoryResponse>
    {
        private Dictionary<Guid, string> _usersNames;
        private readonly IRepository<Message> _repositoryMessage;
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly IRepository<Dialog> _repositoryDialod; 
        private readonly IMapper _mapper;
        public GetMessageHistoryHandler(IRepository<Message> repositoryTodo, IMapper mapper,
            IRepository<Domains.Entities.Profile.User> repositoryUser, IRepository<Dialog> repositoryDialod)
        {
            _usersNames = new Dictionary<Guid, string>();
            _repositoryDialod = repositoryDialod ?? throw new ArgumentNullException(nameof(repositoryDialod));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _repositoryMessage = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetMessageHistoryResponse> Handle(GetMessageHistoryRequest request, CancellationToken cancellationToken)
        {
            if (request.IdDialog == Guid.Empty)
            { return new GetMessageHistoryResponse() { Success = false, Message = "Id Dialog can not be 0" }; }

            var messages = _repositoryMessage
                .GetWithInclude(message => message.DialogId == request.IdDialog,
                        message => message.File, 
                        message => message.Reactions)
                .Take(request.CountMessage)
                .ToList();
            var mapMessages = _mapper.Map<List<MessageMappedDto>>(messages);

            if (mapMessages.Count == 0)
            { return new GetMessageHistoryResponse(){Success = true, Message = "У вас еще нет сообщений"}; }

            var dialog = _repositoryDialod.GetWithInclude(dialog => dialog.Id == request.IdDialog, users => users.Users).FirstOrDefault();

            foreach (var user in dialog.Users)
            {
                if (user.Name != null) 
                    _usersNames.Add(user.Id,$"{user.Patro} {user.Name} {user.Surname}");
                else 
                    _usersNames.Add(user.Id,user.Login);
            }

            var unreadMessages = messages.Where(x => x.IdSender != request.IdUser).ToList();
            foreach (var unreadMessage in unreadMessages)
            {
                unreadMessage.IsOnRead = true;
                _repositoryMessage.Update(unreadMessage);
            }

            
            foreach (var message in mapMessages)
            {
                string res;
                _usersNames.TryGetValue(message.IdSender, out res);
                message.NameSender = res;
            }
            
            if (mapMessages != null && messages.Count > 0)
                return new GetMessageHistoryResponse() { Success = true, Messages = mapMessages};
            else
                return new GetMessageHistoryResponse() { Success = false, Message = "Message list was null or count this list was equal 0" };
        }

    }

}
