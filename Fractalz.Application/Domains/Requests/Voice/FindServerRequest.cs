using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Domains.Requests.Voice
{
    public class FindServerRequest: IRequest<FindServerResponse>
    {
        public string Name { get; set; }
    }
}