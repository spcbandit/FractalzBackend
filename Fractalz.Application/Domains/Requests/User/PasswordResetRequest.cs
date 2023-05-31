using System.ComponentModel.DataAnnotations;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Domains.Requests.User
{
    public class PasswordResetRequest: IRequest<PasswordResetResponse>
    { 
        public string existEmail { get; set; }
        public string Password { get; set; }
    }
}