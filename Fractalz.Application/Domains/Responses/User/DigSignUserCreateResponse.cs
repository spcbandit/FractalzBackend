using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Fractalz.Application.Domains.Responses.User;

public class DigSignUserCreateResponse:BasicResponse
{
   public FileStreamResult FileStream{ get; set; }
   
}