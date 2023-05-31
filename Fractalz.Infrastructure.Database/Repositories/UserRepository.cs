using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities;
using Fractalz.Application.Domains.Entities.Profile;
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
    public class UserRepository : IRepository<User>
    {
        private readonly DbSet<User> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// UserRepository
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ChatContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _dbSet = context.Set<User>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<User> Get(Func<User, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(User item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(User item)
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
        public IEnumerable<User> GetWithInclude(object includeProperty,
            params Expression<Func<User, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<User> GetWithInclude(Func<User, bool> predicate,
            params Expression<Func<User, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<User> Include(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
