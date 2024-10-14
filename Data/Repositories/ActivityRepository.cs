using EventureAPI.Data.Repositories.IRepositories;
using System.Diagnostics;

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

        public Task<IEnumerable<Activity>> GetAllFreeActivitiesAsync(bool isFree)
        {
            throw new NotImplementedException();
        }
    }
}
