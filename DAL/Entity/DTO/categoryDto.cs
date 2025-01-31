using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.DAL.Entity.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }
    }
}