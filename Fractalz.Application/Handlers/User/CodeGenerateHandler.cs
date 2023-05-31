using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Handlers.User;

public class CodeGenerateHandler:IRequestHandler<CodeGenerateRequest,CodeGenerateResponse>
{
    private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
    private readonly IEmailService _emailService;
    public string Theme = "Authentification code";
    public CodeGenerateHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, IEmailService emailService)
    {
        _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        
    }
    public async Task<CodeGenerateResponse> Handle(CodeGenerateRequest request, CancellationToken cancellationToken)
    {
        Random generator = new Random();
        string authCode = generator.Next(0, 1000000).ToString("D6");
        var email = _repositoryUser.GetWithInclude(x => x.Email == request.Email).FirstOrDefault();
        if (email != null)
        {
            email.AuthCode = authCode;
            var codeUpdate = _repositoryUser.Update(email);
            _emailService.SendEmail(email.Email, "", MessageResource.MessageEmailRecource + authCode);
            // _codeSender.SendEmail(request.Email, authCode, Theme);
        }

        if (!(authCode == null))
            return new CodeGenerateResponse() { Success = true, Message = "code generated" };
        else
        { return new CodeGenerateResponse() { Success = false, Message = MessageResource.ServerFailed }; } 
    }
}