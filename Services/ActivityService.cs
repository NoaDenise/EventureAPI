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

        public async Task<IEnumerable<ActivityShowDTO>> GetAll18PlusActivitiesAsync(bool is18Plus)
        {
            try
            {
                var activities = await _activityRepository.GetAll18PlusActivitiesAsync(is18Plus);

                // Map to DTO (Assuming you have a mapping function)
                return activities.Select(a => new ActivityShowDTO
                {
                    // Map properties from Activity to ActivityShowDTO

                    ActivityName = a.ActivityName,
                    ActivityDescription = a.ActivityDescription,
                    DateOfActivity = a.DateOfActivity,
                    ActivityLocation = a.ActivityLocation,
                    ImageUrl = a.ImageUrl,
                    WebsiteUrl = a.WebsiteUrl,
                    ContactInfo = a.ContactInfo
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving activities for 18+. " + ex.Message);
            }

        }

        public Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ActivityShowAdminDTO>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved)
        {
            try
            {
                var activities = await _activityRepository.GetAllActivitiesAwaitingApprovalAsync(isApproved);                return activities.Select(a => new ActivityShowAdminDTO
                {
                    ActivityId = a.ActivityId,
                    UserId = a.UserId,
                    ActivityName = a.ActivityName,
                    ActivityDescription = a.ActivityDescription,
                    DateOfActivity = a.DateOfActivity,
                    ActivityLocation = a.ActivityLocation,
                    ImageUrl = a.ImageUrl,
                    WebsiteUrl = a.WebsiteUrl,
                    ContactInfo = a.ContactInfo,
                    IsApproved = a.IsApproved,
                    IsFree = a.IsFree,
                    Is18Plus = a.Is18Plus
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving activities awaiting approval. " + ex.Message);
            }

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

        public async Task<IEnumerable<ActivityShowDTO>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly)
        {
            try
            {
                var activities = await _activityRepository.GetAllFamilyFriendlyActivitiesAsync(isFamilyFriendly);
                return activities.Select(a => new ActivityShowDTO
                {
                    ActivityName = a.ActivityName,
                    ActivityDescription = a.ActivityDescription,
                    DateOfActivity = a.DateOfActivity,
                    ActivityLocation = a.ActivityLocation,
                    ImageUrl = a.ImageUrl,
                    WebsiteUrl = a.WebsiteUrl,
                    ContactInfo = a.ContactInfo
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving family-friendly activities. " + ex.Message);
            }
        }

        public async Task<IEnumerable<ActivityShowDTO>> GetAllFreeActivitiesAsync(bool isFree)
        {
            try
            {
                var activities = await _activityRepository.GetAllFreeActivitiesAsync(isFree);
                return activities.Select(a => new ActivityShowDTO
                {
                    ActivityName = a.ActivityName,
                    ActivityDescription = a.ActivityDescription,
                    DateOfActivity = a.DateOfActivity,
                    ActivityLocation = a.ActivityLocation,
                    ImageUrl = a.ImageUrl,
                    WebsiteUrl = a.WebsiteUrl,
                    ContactInfo = a.ContactInfo
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving free activities. " + ex.Message);
            }

        }
    }
}
