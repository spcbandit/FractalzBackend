using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;

using MediatR;

using File = Fractalz.Application.Domains.Entities.Chat.File;

namespace Fractalz.Application.Handlers.Chat
{
     public class DownloadFileHandler : IRequestHandler<DownloadFileRequest, DownloadFileResponse>
    {
        private readonly IRepository<File> _repositoryFile;
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        public DownloadFileHandler(IRepository<File> repositoryFile, IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryFile = repositoryFile ?? throw new ArgumentNullException(nameof(repositoryFile));
        }

        public async Task<DownloadFileResponse> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            var file = _repositoryFile.Get(file => file.Id == request.FileId).FirstOrDefault();
            byte[] fileBytes = System.IO.File.ReadAllBytes(
                Environment.CurrentDirectory + $"/uploadedFiles/{request.DialogId}/{file.FileName}");
            
            var memoryStream = new MemoryStream(fileBytes);

            if (memoryStream.Length != 0)
                return new DownloadFileResponse() 
                {
                    Success = true, 
                    FileStream = new Microsoft.AspNetCore.Mvc.FileStreamResult(memoryStream, "application/octet-stream"){FileDownloadName = file.FileName}
                };
            else
                return new DownloadFileResponse() { Success = false, Message = "File not created" };
        }
    }
}
