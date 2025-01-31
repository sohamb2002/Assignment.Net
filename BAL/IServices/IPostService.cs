using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyApp.BAL.IServices
{
    public interface IPostService
    {
        // Get all posts
        Task<ICollection<PostDTO>> GetAllPosts();

        // Add a new post
        Task<Post> AddPost(PostDTO postDTO);

        // Update an existing post
         Task<Post> UpdatePost(int id, PostDTO post);

        // Delete a post
        Task<bool> DeletePost(int id);

        // Get posts by condition (using an expression, e.g., category filter)
        Task<ICollection<PostDTO>> GetPostsByConditionAsync(Expression<Func<Post, bool>> condition);
    }
}
