using EventureAPI.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
//using System.Diagnostics;
using EventureAPI.Models;

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

        public Task<Activity> GetActivityByIdAsync(int activityId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAll18PlusActivitiesAsync(bool is18Plus)
        {
            // If is18Plus is false, return an empty list
            if (!is18Plus)
            {
                return Enumerable.Empty<Activity>(); // Returns an empty list if not 18+
            }

            return await _context.Activities
                .Where(a => a.Is18Plus) // Filter for 18+ activities
                .ToListAsync();
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved)
        {
            if (isApproved)
            {
                return await _context.Activities
                    .Where(a => !a.IsApproved) // Filter for activities that are awaiting approval
                    .ToListAsync();
            }

            return await _context.Activities
                .Where(a => a.IsApproved) // Filter for approved activities
                .ToListAsync();
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesByLocationAsync(string location)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAllFreeActivitiesAsync(bool isFree)
        {
            // If isFree is false, return an empty list
            if (!isFree)
            {
                return Enumerable.Empty<Activity>(); // Returns an empty list if not free
            }

            return await _context.Activities
                .Where(a => a.IsFree) // Filter for free activities
                .ToListAsync();

        }
    }
}
