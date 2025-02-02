using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.DAL.Entity.DTO
{
    public class UpdateUserDTO
    {
        // The Name field is optional, but if provided, it must not exceed the specified length.
public int Id { get; set; }
        public string Name { get; set; }

        // Adding Required attribute to ensure Email is provided in the request body
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        // Password field is optional, but should follow some rules if required
        public string Password { get; set; }

        // Phone field validation with a length restriction
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string Phone { get; set; }
    }
}
