using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        // Database context for accessing categories
        private readonly EventureContext _context;

        // Constructor to initialize the database context
        public CategoryRepository(EventureContext context)
        {
            _context = context;
        }
        // Method to add a new category to the database
        public async Task AddCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to add category to the database.", ex);
            }
        }

        // Method to delete a category from the database
        public async Task DeleteCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to delete category from the database.", ex);
            }
        }

        // Method to edit an existing category in the database
        public async Task EditCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }

            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to update category in the database.", ex);
            }
        }

        // Method to retrieve all categories from the database
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                // Return all categories as a list
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve categories from the database.", ex);
            }
        }

        // Method to retrieve a category by ID from the database
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                // Attempt to find the category by ID
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
                }

                return category; // Return the found category
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving the category with ID {categoryId}.", ex);
            }
        }
    }
}
