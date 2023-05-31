using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.AdminSettings;
using Fractalz.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Fractalz.Infrastructure.Database.Repositories;

public class AdminSettingsRepository : IRepository<AdminSetting>
{
    
    private readonly DbSet<AdminSetting> _dbSet;
    private readonly ChatContext _context;
        
    /// <summary>
    /// AdminSettingsRepository
    /// </summary>
    /// <param name="context"></param>
    public AdminSettingsRepository(ChatContext context)
    {
        _context = context;
        _dbSet = context.Set<AdminSetting>();
    }
    /// <summary>
    /// Создание БД
    /// </summary>
    public int Create(AdminSetting item)
    {
        _dbSet.Add(item);
        return _context.SaveChanges();
    }
    /// <summary>
    /// FindById
    /// </summary>
    public AdminSetting FindById(Guid id)
    {
        return _dbSet.Find(id);
    }
    /// <summary>
    /// Get
    /// </summary>
    public IEnumerable<AdminSetting> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }
    /// <summary>
    /// Get
    /// </summary>
    public IEnumerable<AdminSetting> Get(Func<AdminSetting, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }
    /// <summary>
    /// Remove
    /// </summary>
    public int Remove(AdminSetting item)
    {
        _dbSet.Remove(item);
        return _context.SaveChanges();
    }
    /// <summary>
    /// Update
    /// </summary>
    public int Update(AdminSetting item)
    {
        _context.Entry(item).State = EntityState.Modified;
        return _context.SaveChanges();
    }
    /// <summary>
    /// GetWithInclude
    /// </summary>
    public IEnumerable<AdminSetting> GetWithInclude(Func<AdminSetting, bool> predicate, params Expression<Func<AdminSetting, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.Where(predicate).ToList();
    }

    /// <summary>
    /// GetWithInclude
    /// </summary>
    public IEnumerable<AdminSetting> GetWithInclude(object includeProperty, params Expression<Func<AdminSetting, object>>[] includeProperties)
    {
        return Include(includeProperties).ToList();
    }
    /// <summary>
    /// Include
    /// </summary>
    private IQueryable<AdminSetting> Include(params Expression<Func<AdminSetting, object>>[] includeProperties)
    {
        IQueryable<AdminSetting> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}