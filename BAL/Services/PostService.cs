using AutoMapper;
using MyApp.BAL.IServices;
using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
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
        public async Task<ICollection<Post>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllAsync();
            return _mapper.Map<ICollection<Post>>(posts);
        }
     public async Task<Post> GetPostById(int id)
    {
         Console.WriteLine($"Fetching post with ID: {id}");
    var post = await _postRepo.GetSingleAsync(u => u.Id == id);

    return post;
        // return await _userRepo.GetByIdAsync(id);
    }
    
        // Add a new post
        public async Task<Post> AddPost(PostDTO postDTO)
        {
            try
            {
                // Map the PostDTO to the Post entity using AutoMapper
                var addedPost = _mapper.Map<Post>(postDTO);

                // Add to the database asynchronously
                await _postRepo.AddAsync(addedPost);
                await _postRepo.SaveChangesAsync(); // Save changes to the database

                return addedPost; // Return the created post
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while adding the post.", ex);
            }
        }

        // Update an existing post
        public async Task<Post> UpdatePost(int id, EditPostDTO postDto)
{
    
    Console.WriteLine(postDto.Id);
    
    var existingPost = await _postRepo.GetSingleAsync(a=>a.Id==id);

    if (existingPost == null)
    {
        return null; // Return null if the post doesn't exist
    }

    // Manual Mapping
    existingPost.Title = postDto.Title;
    existingPost.Description = postDto.Description;
    existingPost.IsPublished = postDto.IsPublished;

    await _postRepo.UpdateAsync(existingPost);

    return existingPost; // Return the updated post
}


        // Soft delete a post (unpublish it)
          public async Task<bool> DeletePost(int id)
        {
                   var post = await _postRepo.GetSingleAsync(u => u.Id == id);
            if (post == null) return false;
                post.IsDeleted=true;
            _postRepo.SoftDelete(post);
            await _postRepo.SaveChangesAsync();
            return true;
        }

        // Get posts by condition
        public async Task<ICollection<Post>> GetPostsByConditionAsync(Expression<Func<Post, bool>> condition)
        {
            var posts = await _postRepo.GetAllByConditionAsync(condition); // Fetch posts by condition
            return _mapper.Map<ICollection<Post>>(posts); // Map and return the result
        }
    }
}

