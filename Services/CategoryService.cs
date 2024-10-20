using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Data;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;
using EventureAPI.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace EventureAPI.Services
{
    public class CategoryService : ICategoryService
    {       
        private readonly ICategoryRepository _categoryRepository;

        // Logger for logging information and errors
        private readonly ILogger<CategoryService> _logger;

        // Constructor to initialize repository and logger
        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        // Method to add a new category
        public async Task AddCategoryAsync(CategoryCreateEditDTO categoryCreateEditDTO)
        {
           

            if (categoryCreateEditDTO == null)
            {
                _logger.LogError("CategoryCreateEditDTO is null.");

                throw new ArgumentNullException(nameof(categoryCreateEditDTO), "categoryCreateEditDTO cannot be null.");
            }

            // Create a new category object from the DTO
            var category = new Category
            {
                CategoryName = categoryCreateEditDTO.CategoryName,
                CategoryDescription = categoryCreateEditDTO.CategoryDescription,
            };

            try
            {
                await _categoryRepository.AddCategoryAsync(category);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while adding the Category: {ex.Message}");
                throw;
            }

        }
        // Method to delete a category by ID
        public async Task DeleteCategoryAsync(int categoryId)
        {
            // Retrieve the category from the repository
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                _logger.LogError($"Category with ID {categoryId} not found.");
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            try
            {
               
                await _categoryRepository.DeleteCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the Category: {ex.Message}");
                throw;
            }

        }

        // Method to edit an existing category
        public async Task EditCategoryAsync(int categoryId, CategoryCreateEditDTO categoryCreateEditDTO)
        {
            // Retrieve the category from the repository
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (category == null)
            {
                _logger.LogError($"Category with ID {categoryId} not found.");

                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            // Update category properties from the DTO
            category.CategoryName = categoryCreateEditDTO.CategoryName;
            category.CategoryDescription = categoryCreateEditDTO.CategoryDescription;

            try
            {
                await _categoryRepository.EditCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while editing the Category: {ex.Message}");
                throw;
            }
        }
        // Method to retrieve all categories
        public async Task<IEnumerable<CategoryShowDTO>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();

                return categories.Select(category => new CategoryShowDTO
                {
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving all Categories: {ex.Message}");
                throw;
            }

        }

        // Method to retrieve a category by ID
        public async Task<CategoryShowDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                _logger.LogError($"Category with ID {categoryId} not found.");
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }

            var categoryDTO = new CategoryShowDTO
            {
                
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription
            };

            return categoryDTO;
        }
    }
}
     
