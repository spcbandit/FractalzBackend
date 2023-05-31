using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Chat;

namespace Fractalz.Application.Domains.Responses.Chat;

public class FileTransferResponse : BasicResponse
{
    public Message CreatedMessage { get; set; }
    public List<File> FilesInfo { get; set; }

}