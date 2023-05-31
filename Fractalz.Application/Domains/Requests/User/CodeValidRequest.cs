using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Domains.Requests.User;

public class CodeValidRequest:IRequest<CodeValidResponse>
{
    public string AuthCode { get; set; }
    public string Email { get; set; }
}