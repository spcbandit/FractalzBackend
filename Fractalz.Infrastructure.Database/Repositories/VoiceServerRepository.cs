using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.Database.Repositories
{
    public class VoiceServerRepository : IRepository<VoiceServer>
    {
        private readonly DbSet<VoiceServer> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// VoiceServerRepository
        /// </summary>
        /// <param name="context"></param>
        public VoiceServerRepository(ChatContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _dbSet = context.Set<VoiceServer>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VoiceServer> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<VoiceServer> Get(Func<VoiceServer, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VoiceServer FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(VoiceServer item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(VoiceServer item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(VoiceServer item)
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
        public IEnumerable<VoiceServer> GetWithInclude(object includeProperty,
            params Expression<Func<VoiceServer, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<VoiceServer> GetWithInclude(Func<VoiceServer, bool> predicate,
            params Expression<Func<VoiceServer, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<VoiceServer> Include(params Expression<Func<VoiceServer, object>>[] includeProperties)
        {
            IQueryable<VoiceServer> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}