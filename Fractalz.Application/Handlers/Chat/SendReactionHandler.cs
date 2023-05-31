using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Requests.AdminSetting;
using Fractalz.Application.Domains.Responses.AdminSetting;
using MediatR;

namespace Fractalz.Application.Handlers.Chat;

    public class SendReactionHandler : IRequestHandler<SendReactionRequest, SendReactionResponse>
    {
        private readonly IRepository<Domains.Entities.Chat.Reaction> _repositoryReaction;
        public SendReactionHandler(IRepository<Domains.Entities.Chat.Reaction> repositoryReaction)
        {
            _repositoryReaction = repositoryReaction ?? throw new ArgumentNullException(nameof(repositoryReaction));
        }

        public async Task<SendReactionResponse> Handle(SendReactionRequest request, CancellationToken cancellationToken)
        {
            if (request.IdMessage == Guid.Empty)
            { return new SendReactionResponse() { Success = false, Message = "Id Message can not be 0" }; }

            if (request.IdUser == Guid.Empty)
            { return new SendReactionResponse() { Success = false, Message = "Id User can not be 0" }; }
            
            var reaction = _repositoryReaction.Get(i => i.UserId == request.IdUser && i.MessageId == request.IdMessage).OrderByDescending((setting => setting.DateTime)).FirstOrDefault();
            
            
            if (reaction != null)
            { return new SendReactionResponse() { Success = false, Message = "You may put only one reaction" };}
            
            
            reaction = new Reaction();
    
            reaction.DateTime = DateTime.Now;
            reaction.EmojiType = request.EmojiType;
            reaction.UserId = request.IdUser;
            reaction.MessageId = request.IdMessage;
            var resp = _repositoryReaction.Create(reaction);
            if (resp != 0)
            {
                return new SendReactionResponse() { Success = true};
            }
            else
            {
                return new SendReactionResponse() { Success = false, Message = "AdminSetting not create" };
            }
            
            /*
            var resp = _repositoryReaction.GetWithInclude(x => x.Id == request.IdMessage, x => x.Reactions).FirstOrDefault();

            if (resp != null)
            {
                resp.Reactions.Add(new Domains.Entities.Chat.Reaction 
                {
                    DateTime = DateTime.Now,
                    EmojiType = request.EmojiType,
                    UserId = request.IdUser
                });

            }
            var resultTransaction =  _repositoryReaction.Update(resp);

            if (resultTransaction > 0)
                return new SendReactionResponse() { Success = true };
            else
                return new SendReactionResponse() { Success = false, Message = "Don have changes on this message" };*/
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }

