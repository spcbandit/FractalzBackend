using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Domains.Requests.User;

public class CodeGenerateRequest:IRequest<CodeGenerateResponse>
{
    public string Email {get; set;}

    public bool GenRequest { get; set; }
}