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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // Fetch all categories
          public async Task<ICollection<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return _mapper.Map<ICollection<CategoryDTO>>(categories);  // .Map<ICollection<CategoryDTO>>(categories): The .Map<>() method is used to perform the mapping. This tells AutoMapper to map the provided categories object (which is likely of a different type, such as ICollection<Category>) into an ICollection<CategoryDTO>.
        }

        // Add a new category
        public async Task<Category> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                // Ensure you're creating Category, not User
                var addedCategory = new Category
                {
                    Name = categoryDTO.Name // Correctly mapping to Category
                };

                // Add to the database asynchronously
                await _categoryRepo.AddAsync(addedCategory);
                return addedCategory; // Return the created category
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging library here)
                Console.WriteLine($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while adding the category.", ex);
            }
        }

        // Get a category by its ID
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _categoryRepo.GetByCategoryId(id);
            return category;
        }

        // Update an existing category
        public async Task<Category> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            var existingCategory = await _categoryRepo.GetByCategoryId(id);
            if (existingCategory == null)
            {
                return null; // Category not found
            }

            // Only update the fields passed in the CategoryDTO
            existingCategory.Name = categoryDTO.Name ?? existingCategory.Name;

            // Save changes to the database
            await _categoryRepo.UpdateAsync(existingCategory);
            return existingCategory;
        }

        // Delete a category
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _categoryRepo.GetByCategoryId(id);
            if (category == null) return false;

            _categoryRepo.Delete(category);
            await _categoryRepo.SaveChangesAsync();
            return true;
        }
    }
}
