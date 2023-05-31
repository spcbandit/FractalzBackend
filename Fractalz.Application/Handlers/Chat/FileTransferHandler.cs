using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using Fractalz.Application.Extentions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = Fractalz.Application.Domains.Entities.Chat.File;

namespace Fractalz.Application.Handlers.Chat;

public class FileTransferHandler: IRequestHandler<FileTransferRequest, FileTransferResponse>
{
    private const int _maxLengthFile = 209715200;
    private readonly IRepository<Message> _repositoryMessage;
    private readonly IRepository<Dialog> _repositoryDialog;
    private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
    private readonly ILinkedEventService _linkedEventService;
    private readonly IMapper _mapper;

    public FileTransferHandler(IRepository<Message> repositoryMessage,IRepository<Dialog> repositoryTodo,ILinkedEventService linkedEventService,IMapper mapper,IRepository<Domains.Entities.Profile.User> repositoryUser)
    {
        _repositoryMessage = repositoryMessage ?? throw new ArgumentNullException(nameof(repositoryMessage));
         _repositoryDialog = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
         _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        _linkedEventService = linkedEventService ?? throw new ArgumentNullException(nameof(linkedEventService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<FileTransferResponse> Handle(FileTransferRequest request, CancellationToken cancellationToken)
    {
        var message = new Message
            {
                DialogId = request.DialogId,
                Text = request.Message,
                IdSender = request.IdSender,
                Created = DateTime.Now,
                IsDelivered = false,
                IsOnRead = false,
                IsUpdate = false,
            };
            
            // Загрузка файлов в каталог
            if (request.Files!= null &&
                request.Files.Count > 0)
            {
                if (request.Files.Any(file => file.Length > _maxLengthFile))
                { return new FileTransferResponse() { Success = false, Message = "The maximum file size is 200Mb" }; }

                var files = new List<File>();
                var uploadPath = $"{Environment.CurrentDirectory}{ConstResource.DefaultFileUploadPath}{request.DialogId}/";
                
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in request.Files)
                {
                    var path = uploadPath + file.FileName;

                    using (var fileStream = new FileStream(path, FileMode.Create))
                        await file.CopyToAsync(fileStream);

                    files.Add(new File
                    {
                        ByteLength = file.Length,
                        Extension = Path.GetExtension(file.FileName),
                        Path = path,
                        FileName = file.FileName
                    });
                }
                message.File = files;
            }

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
                
                _linkedEventService.InvokeGetMessage(mapMessage);
                
                if(dialog != null)
                    _linkedEventService.InvokeDialogUpdate(mapDialog);
                
                return new FileTransferResponse() { Success = true, CreatedMessage = message };
            }
            else
                return new FileTransferResponse() { Success = false, Message = "Message not created" };
    }
}