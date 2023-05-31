using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fractalz.Application.Abstractions;
using Fractalz.Infrastructure.Database.Repositories;
using Fractalz.Application.Domains.Entities.Todo;
using Microsoft.EntityFrameworkCore;
using System;
using Fractalz.Application.Domains.Entities.AdminSettings;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Conference;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Entities.Timetable;
using Fractalz.Application.Domains.Entities.Voice;
//using Fractalz.Infrastructure.Scheduler.Entities;
//using Fractalz.Infrastructure.Scheduler.Repositories;
using MediatR;

namespace Fractalz.Infrastructure.Database
{
    public static class ServiceCollection
    {
        /// <summary>
        /// DataBase
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddInfrastructureDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatContext>(options => 
                options.UseMySql(configuration.GetConnectionString("DBConnection"), 
                new MySqlServerVersion(new Version(5, 6, 45))));

            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<TodoList>, TodoRepository>();
            services.AddTransient<IRepository<Timetable>, TimetableRepository>();
            services.AddTransient<IRepository<Schedule>, ScheduleRepository>();
            services.AddTransient<IRepository<Task>, TaskRepository>();
            services.AddTransient<IRepository<Message>, MessageRepository>();
            services.AddTransient<IRepository<Dialog>, DialogRepository>();
            services.AddTransient<IRepository<File>, FileRepository>();
            services.AddTransient<IRepository<VoiceServer>, VoiceServerRepository>();
            services.AddTransient<IRepository<VoiceRoom>, VoiceRoomRepository>();
            services.AddTransient<IRepository<AdminSetting>, AdminSettingsRepository>();
            services.AddTransient<IRepository<Reaction>, ReactionRepository>();
            services.AddTransient<IRepository<Books>, BooksRepository>();
            services.AddTransient<IRepository<BookSections>, BookSectionsRepository>();
            services.AddTransient<IRepository<BookSheets>, BookSheetsRepository>();
            services.AddTransient<IRepository<DocumentWorkSpace>, WorkSpaceRepository>();
        }
    }
}
