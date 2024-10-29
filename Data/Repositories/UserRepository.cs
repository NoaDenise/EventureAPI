﻿using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventureContext _context;
        public UserRepository(EventureContext context)
        {
            _context = context;
        }

        // Lists every user
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var allUsers = await _context.Users.ToListAsync();
            return allUsers;
        }

        // Adds a user to the database
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        // Delets a user from the database
        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null) 
            {
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();
        }

        // Edits the user found by id
        public async Task EditUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Finds a user by id
        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddCategoryToUserAsync(string userId, int categoryId)
        {
            var user = await _context.Users.FindAsync(userId);
            var category = await _context.Categories.FindAsync(categoryId);

            if (user == null || category == null)
            {
                throw new Exception("User or Category not found.");
            }

            var userCategory = new UserCategory
            {
                UserId = userId,
                CategoryId = categoryId
            };

            _context.UserCategories.Add(userCategory);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Category>> GetUserPreferencesAsync(string userId)
        {
            return await _context.UserCategories
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Category)
                .ToListAsync();
        }
    }
}
