using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Responses.Chat
{
    public class DownloadFileResponse : BasicResponse
    {
        public FileStreamResult FileStream { get; set; }
    }
}