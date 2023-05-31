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
    public class DialogRepository : IRepository<Dialog>
    {
        private readonly DbSet<Dialog> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// DialogRepository
        /// </summary>
        /// <param name="context"></param>
        public DialogRepository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<Dialog>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Dialog> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Dialog> Get(Func<Dialog, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dialog FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(Dialog item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(Dialog item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(Dialog item)
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
        public IEnumerable<Dialog> GetWithInclude(Func<Dialog, bool> predicate,
            params Expression<Func<Dialog, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<Dialog> Include(params Expression<Func<Dialog, object>>[] includeProperties)
        {
            IQueryable<Dialog> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<Dialog> GetWithInclude(object includeProperty,
            params Expression<Func<Dialog, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
    }
}
