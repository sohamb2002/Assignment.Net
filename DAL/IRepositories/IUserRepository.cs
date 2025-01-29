
using MyApp.DAL.Entity.DTO;
using MyApp.DAL.Entity;
using MyApp.DAL.Interfaces;

namespace MyApp.DAL.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<ICollection<User>> GetAllAsync();
    }
    
}