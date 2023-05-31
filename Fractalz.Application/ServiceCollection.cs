using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains;
using Fractalz.Application.Handlers;
using Fractalz.Application.Mapping;

namespace Fractalz.Application
{
    public static class ServiceCollection
    {
        /// <summary>
        /// AddApplication
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddMediatR(assembly);
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
