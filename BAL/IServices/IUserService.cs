using Microsoft.AspNetCore.Mvc;
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
        Task<ICollection<User>> GetAllUsersAsync();
      Task<User> GetUserByEmailAsync(string email);

 Task<User> UpdateUser(int id, UpdateUserDTO user);
    Task<User> GetUserById(int id);
        // Add a new user
        Task<User> AddUser(UserDTO userDTO);

        // Update an existing user
        // Task<User> UpdateUser(UpdateUserDTO userDTO);

        // Delete a user
        Task<bool> DeleteUser(int id);

        // Get a user by condition
         Task<ICollection<UserDTO>> GetUsersByConditionAsync(Expression<Func<User, bool>> condition);

        // Authenticate user (Login)
    }
}
