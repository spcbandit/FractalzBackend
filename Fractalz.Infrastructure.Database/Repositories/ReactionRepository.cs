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
namespace Fractalz.Infrastructure.Database.Repositories;

    public class ReactionRepository : IRepository<Reaction>
    {
        private readonly DbSet<Reaction> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// ReactionRepository
        /// </summary>
        /// <param name="context"></param>
        public ReactionRepository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<Reaction>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Reaction> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Reaction> Get(Func<Reaction, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reaction FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(Reaction item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(Reaction item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(Reaction item)
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
        public IEnumerable<Reaction> GetWithInclude(Func<Reaction, bool> predicate,
            params Expression<Func<Reaction, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<Reaction> Include(params Expression<Func<Reaction, object>>[] includeProperties)
        {
            IQueryable<Reaction> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<Reaction> GetWithInclude(object includeProperty,
            params Expression<Func<Reaction, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
    }

