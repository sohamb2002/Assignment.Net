using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.DAL.Entity.DTO
{
   public class UpdateUserDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    // ✅ Make Password nullable so it is not required
    public string? Password { get; set; }=null;

    [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
    public string? Phone { get; set; }

    public bool? IsActive { get; set; }
}

}
