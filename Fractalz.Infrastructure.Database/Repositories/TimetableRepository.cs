using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Timetable;
using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.Database.Repositories;

public class TimetableRepository : IRepository<Timetable>
{
    private readonly DbSet<Timetable> _dbSet;
    private readonly ChatContext _context;

    /// <summary>
    /// TimetableRepository
    /// </summary>
    /// <param name="context"></param>
    public TimetableRepository(ChatContext context)
    {
        _context = context;
        _dbSet = context.Set<Timetable>();
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Timetable> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<Timetable> Get(Func<Timetable, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    /// <summary>
    /// FindById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Timetable FindById(Guid id)
    {
        return _dbSet.Find(id);
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Timetable item)
    {
        _dbSet.Add(item);
        return _context.SaveChanges();
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Update(Timetable item)
    {
        _context.Entry(item).State = EntityState.Modified;
        return _context.SaveChanges();
    }

    /// <summary>
    /// Remove
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Remove(Timetable item)
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
    public IEnumerable<Timetable> GetWithInclude(object includeProperty,
        params Expression<Func<Timetable, object>>[] includeProperties)
    {
        return Include(includeProperties).ToList();
    }

    /// <summary>
    /// GetWithInclude
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public IEnumerable<Timetable> GetWithInclude(Func<Timetable, bool> predicate,
        params Expression<Func<Timetable, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.Where(predicate).ToList();
    }

    /// <summary>
    /// Include
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    private IQueryable<Timetable> Include(params Expression<Func<Timetable, object>>[] includeProperties)
    {
        IQueryable<Timetable> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
