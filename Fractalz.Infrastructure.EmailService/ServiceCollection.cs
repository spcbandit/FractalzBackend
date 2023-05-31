using Fractalz.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Fractalz.Application.Domains.Options;

namespace Fractalz.Infrastructure.EmailService
{
    public static class ServiceCollection
    {
        /// <summary>
        /// EmailService
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddInfrastructureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailServiceOptions>(configuration.GetSection("EmailServiceOptions"));
            services.AddTransient<IEmailService, Adaptors.EmailService>();
        }
    }
}
