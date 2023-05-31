using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Entities.Todo;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Entities.AdminSettings;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Conference;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Entities.Timetable;

namespace Fractalz.Infrastructure.Database.Contexts
{
    public class ChatContext : DbContext
    {
        public virtual DbSet<Dialog> Dialogs { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TodoList> TodoLists { get; set; }
        public virtual DbSet<Timetable> Timetable { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<DocumentWorkSpace> Document { get; set; }
        public virtual DbSet<AdminSetting> AdminSettings { get; set; }
        public virtual DbSet<Application.Domains.Entities.Todo.Task> Tasks { get; set; }

        public ChatContext() { }
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
