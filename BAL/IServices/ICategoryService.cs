using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyApp.BAL.IServices
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDTO>> GetAllCategoriesAsync();
        Task<Category> UpdateCategory(int id, CategoryDTO category);
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(CategoryDTO category);
        Task<bool> DeleteCategory(int id);
    }
}
