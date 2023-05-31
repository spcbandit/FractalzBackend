using System;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fractalz.Infrastructure.LinkedEvent
{
    public static class ServiceCollection
    {
        /// <summary>
        /// LinkedEventService
        /// </summary>
        /// <param name="services"></param>
        public static void AddInfrastructureLinkedEventService(this IServiceCollection services)
        {
            services.AddSingleton<ILinkedEventService, LinkedEventService>();
        }
    }
}