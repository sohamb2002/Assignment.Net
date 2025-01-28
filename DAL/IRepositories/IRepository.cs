using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace MyApp.DAL.Interfaces
{
    public interface IRepository<T>
    {
        // Synchronous methods
        IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition);
        T GetSingle(Expression<Func<T, bool>> condition);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);

        // Asynchronous methods
        Task<ICollection<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition);
       public Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> condition);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> condition);
        Task<List<T>> GetMultipleAsync(Expression<Func<T, bool>> condition);

        // Execution Strategy (for handling DB retries)
        IExecutionStrategy GetExecutionStrategy();

        // Additional query methods for like-based search
        IQueryable<T> GetAllByLikeCondition(Expression<Func<T, string>> propertySelector, string value);
        Task<ICollection<T>> GetAllByLikeConditionAsync(Expression<Func<T, string>> propertySelector, string value);

        // Save changes
        Task SaveChangesAsync();
    }
}
