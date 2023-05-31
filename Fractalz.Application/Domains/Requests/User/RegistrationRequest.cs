using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Application.Domains.Responses.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Requests.User
{
    public class RegistrationRequest : IRequest<RegistrationResponse>
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}