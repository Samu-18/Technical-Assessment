using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskAPI.Repositories;

public interface IRepository<T> where T : class
{
    System.Threading.Tasks.Task<T?> GetByIdAsync(int id);
    System.Threading.Tasks.Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    System.Threading.Tasks.Task<T> AddAsync(T entity);
    System.Threading.Tasks.Task UpdateAsync(T entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}