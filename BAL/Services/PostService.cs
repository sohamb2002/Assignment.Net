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
        public async Task<ICollection<PostDTO>> GetAllPublishedPosts(bool isPublished)
        {
            var posts = await _postRepo.GetAllAsync(post => post.IsPublished == isPublished);
            return _mapper.Map<ICollection<PostDTO>>(posts);
        }

        // Add a new post
        public async Task<bool> AddPost(PostDTO postDTO)
        {
            if (postDTO == null)
                throw new ArgumentNullException(nameof(postDTO));

            var post = _mapper.Map<Post>(postDTO);
            _postRepo.Add(post);
            await _postRepo.SaveChangesAsync();
            return true;
        }

        // Update an existing post
        public async Task<bool> UpdatePost(PostDTO postDTO)
        {
            if (postDTO == null)
                throw new ArgumentNullException(nameof(postDTO));

            var post = _mapper.Map<Post>(postDTO);
            _postRepo.Update(post);
            await _postRepo.SaveChangesAsync();
            return true;
        }
     
        public async Task<bool> DeletePost(int id)
        {
            var post = await _postRepo.GetSingleAsync(p => p.Id == id);
            if (post == null)
                return false;

            _postRepo.Delete(post);
            await _postRepo.SaveChangesAsync();
            return true;
        }

        // Get a single post by condition
        public async Task<PostDTO> GetPostByConditionAsync(Expression<Func<Post, bool>> condition)
        {
            var post = await _postRepo.GetSingleAsync(condition);
            return _mapper.Map<PostDTO>(post);
        }
    }

    internal interface IPostRepository<T>
    {
    }
}
