using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Handlers.User;

public class CodeValidHandler:IRequestHandler<CodeValidRequest,CodeValidResponse>
{
    private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
    public CodeValidHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
    {
        _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
    }
    public async Task<CodeValidResponse> Handle(CodeValidRequest request, CancellationToken cancellationToken)
    {
        var email = _repositoryUser.GetWithInclude(x => x.Email == request.Email).FirstOrDefault();
        if (email != null)
        {
            if (email.AuthCode == request.AuthCode)
            {
                email.IsEmailConfirmed += 1;
                var conf = _repositoryUser.Update(email);
                return new CodeValidResponse() { Success = true, Message = "Code valide" };
            }
            
        }
        return new CodeValidResponse() { Success = false, Message = "Code invalide" };
    }
}