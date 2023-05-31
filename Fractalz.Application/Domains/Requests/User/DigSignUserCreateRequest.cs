using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Fractalz.Application.Domains.Responses.User;
using MediatR;

namespace Fractalz.Application.Domains.Requests.User;

public class DigSignUserCreateRequest: IRequest<DigSignUserCreateResponse>
{
    [Required]
    public string OrganizationName { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required] 
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Number { get; set; }
    [Required]
    public string Server { get; set; }
    [Required]
    public string Port { get; set; }
    [NotNull]
    public string Proxy { get; set; }

}