using MyApp.DAL.Entity.DTO;

namespace MyApp.BAL.IServices
{
    public interface IPostService
    {
        public  Task<ICollection<PostDTO>> GetAllPublishedPosts(bool ispublished);
       
        public Task<bool> AddPost(PostDTO post);
        public Task<bool> UpdatePost(PostDTO post);
        public Task<bool> DeletePost(int id);
    
    }
}