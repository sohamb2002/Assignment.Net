using System.Linq.Expressions;
using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using MyApp.DAL.Interfaces;

namespace MyApp.DAL.IRepositories
{
    public interface IPostRepository : IRepository<Post>
    {
      //  Task<Post> GetSingleAsync(Expression<Func<Post, bool>> condition);
     
    }

}