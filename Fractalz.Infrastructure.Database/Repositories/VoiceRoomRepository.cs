using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.Database.Repositories
{
    public class VoiceRoomRepository : IRepository<VoiceRoom>
    {
        private readonly DbSet<VoiceRoom> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// VoiceRoomRepository
        /// </summary>
        /// <param name="context"></param>
        public VoiceRoomRepository(ChatContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _dbSet = context.Set<VoiceRoom>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VoiceRoom> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<VoiceRoom> Get(Func<VoiceRoom, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VoiceRoom FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(VoiceRoom item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(VoiceRoom item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(VoiceRoom item)
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
        public IEnumerable<VoiceRoom> GetWithInclude(object includeProperty,
            params Expression<Func<VoiceRoom, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<VoiceRoom> GetWithInclude(Func<VoiceRoom, bool> predicate,
            params Expression<Func<VoiceRoom, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<VoiceRoom> Include(params Expression<Func<VoiceRoom, object>>[] includeProperties)
        {
            IQueryable<VoiceRoom> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}