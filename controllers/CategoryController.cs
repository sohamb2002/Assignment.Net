using MyApp.BAL.IServices;
using MyApp.DAL.Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet("fetchAllCategories")]
        public async Task<ActionResult<ICollection<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("fetchCategory/{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/category
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoryDTO>> AddCategory([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedCategory = await _categoryService.AddCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = addedCategory.Id }, addedCategory);
        }

        // PUT: api/category/{id}
        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCategory = await _categoryService.UpdateCategory(id, categoryDto);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        // DELETE: api/category/{id}
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategory(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
