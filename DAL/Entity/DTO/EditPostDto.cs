using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.DAL.Entity.DTO
{
    public class EditPostDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

       
        
        public bool? IsPublished { get; set; }
    }
}