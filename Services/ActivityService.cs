using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;

namespace EventureAPI.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        private readonly List<Activity> _activities = new List<Activity>
    {
        new Activity
        {
            ActivityId = 1,
            UserId = "exampleUserId",
            ActivityName = "Hiking Adventure",
            ActivityDescription = "Join us for a fun hiking adventure in the mountains!",
            DateOfActivity = DateTime.Now.AddDays(10),
            ActivityLocation = "Mountain Trail",
            IsApproved = true,
            IsFree = false,
            Is18Plus = false,
            IsFamilyFriendly = true
        },
        new Activity
        {
            ActivityId = 2,
            UserId = "exampleUserId",
            ActivityName = "Cooking Class",
            ActivityDescription = "Learn to cook delicious meals.",
            DateOfActivity = DateTime.Now.AddDays(5),
            ActivityLocation = "Culinary School",
            IsApproved = true,
            IsFree = true,
            Is18Plus = false,
            IsFamilyFriendly = true
        }
    };

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Task AddActivityAsync(ActivityCreateEditDTO activityDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivity(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task EditActivityAsync(int activityId, ActivityCreateEditDTO activityDto)
        {
            throw new NotImplementedException();
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
