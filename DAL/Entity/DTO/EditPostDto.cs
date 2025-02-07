using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.DAL.Entity.DTO
{
    public class EditPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

       
        
        public bool? IsPublished { get; set; }
    }
}