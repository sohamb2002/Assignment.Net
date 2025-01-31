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

        public async Task<ICollection<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<ICollection<UserDTO>>(users);
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
        return await _userRepo.GetByIdAsync(id);
    }

    public async Task<User> UpdateUser(int id, UserDTO user)
    {
        var existingUser = await _userRepo.GetByIdAsync(id);
        if (existingUser == null)
        {
            return null; // User not found
        }

        // Only update the fields passed in the UserDTO
        existingUser.Name = user.Name ?? existingUser.Name;
        existingUser.Password = user.Password ?? existingUser.Password;
        existingUser.Email = user.Email?? existingUser.Email;
        existingUser.Phone = user.Phone?? existingUser.Phone;
        existingUser.IsActive = user.IsActive ?? existingUser.IsActive;
        existingUser.CreatedAt = user.CreatedAt ?? existingUser.CreatedAt;

        // Save changes to the database
        await _userRepo.UpdateAsync(existingUser);
        return existingUser;
    }


        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepo.GetSingleAsync(u => u.Id == id);
            if (user == null) return false;
user.IsActive=false;
            _userRepo.SoftDelete(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<UserDTO> GetUserByConditionAsync(Expression<Func<User, bool>> condition)
        {
            var user = await _userRepo.GetSingleAsync(condition);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
