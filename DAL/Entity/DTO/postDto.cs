using System;

namespace MyApp.DAL.Entity.DTO
{
    public class PostDTO
    {
        public int Id { get; set; } // User ID
        public string Name { get; set; } // User Name
        public int PostId { get; set; } // Post ID
        public string Title { get; set; } // Post Title
        public string Description { get; set; } // Post Description
        public int Category { get; set; } // Post Category
        public DateTime? CreatedDate { get; set; } // Post Creation Date
        public bool? IsPublished { get; set; } // Post Publish Status
    }
}
