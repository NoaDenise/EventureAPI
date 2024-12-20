﻿using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
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

        public async Task EditAdminInfoAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task EditAdminPasswordAsync(User user)
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

        public async Task<int> AddUserEvent(string userId, int activityId)
        {
            var existingUserEvent = await _context.UserEvents
                .FirstOrDefaultAsync(ue => ue.UserId == userId && ue.ActivityId == activityId);

            if (existingUserEvent != null)
            {
                throw new InvalidOperationException("User has already liked this event.");
            }

            // Create a new UserEvent
            var userEvent = new UserEvent
            {
                UserId = userId,
                ActivityId = activityId
            };

            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();

            return userEvent.UserEventId; // Return the UserEventId after creation
        }

        //temporary method
        public async Task<IEnumerable<int>> GetLikedActivities(string userId)
        {
            return await _context.UserEvents
            .Where(ue => ue.UserId == userId)
            .Select(ue => ue.ActivityId)
            .ToListAsync();


        }

        public async Task<IEnumerable<UserEvent>> GetUsersSavedEventsAsync(string userId)
        {
            return await _context.UserEvents.Include(u => u.User).Include(a => a.Activity).Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<UserEvent> GetUserEventByIdAsync(int userEventId)
        {
            return await _context.UserEvents.Include(u => u.User).Include(a => a.Activity).SingleOrDefaultAsync(ue => ue.UserEventId == userEventId);
        }

        public async Task DeleteUserEventAsync(UserEvent userEvent)
        {
            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserEvent>> GetUserEventsByCategoryAsync(int categoryId)
        {
            return await _context.UserEvents.Include(u => u.User).Include(a => a.Activity).Include(ac => ac.Activity.ActivityCategories).ToListAsync();
        }


        // Also deletes/removes a userEvent but uses a different input to do so
        public async Task<bool> RemoveUserEvent(string userId, int activityId)
        {
            var userEvent = await _context.UserEvents
                .FirstOrDefaultAsync(ue => ue.UserId == userId && ue.ActivityId == activityId);

            if (userEvent == null)
            {
                return false;
            }

            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
