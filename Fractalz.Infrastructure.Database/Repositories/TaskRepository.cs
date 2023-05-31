using Fractalz.Application.Abstractions;
using Fractalz.Infrastructure.Database.Contexts;
using Fractalz.Application.Domains.Entities.Todo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task = Fractalz.Application.Domains.Entities.Todo.Task;

namespace Fractalz.Infrastructure.Database.Repositories
{
    public class TaskRepository : IRepository<Application.Domains.Entities.Todo.Task>
    {
        private readonly DbSet<Application.Domains.Entities.Todo.Task> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// TaskRepository
        /// </summary>
        /// <param name="context"></param>
        public TaskRepository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<Application.Domains.Entities.Todo.Task>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Application.Domains.Entities.Todo.Task> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Application.Domains.Entities.Todo.Task> Get(Func<Application.Domains.Entities.Todo.Task, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Application.Domains.Entities.Todo.Task FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(Application.Domains.Entities.Todo.Task item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(Application.Domains.Entities.Todo.Task item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(Application.Domains.Entities.Todo.Task item)
        {
            _dbSet.Remove(item);
            return _context.SaveChanges();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Application.Domains.Entities.Todo.Task> GetWithInclude(Func<Application.Domains.Entities.Todo.Task, bool> predicate, params Expression<Func<Application.Domains.Entities.Todo.Task, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Task> GetWithInclude(object includeProperty,
            params Expression<Func<Task, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
