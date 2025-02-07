using MyApp.BAL.IServices;
using MyApp.DAL.Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.DAL.Entity;
using MyApp.Controllers.MyApp;

namespace MyApp.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _uservice;

        public UserController(IUserService service)
        {
            _uservice = service;
        }

        [HttpGet("FetchAllUsers")]
        public async Task<ApiResponse<ICollection<UserDTO>>> GetUsers()
        {
            var response = new ApiResponse<ICollection<UserDTO>>();
            try
            {
                // Fetch data from the service layer
                var users = await _uservice.GetAllUsersAsync();

                response.Data = users;
                response.StatusCode = 200;
                response.Message = "Users fetched successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                // Log the exception (add logging if necessary)
                response.StatusCode = 500; // Internal Server Error
                response.Message = "An error occurred while fetching users.";
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }

    [HttpGet("fetchActiveUsers")]
        public async Task<ApiResponse<ICollection<UserDTO>>> GetActiveUsers()
        {
            var response = new ApiResponse<ICollection<UserDTO>>();
            try
            {
                // Fetch data from the service layer
                var users = await _uservice.GetUsersByConditionAsync(u=>u.IsActive==true);

                response.Data = users;
                response.StatusCode = 200;
                response.Message = "Users fetched successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                // Log the exception (add logging if necessary)
                response.StatusCode = 500; // Internal Server Error
                response.Message = "An error occurred while fetching users.";
                response.Success = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }




[HttpPost("CreateUser")]
public async Task<ApiResponse<User>> CreateUser([FromBody] UserDTO user)
{
    var response = new ApiResponse<User>();

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
        var createdUser = await _uservice.AddUser(user);

        if (createdUser == null)
        {
            response.StatusCode = 400;
            response.Message = "Error creating user.";
            return response;
        }

        response.Data = createdUser;
        response.StatusCode = 201;
        response.Message = "User created successfully.";
        response.Success = true;
    }
    catch (Exception ex)
    {
        // Log the exception (add logging if necessary)
        response.StatusCode = 500; // Internal Server Error
        response.Message = "An error occurred while creating the user.";
        response.Success = false;
        response.Errors = new List<string> { ex.Message };
    }

    return response;
}
    

[HttpPut("UpdateUser/{id}")]
public async Task<ApiResponse<User>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDTO user)
{
    var response = new ApiResponse<User>();

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

        // Fetch the user by ID and update
        var updatedUser = await _uservice.UpdateUser(id, user);

        if (updatedUser == null)
        {
            response.StatusCode = 404;
            response.Message = "User not found.";
            return response;
        }

        response.Data = updatedUser;
        response.StatusCode = 200;  // Updated status code for success
        response.Message = "User updated successfully.";
        response.Success = true;
    }
    catch (Exception ex)
    {
        // Log the exception
        response.StatusCode = 500; // Internal Server Error
        response.Message = "An error occurred while updating the user.";
        response.Success = false;
        response.Errors = new List<string> { ex.Message };
    }

    return response;
}
    
[HttpGet("GetUserById/{id}")]
public async Task<ApiResponse<User>> GetUserById([FromRoute] int id)
{
    var response = new ApiResponse<User>();
    try
    {
        // Fetch data from the service layer
        var user = await _uservice.GetUserById(id);

        if (user == null)
        {
            response.StatusCode = 404;  // Not Found
            response.Message = "User not found.";
            response.Success = false;
        }
        else
        {
            response.Data = user;
            response.StatusCode = 200;  // OK
            response.Message = "User fetched successfully.";
            response.Success = true;
        }
    }
    catch (Exception ex)
    {
        // Log the exception (add logging if necessary)
        response.StatusCode = 500;  // Internal Server Error
        response.Message = "An error occurred while fetching the user.";
        response.Success = false;
        response.Errors = new List<string> { ex.Message };
    }

    return response;
}

[HttpPut("DeleteUser/{id}")]
public async Task<ApiResponse<User>> DeleteUser([FromRoute] int id)
{
    var response = new ApiResponse<User>();
    try
    {
        bool isDeleted = await _uservice.DeleteUser(id);
        
        if (isDeleted)
        {
            response.StatusCode = 200;
            response.Message = "User deleted successfully.";
            response.Success = true;
        }
        else
        {
            response.StatusCode = 404; // Not Found
            response.Message = "User not found or could not be deleted.";
            response.Success = false;
        }
    }
    catch (Exception ex)
    {
        response.StatusCode = 500;
        response.Message = "An error occurred while deleting the user.";
        response.Success = false;
        throw new Exception(ex.ToString());
    }
    
    return response; // Ensure response is returned in all cases
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
    }
