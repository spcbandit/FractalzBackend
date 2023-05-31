using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Timetable;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.Database.Repositories;

public class ScheduleRepository : IRepository<Schedule>
{
    private readonly DbSet<Schedule> _dbSet;
    private readonly ChatContext _context;

    /// <summary>
    /// ScheduleRepository
    /// </summary>
    /// <param name="context"></param>
    public ScheduleRepository(ChatContext context)
    {
        _context = context;
        _dbSet = context.Set<Schedule>();
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Schedule> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<Schedule> Get(Func<Schedule, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    /// <summary>
    /// FindById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Schedule FindById(Guid id)
    {
        return _dbSet.Find(id);
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Schedule item)
    {
        _dbSet.Add(item);
        return _context.SaveChanges();
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Update(Schedule item)
    {
        _context.Entry(item).State = EntityState.Modified;
        return _context.SaveChanges();
    }

    /// <summary>
    /// Remove
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Remove(Schedule item)
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
    public IEnumerable<Schedule> GetWithInclude(object includeProperty,
        params Expression<Func<Schedule, object>>[] includeProperties)
    {
        return Include(includeProperties).ToList();
    }

    /// <summary>
    /// GetWithInclude
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public IEnumerable<Schedule> GetWithInclude(Func<Schedule, bool> predicate,
        params Expression<Func<Schedule, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.Where(predicate).ToList();
    }

    /// <summary>
    /// Include
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    private IQueryable<Schedule> Include(params Expression<Func<Schedule, object>>[] includeProperties)
    {
        IQueryable<Schedule> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}