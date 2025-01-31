using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyApp.DAL.DBContext;
using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using MyApp.DAL.IRepositories;
using MyApp.DAL.Repository;

public class PostRepository : Repository<Post, AssignmentNetContext>, IPostRepository
{
    private readonly AssignmentNetContext _context;

    public PostRepository(AssignmentNetContext context) : base(context)
    {
        _context = context;
    }

    // public async Task<ICollection<Post>> GetAllPublishedPostsOfActiveUserById(int userId)
    // {
    //     var post = await _context.Posts
    //         .Where(post => post.CreatedBy == userId && post.IsPublished == true)
    //         .Select(post => new Post
    //         {
    //             Id = post.Id,
    //             Title = post.Title,
    //             Description = post.Description,
    //             Category = post.Category,
    //             CreatedBy = post.CreatedBy,
    //             CreatedDate = post.CreatedDate,
    //             IsPublished = post.IsPublished
    //         })
    //         .ToListAsync();

    //     return post;
    // }
  public async Task<ICollection<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }
  public async Task<ICollection<Post>> GetListAsync(Expression<Func<Post, bool>> condition)
        {
            return await _context.Posts.Where(condition).ToListAsync();
        }
//                     public async Task<Post> GetByIdAsync(int id)
// {
    
//     return await EMDBContext.Set<Post>().FirstOrDefaultAsync(u => u.Id == id);
// }

}
