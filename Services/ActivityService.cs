using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;

namespace EventureAPI.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Task AddActivityAsync(ActivityCreateEditDTO activityDto)
        {
            throw new NotImplementedException();
        }

        public Task AddActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivity(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task EditActivityAsync(int activityId, ActivityCreateEditDTO activityDto)
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

        public Task<IEnumerable<ActivityShowDTO>> GetAll18PlusActivitiesAsync(bool is18Plus)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowAdminDTO>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByLocationAsync(string location)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllFreeActivitiesAsync(bool isFree)
        {
            throw new NotImplementedException();
        }
    }
}
