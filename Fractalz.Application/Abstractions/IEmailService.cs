using System.Threading.Tasks;

namespace Fractalz.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
        
    }
}