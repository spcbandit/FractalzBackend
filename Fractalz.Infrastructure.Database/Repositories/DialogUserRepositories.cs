using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.Database.Repositories
{
    public class DialogUserRepositories: IRepository<DialogUser>
    {
        private readonly DbSet<DialogUser> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// DialogUserRepositories
        /// </summary>
        /// <param name="context"></param>
        public DialogUserRepositories(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<DialogUser>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DialogUser> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DialogUser> Get(Func<DialogUser, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DialogUser FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(DialogUser item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(DialogUser item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(DialogUser item)
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
        public IEnumerable<DialogUser> GetWithInclude(Func<DialogUser, bool> predicate,
            params Expression<Func<DialogUser, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<DialogUser> Include(params Expression<Func<DialogUser, object>>[] includeProperties)
        {
            IQueryable<DialogUser> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<DialogUser> GetWithInclude(object includeProperty,
            params Expression<Func<DialogUser, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
    }
}