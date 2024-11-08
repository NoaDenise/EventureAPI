using EventureAPI.Models;
using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryShowAdminDTO>> GetAllCategoriesAsync();
        Task AddCategoryAsync(CategoryCreateEditDTO categoryCreateEditDTO);
        Task EditCategoryAsync(int categoryId, CategoryCreateEditDTO categoryCreateEditDTO);
        Task DeleteCategoryAsync(int categoryId);
        Task<CategoryShowDTO> GetCategoryByIdAsync(int categoryId);
    }
}
