using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyApp.BAL.IServices
{
    public interface IUserService
    {
        // Get all users
        Task<ICollection<UserDTO>> GetAllUsersAsync();

 Task<User> UpdateUser(int id, UserDTO user);
    Task<User> GetUserById(int id);
        // Add a new user
        Task<User> AddUser(UserDTO userDTO);

        // Update an existing user
        // Task<User> UpdateUser(UserDTO userDTO);

        // Delete a user
        Task<bool> DeleteUser(int id);

        // Get a user by condition
        Task<UserDTO> GetUserByConditionAsync(Expression<Func<User, bool>> condition);

        // Authenticate user (Login)
    }
}
