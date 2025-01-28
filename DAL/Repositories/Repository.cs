using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyApp.DAL.Interfaces;

namespace MyApp.DAL.Repository
{
    public abstract class Repository<T, Tcontext> : IRepository<T> where T : class where Tcontext : DbContext
    {
        protected readonly Tcontext EMDBContext = null;

        public Repository(Tcontext context)
        {
            this.EMDBContext = context;
        }

        // Synchronous methods
        public bool Add(T entity)
        {
            this.EMDBContext.Set<T>().Add(entity);
            SaveChangesManaged(); // To persist changes
            return true;
        }

        public bool Update(T entity)
        {
            this.EMDBContext.Entry(entity).State = EntityState.Modified;
            SaveChangesManaged(); // To persist changes
            return true;
        }

        public bool Delete(T entity)
        {
            this.EMDBContext.Set<T>().Remove(entity);
            SaveChangesManaged(); // To persist changes
            return true;
        }

        public void SaveChangesManaged()
        {
            this.EMDBContext.SaveChanges();
        }

        // Asynchronous methods
        public async Task<ICollection<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition)
        {
            return await EMDBContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> condition)
        {
            return await EMDBContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> condition)
        {
            return await EMDBContext.Set<T>().FirstOrDefaultAsync(condition);
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

        public List<T> GetMultiple(Expression<Func<T, bool>> condition)
        {
            return EMDBContext.Set<T>().Where(condition).ToList();
        }

        // To handle DB retries (optional)
        public IExecutionStrategy GetExecutionStrategy()
        {
            return this.EMDBContext.Database.CreateExecutionStrategy();
        }

        // Save changes asynchronously (future implementation, might not be used)
        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
