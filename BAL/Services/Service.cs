using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyApp.BAL.IServices;
using MyApp.DAL.Interfaces;
using MyApp.DAL.IRepositories;

namespace MyApp.BAL.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _repository.GetAllWithoutCondition();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null) return null;

            await _repository.UpdateAsync(entity);
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.SoftDelete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<T>> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return await _repository.GetAllByConditionAsync(condition);
        }
    }
}
