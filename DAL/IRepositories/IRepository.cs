using Microsoft.EntityFrameworkCore.Storage;
using MyApp.DAL.Entity;
using System.Linq.Expressions;

namespace MyApp.DAL.Interfaces
{
    public interface IRepository<T>
    {
        // Synchronous methods
        IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition);
        T GetSingle(Expression<Func<T, bool>> condition);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool SoftDelete(T entity);
Task<T> AddAsync(T entity);
Task<T> GetByIdAsync(int id);
// Task<Post> GetByPostId(int id);
// Task<Category> GetByCategoryId(int id);
Task<T> UpdateAsync(T entity);
        // Asynchronous methods
        Task<ICollection<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> condition);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> condition);
        Task<List<T>> GetMultipleAsync(Expression<Func<T, bool>> condition);

        // Execution Strategy (for handling DB retries)
        IExecutionStrategy GetExecutionStrategy();

        // Save changes
        Task SaveChangesAsync();
    }
}
