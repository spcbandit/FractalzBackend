using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Infrastructure.Database.Repositories
{
    public class TodoRepository : IRepository<TodoList>
    {
        private readonly DbSet<TodoList> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// TodoRepository
        /// </summary>
        /// <param name="context"></param>
        public TodoRepository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<TodoList>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoList> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TodoList> Get(Func<TodoList, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TodoList FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(TodoList item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(TodoList item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(TodoList item)
        {
            _dbSet.Remove(item);
            return _context.SaveChanges();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<TodoList> GetWithInclude(object includeProperty,
            params Expression<Func<TodoList, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<TodoList> GetWithInclude(Func<TodoList, bool> predicate,
            params Expression<Func<TodoList, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<TodoList> Include(params Expression<Func<TodoList, object>>[] includeProperties)
        {
            IQueryable<TodoList> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
