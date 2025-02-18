﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyApp.DAL.Entity;
using MyApp.DAL.Interfaces;

namespace MyApp.DAL.Repository
{
    public abstract class Repository<T, Tcontext> : IRepository<T> where T : class where Tcontext : DbContext
    {
        protected readonly Tcontext EMDBContext;

        public Repository(Tcontext context)
        {
            EMDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Synchronous methods
        public T Add(T entity)
        {
            this.EMDBContext.Set<T>().Add(entity);
            EMDBContext.SaveChanges(); // Synchronous
            return entity;
        }

        public bool Update(T entity)
        {
            this.EMDBContext.Entry(entity).State = EntityState.Modified;
            EMDBContext.SaveChanges(); // Synchronous
            return true;
        }
//      public async Task<Post> GetByPostId(int id)
// {
    
//     return await EMDBContext.Set<Post>().FirstOrDefaultAsync(u => u.Id == id);
// }
        public bool Delete(T entity)
        {
            this.EMDBContext.Set<T>().Remove(entity);
            EMDBContext.SaveChanges(); // Synchronous
            return true;
        }
        public bool SoftDelete(T entity)
        {
            this.EMDBContext.Entry(entity).State = EntityState.Modified;
                    EMDBContext.SaveChanges(); 
                    return true;
        }
        // Asynchronous methods
        public async Task<T> AddAsync(T entity)
        {
            await  this.EMDBContext.Set<T>().AddAsync(entity);
            if(entity!=null)
            {
                 await EMDBContext.SaveChangesAsync();
            }
            // Async
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.EMDBContext.Entry(entity).State = EntityState.Modified;
            await EMDBContext.SaveChangesAsync(); // Async
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            this.EMDBContext.Set<T>().Remove(entity);
            await EMDBContext.SaveChangesAsync(); // Async
            return true;
        }
     public async  Task<ICollection<T>> GetAllWithoutCondition()
     {
        return await EMDBContext.Set<T>().ToListAsync();
     }
        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> condition)
        {
            return await EMDBContext.Set<T>().Where(condition).ToListAsync();
        }

    public async Task<T> GetByIdAsync(int id)
{
    
    return await EMDBContext.Set<T>().FirstOrDefaultAsync(id => id.Equals(id));
}
//     public async Task<Category> GetByCategoryId(int id)
// {
    
//     return await EMDBContext.Set<Category>().FirstOrDefaultAsync(u => u.Id == id);
// }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> condition)
        {
            return await EMDBContext.Set<T>().FirstOrDefaultAsync(condition);
        }
public async Task<ICollection<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition)
{
    return await EMDBContext.Set<T>().Where(condition).ToListAsync() as ICollection<T>;
}

        public async Task<List<T>> GetMultipleAsync(Expression<Func<T, bool>> condition)
        {
            return await EMDBContext.Set<T>().Where(condition).ToListAsync();
        }

        // Synchronous queries
        public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition)
        {
            return EMDBContext.Set<T>().Where(condition);
        }

        public T GetSingle(Expression<Func<T, bool>> condition)
        {
            return EMDBContext.Set<T>().FirstOrDefault(condition);
        }

        // DB Execution Strategy
        public IExecutionStrategy GetExecutionStrategy()
        {
            return this.EMDBContext.Database.CreateExecutionStrategy();
        }

        // Save Changes
        public async Task SaveChangesAsync()
        {
            await EMDBContext.SaveChangesAsync();
        }
    }
}
