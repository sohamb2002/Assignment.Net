using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using MyApp.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MyApp.BAL.IServices;

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
            IsActive = user.IsActive
        };

        // Add to the database asynchronously
       await _userRepo.AddAsync(addedUser);  // This should be an async method if you're using EF Core or similar
        return addedUser; // Return the created user
    }
    catch (Exception ex)
    {
        // Log error
        throw new Exception("An error occurred while adding the user.", ex);
    }
}




        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            if (userDTO == null) throw new ArgumentNullException(nameof(userDTO));

            var user = _mapper.Map<User>(userDTO);
            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepo.GetSingleAsync(u => u.Id == id);
            if (user == null) return false;

            _userRepo.Delete(user);
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
