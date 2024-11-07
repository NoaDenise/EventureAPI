using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventureAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        // Constructor for dependency injection of the category service
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        // Retrieves all categories
        [HttpGet("getAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryShowAdminDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories); 
        }

        // GET: api/Category/{id}
        // Retrieves a specific category by ID
        [HttpGet("getCategoryById/{id}")]
       /* [Authorize]*/ // Requires authorization to access this endpoint
        public async Task<ActionResult<CategoryShowDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
        }

        // POST: api/Category
        // Adds a new category
        [HttpPost("addCategory")]
        public async Task<ActionResult> AddCategory([FromBody] CategoryCreateEditDTO categoryCreateEditDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            try
            {
                await _categoryService.AddCategoryAsync(categoryCreateEditDTO);
                return CreatedAtAction(nameof(GetCategoryById), new { id = categoryCreateEditDTO.CategoryName }, new { message = "Category added successfully." }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the category.", error = ex.Message }); 
            }
        }

        // PUT: api/Category/{id}
        // Edits an existing category by ID
        [HttpPut("editCategory/{id}")]
       /* [Authorize] */// Requires authorization to access this endpoint
        public async Task<ActionResult> EditCategory(int id, [FromBody] CategoryShowAdminDTO categoryShowAdminDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            try
            {
                await _categoryService.EditCategoryAsync(id, categoryShowAdminDTO);
                return Ok(new { message = "Category updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the category.", error = ex.Message }); 
            }
        }

        // DELETE: api/Category/{id}
        // Deletes a category by ID
        [HttpDelete("deleteCategory/{id}")]
       /* [Authorize] */// Requires authorization to access this endpoint
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok(new { message = "Category deleted successfully." }); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the category.", error = ex.Message }); 
            }
        }
    }
}
