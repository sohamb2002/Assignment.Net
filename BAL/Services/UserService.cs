using MyApp.DAL.Entity;


using MyApp.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MyApp.BAL.IServices;
using MyApp.DAL.Entity.DTO;

namespace MyApp.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users;
        }

public async Task<User> AddUser(UserDTO user)
{
    try
    {
        // Assuming you're adding the user to the database
        var addedUser = new User
        {
            Name = user.Name,
            Password = user.Password,
            Email = user.Email,
            Phone = user.Phone,
            IsActive = user.IsActive
        };

        // Add to the database asynchronously
       await _userRepo.AddAsync(addedUser);  // This should be an async method if you're using EF Core or similar
        return addedUser; // Return the created user
    }
    catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
    throw new Exception("An error occurred while adding the user.", ex);
}

}




        public async Task<User> GetUserById(int id)
    {
         Console.WriteLine($"Fetching user with ID: {id}");
    var user = await _userRepo.GetSingleAsync(u => u.Id == id);
    Console.WriteLine($"Fetched user: {user?.Name ?? "not found"}");
    return user;
        // return await _userRepo.GetByIdAsync(id);
    }

public async Task<User> UpdateUser(int id, UpdateUserDTO user)
{
    // Fetch the user from the database
    var existingUser = await _userRepo.GetSingleAsync(u => u.Id == id);

    if (existingUser == null)
    {
        return null; // User not found
    }

    // Check if the new email is already taken by another user
    if (!string.IsNullOrEmpty(user.Email) && user.Email != existingUser.Email)
    {
        var emailExists = await _userRepo.GetSingleAsync(u => u.Email == user.Email);
        if (emailExists != null)
        {
            return null; // Email is already in use
        }
    }

    // Only update the fields if they are provided in the UserDTO
    existingUser.Name = user.Name ?? existingUser.Name;
    existingUser.Email = user.Email ?? existingUser.Email;
    existingUser.Phone = user.Phone ?? existingUser.Phone;
    existingUser.IsActive = user.IsActive ?? existingUser.IsActive;

    // ✅ Only update password if it's provided
    if (!string.IsNullOrEmpty(user.Password))
    {
        existingUser.Password = user.Password;  // Consider hashing it before saving
    }

    // Save changes to the database
    await _userRepo.UpdateAsync(existingUser);

    return existingUser;
}




        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepo.GetSingleAsync(u => u.Id == id);
            if (user == null) return false;
user.IsDeleted=true;
            _userRepo.SoftDelete(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<UserDTO>> GetUsersByConditionAsync(Expression<Func<User, bool>> condition)
        {
            var posts = await _userRepo.GetAllByConditionAsync(condition); // Fetch posts by condition
            return _mapper.Map<ICollection<UserDTO>>(posts); // Map and return the result
        }
public async Task<User> GetUserByEmailAsync(string email)
{
    if (string.IsNullOrEmpty(email))
    {
        throw new ArgumentException("Email cannot be empty", nameof(email));
    }

    var user = await _userRepo.GetSingleAsync(u => u.Email == email);

    return user;
}


    }
}
