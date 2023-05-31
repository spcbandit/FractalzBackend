using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.Database.Repositories;

public class WorkSpaceRepository: IRepository<DocumentWorkSpace>
{
     private readonly DbSet<DocumentWorkSpace> _dbSet;
        private readonly ChatContext _context;
        
        /// <summary>
        /// DialogRepository
        /// </summary>
        /// <param name="context"></param>
        public WorkSpaceRepository(ChatContext context)
        {
            _context = context;
            _dbSet = context.Set<DocumentWorkSpace>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocumentWorkSpace> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DocumentWorkSpace> Get(Func<DocumentWorkSpace, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DocumentWorkSpace FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(DocumentWorkSpace item)
        {
            _dbSet.Add(item);
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(DocumentWorkSpace item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Remove(DocumentWorkSpace item)
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
        public IEnumerable<DocumentWorkSpace> GetWithInclude(Func<DocumentWorkSpace, bool> predicate,
            params Expression<Func<DocumentWorkSpace, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IQueryable<DocumentWorkSpace> Include(params Expression<Func<DocumentWorkSpace, object>>[] includeProperties)
        {
            IQueryable<DocumentWorkSpace> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// GetWithInclude
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<DocumentWorkSpace> GetWithInclude(object includeProperty,
            params Expression<Func<DocumentWorkSpace, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
    
}