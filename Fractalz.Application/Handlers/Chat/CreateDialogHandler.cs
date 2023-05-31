using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fractalz.Application.Handlers.Chat
{
    public class CreateDialogHandler : IRequestHandler<CreateDialogRequest, CreateDialogResponse>
    {
        private readonly IRepository<Dialog> _repositoryDialog;
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        
        /// <summary>
        /// CreateDialogHandler
        /// </summary>
        /// <param name="repositoryTodo"></param>
        /// <param name="repositoryUser"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CreateDialogHandler(IRepository<Dialog> repositoryTodo, IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryDialog = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        }

        public async Task<CreateDialogResponse> Handle(CreateDialogRequest request, CancellationToken cancellationToken)
        {
            //проверка на колличество пользователей
            if (request.UsersId.Count < 1)
            { return new CreateDialogResponse() { Success = false, Message = "UserId must be 1 or more" }; }
            // Создание нового диалога
            var users = _repositoryUser.Get(user => request.UsersId.Contains(user.Id)).ToList();
            var dialog = new Dialog
            {Created = DateTime.Now,};
            _repositoryDialog.Create(dialog);
            foreach (var user in users)
            {
                dialog.Users.Add(user);
            }

            var res = _repositoryDialog.Update(dialog);
            
            if (res != 0)
                return new CreateDialogResponse() { Success = true, Dialog = dialog };
            else
                return new CreateDialogResponse() { Success = false, Message = "Dialog not created" };
        }
    }
}
