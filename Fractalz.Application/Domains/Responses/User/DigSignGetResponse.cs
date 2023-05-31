using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Fractalz.Application.Domains.Responses.User;

public class DigSignGetResponse:BasicResponse
{
    public FileStreamResult FileStreamResult { get; set; }
}