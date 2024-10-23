using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly EventureContext _context;

        public ActivityRepository(EventureContext context)
        {
            _context = context;
        }

        public Task AddActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task EditActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            return await _context.Activities.SingleOrDefaultAsync(a => a.ActivityId == activityId);
        }

        public Task<IEnumerable<Activity>> GetAll18PlusActivitiesAsync(bool is18Plus)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByCategoryAsync(int categoryId)
        {
            //include tables to find categoryId
            return await _context.Activities
                .Include(a => a.ActivityCategories)
                .ThenInclude(ac => ac.Category)
                .Where(a => a.ActivityCategories.Any(ac => ac.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByLocationAsync(string location)
        {
            return await _context.Activities.Where(a => a.ActivityLocation == location).ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId)
        {
            //finding a user's preferences by filtering among userCategories
            return await _context.Activities
                .Include(a => a.ActivityCategories)
                .ThenInclude(a => a.Category)
                .Where(a => a.ActivityCategories
                .Any(a => a.Category.UserCategories
                .Any(a => a.UserCategoryId == userCategoryId)))
                .ToListAsync();
        }

        public Task<IEnumerable<Activity>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetAllFreeActivitiesAsync(bool isFree)
        {
            throw new NotImplementedException();
        }
    }
}
