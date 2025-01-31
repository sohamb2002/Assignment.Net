using MyApp.BAL.IServices;
using MyApp.DAL.Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.DAL.Entity;
using MyApp.Controllers.MyApp;

namespace MyApp.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // Fetch all posts
        [HttpGet("FetchAllPosts")]
        public async Task<ApiResponse<ICollection<PostDTO>>> GetAllPosts()
        {
            var response = new ApiResponse<ICollection<PostDTO>>();
            try
            {
                // Fetch data from the service layer
                var posts = await _postService.GetAllPosts();

                response.Data = posts;
                response.StatusCode = 200;
                response.Message = "Posts fetched successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                // Log the exception (add logging if necessary)
                response.StatusCode = 500; // Internal Server Error
                response.Message = "An error occurred while fetching posts.";
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }

        // Fetch posts by category ID
        [HttpGet("FetchPost/{cat_id}")]
        public async Task<ApiResponse<ICollection<PostDTO>>> GetPostsByCategoryId(int cat_id)
        {
            var response = new ApiResponse<ICollection<PostDTO>>();
            try
            {
                // Fetch posts by category condition
                var posts = await _postService.GetPostsByConditionAsync(p => p.Category == cat_id);

                if (posts == null || posts.Count == 0)
                {
                    response.StatusCode = 404; // Not Found
                    response.Message = "No posts found for the given category.";
                    response.Success = false;
                    return response;
                }

                response.Data = posts;
                response.StatusCode = 200; // OK
                response.Message = "Posts fetched successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.Message = "An error occurred while fetching posts.";
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
[HttpPut("EditPost/{id}")]
public async Task<ApiResponse<Post>> UpdatePost([FromRoute] int id, [FromBody] PostDTO post)
{
    var response = new ApiResponse<Post>();

    try
    {
        // Validate the user input
        if (!ModelState.IsValid)
        {
            response.StatusCode = 400;
            response.Message = "Invalid Post data.";
            response.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return response;
        }

        // Fetch the user by ID and update
        var updatedUser = await _postService.UpdatePost(id, post);

        if (updatedUser == null)
        {
            response.StatusCode = 404;
            response.Message = "Post not found.";
            return response;
        }

        response.Data = updatedUser;
        response.StatusCode = 200;  // Updated status code for success
        response.Message = "Post updated successfully.";
        response.Success = true;
    }
    catch (Exception ex)
    {
        // Log the exception
        response.StatusCode = 500; // Internal Server Error
        response.Message = "An error occurred while updating the post.";
        response.Success = false;
        response.Errors = new List<string> { ex.Message };
    }

    return response;
}
 //GetPostsByConditionAsync   
     [HttpGet("FetchPublishedPosts")]
        public async Task<ApiResponse<ICollection<PostDTO>>> GetPublishedPosts()
        {
            var response = new ApiResponse<ICollection<PostDTO>>();
            try
            {
                // Fetch posts by category condition
                var posts = await _postService.GetPostsByConditionAsync(p => p.IsPublished == true);

                if (posts == null || posts.Count == 0)
                {
                    response.StatusCode = 404; // Not Found
                    response.Message = "No posts found for the given category.";
                    response.Success = false;
                    return response;
                }

                response.Data = posts;
                response.StatusCode = 200; // OK
                response.Message = "Posts fetched successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.Message = "An error occurred while fetching posts.";
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }

[HttpGet("FetchPublishedPost/{id}")]
        public async Task<ApiResponse<ICollection<PostDTO>>> GetPublishedPostbyId([FromRoute] int id)
        {
            var response = new ApiResponse<ICollection<PostDTO>>();
            try
            {
                // Fetch posts by category condition
                var posts = await _postService.GetPostsByConditionAsync(p => (p.Id==id) && (p.IsPublished==true));

                if (posts == null || posts.Count == 0)
                {
                    response.StatusCode = 404; // Not Found
                    response.Message = "No posts found for the given category.";
                    response.Success = false;
                    return response;
                }

                response.Data = posts;
                response.StatusCode = 200; // OK
                response.Message = "Posts fetched successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.Message = "An error occurred while fetching posts.";
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }


[HttpPost("CreatePost")]
    public async Task<ApiResponse<Post>> CreatePost([FromBody] PostDTO post)
    {
         var response = new ApiResponse<Post>();

    try
    {

        // Validate the user input
        if (!ModelState.IsValid)
        {
            response.StatusCode = 400;
            response.Message = "Invalid user data.";
            response.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return response;
        }

        // Add user to database
        var createdPost = await _postService.AddPost(post);

        if (createdPost == null)
        {
            response.StatusCode = 400;
            response.Message = "Error creating post.";
            return response;
        }

        response.Data = createdPost;
        response.StatusCode = 201;
        response.Message = "Post created successfully.";
        response.Success = true;
    }
    catch (Exception ex)
    {
        // Log the exception (add logging if necessary)
        response.StatusCode = 500; // Internal Server Error
        response.Message = "An error occurred while creating the post.";
        response.Success = false;
        response.Errors = new List<string> { ex.Message };
    }

    return response;
}


[HttpPut("DeletePost/{id}")]
public async Task<ApiResponse<Post>> DeletePost([FromRoute] int id)
{
    var response = new ApiResponse<Post>();
    try
    {
        bool isDeleted = await _postService.DeletePost(id); // Assuming you have a DeletePost method in your IPostService
        
        if (isDeleted)
        {
            response.StatusCode = 200;
            response.Message = "Post deleted successfully.";
            response.Success = true;
        }
        else
        {
            response.StatusCode = 404; // Not Found
            response.Message = "Post not found or could not be deleted.";
            response.Success = false;
        }
    }
    catch (Exception ex)
    {
        response.StatusCode = 500;
        response.Message = "An error occurred while deleting the post.";
        response.Success = false;
        throw new Exception(ex.ToString());
    }
    
    return response; // Ensure response is returned in all cases
}






    }
}
namespace MyApp
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ApiResponse(T data, int statusCode, string message = "")
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = null;
            StatusCode = statusCode;
        }

        public ApiResponse()
        {
        }
    }
}
    
