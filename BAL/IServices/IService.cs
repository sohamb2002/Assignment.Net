using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyApp.BAL.IServices
{
    public interface IService<T> where T : class
    {
        // Get all entities
        Task<ICollection<T>> GetAll();

        // Get entity by ID
        Task<T> GetById(int id);

        // Add a new entity
        Task<T> Add(T entity);

        // Update an existing entity
        Task<T> Update(int id, T entity);

        // Delete an entity
        Task<bool> Delete(int id);

        // Get entities by condition (expression)
        Task<ICollection<T>> GetByCondition(Expression<Func<T, bool>> condition);
    }
}
