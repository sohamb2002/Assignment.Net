using AutoMapper;
using MyApp.BAL.IServices;
using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using MyApp.DAL.Interfaces;
using MyApp.DAL.IRepositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyApp.BAL.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepo;

        public PostService(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        // Get all published posts
        public async Task<ICollection<PostDTO>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllAsync();
            return _mapper.Map<ICollection<PostDTO>>(posts);
        }

        // Add a new post
    public async Task<Post> AddPost(PostDTO postDTO)
{
    try
    {
        // Assuming you're adding the post to the database
        var addedPost = new Post
        {
            Title = postDTO.Title,          // Using postDTO instead of post
            Description = postDTO.Description,
            Category = postDTO.Category,
            CreatedBy = postDTO.CreatedBy,
            CreatedDate = postDTO.CreatedDate,
            IsPublished = postDTO.IsPublished
        };

        // Add to the database asynchronously
        await _postRepo.AddAsync(addedPost);  // This should be an async method if you're using EF Core or similar
        await _postRepo.SaveChangesAsync();  // Make sure to save changes to the database
        return addedPost; // Return the created post
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
        throw new Exception("An error occurred while adding the post.", ex);
    }
}

        // Update an existing post
public async Task<Post> UpdatePost(int id, PostDTO postDTO)
{
    // Fetch the existing post by its ID
    var existingPost = await _postRepo.GetByIdAsync(id);
    
    // If the post doesn't exist, return null
    if (existingPost == null)
    {
        return null; // Post not found
    }

    // Update the properties of the existing post with the provided DTO properties
    _mapper.Map(postDTO, existingPost);
    await _postRepo.SaveChangesAsync(); // Save the updated post to the database
    return existingPost; // Return the updated post
   
}




     
        public async Task<bool> DeletePost(int id)
        {
                   var user = await _postRepo.GetSingleAsync(u => u.Id == id);
            if (user == null) return false;
                user.IsPublished=false;
            _postRepo.SoftDelete(user);
            await _postRepo.SaveChangesAsync();
            return true;
        }

        // Get a single post by condition
public async Task<ICollection<PostDTO>> GetPostsByConditionAsync(Expression<Func<Post, bool>> condition)
{
    var posts = await _postRepo.GetListAsync(condition); // Fetch multiple posts
    return _mapper.Map<ICollection<PostDTO>>(posts); // Return the mapped DTOs
}



    }

    internal interface IPostRepository<T>
    {
    }
}
