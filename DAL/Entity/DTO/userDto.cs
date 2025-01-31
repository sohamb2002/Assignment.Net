using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.DAL.Entity.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email can't be longer than 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string Phone { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; } =DateTime.UtcNow;

        // Conversion methods to map between UserDTO and User entity
        public static User ToEntity(UserDTO userDTO)
        {
            return new User
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Phone = userDTO.Phone,
                IsActive = userDTO.IsActive,
                CreatedAt = userDTO.CreatedAt ?? DateTime.UtcNow // Use default if null
            };
        }

        public static UserDTO FromEntity(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
