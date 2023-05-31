using Fractalz.Application.Domains.Responses.Chat;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Chat
{
    public class FindUserRequest : IRequest<FindUserResponse>
    {
        public string FindStr { get; set; }
    }
}