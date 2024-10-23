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

        public async Task AddActivityAsync(ActivityCreateEditDTO activityDto)
        {
            var newActivity = new Activity
            {
                UserId = activityDto.UserId,
                ActivityName = activityDto.ActivityName,
                ActivityDescription = activityDto.ActivityDescription,
                DateOfActivity = activityDto.DateOfActivity,
                ActivityLocation = activityDto.ActivityLocation,
                ImageUrl = activityDto.ImageUrl,
                WebsiteUrl = activityDto.WebsiteUrl,
                ContactInfo = activityDto.ContactInfo
            };

            await _activityRepository.AddActivityAsync(newActivity);
        }

        public Task AddActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(int activityId)
        {
            // Hämta aktiviteten och ta bort den
            var activity = _activityRepository.GetActivityByIdAsync(activityId).Result;
            if (activity == null) throw new KeyNotFoundException("Activity not found.");

            _activityRepository.DeleteActivity(activity);
        }

        public Task DeleteActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public async Task EditActivityAsync(int activityId, ActivityCreateEditDTO activityDto)
        {
            var activity = await _activityRepository.GetActivityByIdAsync(activityId);
            if (activity == null) throw new KeyNotFoundException("Activity not found.");

            // Uppdatera fälten
            activity.ActivityName = activityDto.ActivityName;
            activity.ActivityDescription = activityDto.ActivityDescription;
            activity.DateOfActivity = activityDto.DateOfActivity;
            activity.ActivityLocation = activityDto.ActivityLocation;
            activity.ImageUrl = activityDto.ImageUrl;
            activity.WebsiteUrl = activityDto.WebsiteUrl;
            activity.ContactInfo = activityDto.ContactInfo;

            await _activityRepository.EditActivityAsync(activity);
        }

        public Task EditActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            return await _activityRepository.GetActivityByIdAsync(activityId);
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

        Task IActivityService.DeleteActivity(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}
