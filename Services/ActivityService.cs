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

        // Changed to Other dto that includes the filter things.
        public async Task<IEnumerable<ActivityFilteredDTO>> GetAllActivitiesAsync()
        {
            var allActivities = await _activityRepository.GetAllActivitiesAsync();

            return allActivities.Select(a => new ActivityFilteredDTO
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
                ContactInfo = activityDto.ContactInfo,
                IsFree = activityDto.IsFree,
                Is18Plus = activityDto.Is18Plus,
                IsFamilyFriendly = activityDto.IsFamilyFriendly,
                IsApproved = false
            };

            await _activityRepository.AddActivityAsync(newActivity);
        }

        public async Task DeleteActivityAsync(int activityId)
        {
            // Hämta aktiviteten och ta bort den
            var activity = _activityRepository.GetActivityByIdAsync(activityId).Result;
            if (activity == null) throw new KeyNotFoundException("Activity not found.");

            await _activityRepository.DeleteActivityAsync(activity);
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

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            var chosenActivity = await _activityRepository.GetActivityByIdAsync(activityId);

            if (chosenActivity == null)
            {
                throw new Exception($"Activity with ID {activityId} not found.");
            }
            return chosenActivity;

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

        public async Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByCategoryAsync(int categoryId)
        {
            var activities = await _activityRepository.GetAllActivitiesByCategoryAsync(categoryId);

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

        public async Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByLocationAsync(string location)
        {
            var activities = await _activityRepository.GetAllActivitiesByLocationAsync(location);

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

        public async Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId)
        {
            var activities = await _activityRepository.GetAllActivitiesByCategoryAsync(userCategoryId);

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

        public Task<IQueryable<Activity>> GetActivitiesQueryableAsync()
        {
            return _activityRepository.GetActivitiesQueryableAsync();
        }
    }
}
