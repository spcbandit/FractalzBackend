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
using Fractalz.Application.Domains.Entities.Chat;
namespace Fractalz.Infrastructure.Database.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly DbSet<Message> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// MessageRepository
        /// </summary>
        /// <param name="context"></param>
        public MessageRepository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<Message>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Message> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Message> Get(Func<Message, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(Message item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(Message item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(Message item)
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
        public IEnumerable<Message> GetWithInclude(Func<Message, bool> predicate,
            params Expression<Func<Message, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<Message> Include(params Expression<Func<Message, object>>[] includeProperties)
        {
            IQueryable<Message> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<Message> GetWithInclude(object includeProperty,
            params Expression<Func<Message, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
    }
}
