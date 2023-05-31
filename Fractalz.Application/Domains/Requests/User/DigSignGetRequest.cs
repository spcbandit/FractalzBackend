using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Domains.Requests.User;

public class DigSignGetRequest: IRequest<DigSignGetResponse>
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string OrganizationName { get; set; }
}