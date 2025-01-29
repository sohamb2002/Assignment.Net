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
        // Get all posts that match the published status
        Task<ICollection<PostDTO>> GetAllPublishedPosts(bool isPublished);

        // Add a new post
        Task<bool> AddPost(PostDTO postDTO);

        // Update an existing post
        Task<bool> UpdatePost(PostDTO postDTO);

        // Delete a post
        Task<bool> DeletePost(int id);

        // Get a post by condition
         
        Task<PostDTO> GetPostByConditionAsync(Expression<Func<Post, bool>> condition);
    }
}
