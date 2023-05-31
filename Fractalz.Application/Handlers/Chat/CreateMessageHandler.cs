using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Extentions;
using Microsoft.AspNetCore.Http;
using File = Fractalz.Application.Domains.Entities.Chat.File;

namespace Fractalz.Application.Handlers.Chat
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageRequest, CreateMessageResponse>
    {
        private const int _maxLengthFile = 209715200;
        private readonly IRepository<Dialog> _repositoryDialog;
        private readonly IRepository<Message> _repositoryMessage;
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly ILinkedEventService _linkedEventService;
        
        private readonly IMapper _mapper;
        public CreateMessageHandler(IRepository<Dialog> repositoryTodo, IRepository<Message> repositoryMessage,
            ILinkedEventService linkedEventService, IMapper mapper, IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryDialog = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
            _repositoryMessage = repositoryMessage ?? throw new ArgumentNullException(nameof(repositoryMessage));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _linkedEventService = linkedEventService ?? throw new ArgumentNullException(nameof(linkedEventService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CreateMessageResponse> Handle(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            { return new CreateMessageResponse() { Success = false, Message = "UserId cannot be 0" }; }

            if (request.DialogId == Guid.Empty)
            { return new CreateMessageResponse() { Success = false, Message = "Dialog cannot be 0" }; }

            if (string.IsNullOrEmpty(request.Message))
            { return new CreateMessageResponse() { Success = false, Message = "Message cannot be null or empty" }; }

            var message = new Message
            {
                DialogId = request.DialogId,
                IdSender = request.UserId,
                Text = request.Message,
                Created = DateTime.Now,
                IsDelivered = false,
                IsOnRead = false,
                IsUpdate = false,
            };
            
            var res = _repositoryMessage.Create(message);

            if (res != 0)
            {
                var dialog = _repositoryDialog.GetWithInclude(x => x.Id == request.DialogId,
                        x => x.Users)
                    .FirstOrDefault();
                dialog.LastMessage = message.Text;
                dialog.DateSend = message.Created.ToBeautyTime();
                _repositoryDialog.Update(dialog);
                
                var mapDialog = _mapper.Map<DialogsMappedDto>(dialog);
                
                var mapMessage = _mapper.Map<MessageMappedDto>(message);

                var user = _repositoryUser.FindById(message.IdSender);
                
                if (user.Name != null) 
                    mapMessage.NameSender = $"{user.Patro} {user.Name} {user.Surname}";
                else 
                    mapMessage.NameSender = user.Login;
                
                mapDialog.Name = mapMessage.NameSender;

                mapDialog.UserId = mapMessage.IdSender;
                
                _linkedEventService.InvokeGetMessage(mapMessage);
                
                if(dialog != null)
                    _linkedEventService.InvokeDialogUpdate(mapDialog);
                
                return new CreateMessageResponse() { Success = true, CreatedMessage = message };
            }
            else
                return new CreateMessageResponse() { Success = false, Message = "Message not created" };
        }
    }
}
