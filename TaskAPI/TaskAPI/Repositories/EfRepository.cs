using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Repositories;

public sealed class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppContextDb _db;
    private readonly DbSet<T> _set;

    public EfRepository(AppContextDb db)
    {
        _db = db;
        _set = _db.Set<T>();
    }

    public async System.Threading.Tasks.Task<T?> GetByIdAsync(int id)
        => await _set.FindAsync(id);

    public async System.Threading.Tasks.Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        => filter is null ? await _set.ToListAsync() : await _set.Where(filter).ToListAsync();

    public async System.Threading.Tasks.Task<T> AddAsync(T entity)
    {
        _set.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(T entity)
    {
        _set.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var e = await GetByIdAsync(id);
        if (e is null) return;
        _set.Remove(e);
        await _db.SaveChangesAsync();
    }
}
