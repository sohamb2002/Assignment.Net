using System.Linq.Expressions;
using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using MyApp.DAL.Interfaces;

namespace MyApp.DAL.IRepositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<ICollection<Post>> GetAllAsync(); // Correct method signature
        Task<ICollection<Post>> GetListAsync(Expression<Func<Post, bool>> condition); // Correct method signature
        Task<Post> GetByIdAsync(int id);
    }
}
