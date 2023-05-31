using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Fractalz.Application.Domains.Responses.Chat;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fractalz.Application.Domains.Requests.Chat;

public class FileTransferRequest : IRequest<FileTransferResponse>
{
    public Guid DialogId { get; set; }
    public string Message { get; set; }
    public Guid IdSender { get; set; }
    public List<IFormFile> Files { get; set; }
}