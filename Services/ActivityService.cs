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


        //Q needed to add Id and location and bools for MVC, so used AShowAdminDto
        //       public async Task<IEnumerable<ActivityShowAdminDTO>> GetAllActivitiesAsync()
        //{
        //    var allActivities = await _activityRepository.GetAllActivitiesAsync();

        //    return allActivities.Select(a => new ActivityShowAdminDTO
        //    {
        //        ActivityId = a.ActivityId,
        //        ActivityName = a.ActivityName,
        //        ActivityDescription = a.ActivityDescription,
        //        ActivityLocation = a.ActivityLocation,
        //        DateOfActivity = a.DateOfActivity,
        //        ImageUrl = a.ImageUrl,
        //        WebsiteUrl = a.WebsiteUrl,
        //        ContactInfo = a.ContactInfo,
        //        IsFree = a.IsFree,
        //        Is18Plus = a.Is18Plus,
        //        IsFamilyFriendly = a.IsFamilyFriendly
        //    }).ToList();
        //}

        // Changed to Other dto that includes the filter things.
        public async Task<IEnumerable<ActivityFilteredDTO>> GetAllActivitiesAsync()
        {
            var allActivities = await _activityRepository.GetAllActivitiesAsync();

            return allActivities.Select(a => new ActivityFilteredDTO

            {
                ActivityId = a.ActivityId,
                ActivityName = a.ActivityName,
                ActivityDescription = a.ActivityDescription,
                ActivityLocation = a.ActivityLocation,
                DateOfActivity = a.DateOfActivity,
                ImageUrl = a.ImageUrl,
                WebsiteUrl = a.WebsiteUrl,
                ContactInfo = a.ContactInfo,
                IsFree = a.IsFree,
                Is18Plus = a.Is18Plus,
                IsFamilyFriendly = a.IsFamilyFriendly,
                IsApproved = a.IsApproved
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

        //Q edited endpoint to match mvc
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
            activity.IsApproved = activity.IsApproved;
            activity.IsFree = activityDto.IsFree;
            activity.Is18Plus = activityDto.Is18Plus;
            activity.IsFamilyFriendly = activityDto.IsFamilyFriendly;

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
                var activities = await _activityRepository.GetAllActivitiesAwaitingApprovalAsync(isApproved); return activities.Select(a => new ActivityShowAdminDTO
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


        public async Task ApproveActivityAsync(int activityId)
        {
            var activity = await _activityRepository.GetActivityByIdAsync(activityId);
            if (activity == null)
            {
                throw new Exception("Activity not found");
            }

            //when approving the activity in MVC, the false turns into true
            activity.IsApproved = true;
            await _activityRepository.EditActivityAsync(activity);

        }
        public Task<IQueryable<Activity>> GetActivitiesQueryableAsync()
        {
            return _activityRepository.GetActivitiesQueryableAsync();

        }

        public async Task<IEnumerable<ActivityCategoryShowDTO>> GetAllActivityCategoriesAsync()
        {
            var activityCategories = await _activityRepository.GetAllActivityCategoriesAsync();

            return activityCategories.Select(a => new ActivityCategoryShowDTO
            {
               ActivityCategoryId = a.ActivityCategoryId,
               ActivityId = a.ActivityId,
               ActivityName = a.Activity.ActivityName,
               CategoryId = a.Category.CategoryId,
               CategoryName = a.Category.CategoryName
            }).ToList();
        }

        public async Task<IEnumerable<ActivityCategoryShowCategoriesDTO>> GetActivitysCategoriesAsync(int activityId)
        {
            var activitysCategories = await _activityRepository.GetActivitysCategoriesAsync(activityId);

            return activitysCategories.Select(a => new ActivityCategoryShowCategoriesDTO
            {
                CategoryId = a.CategoryId,
                CategoryName= a.Category.CategoryName
            }).ToList();
        }

        public async Task<ActivityCategoryShowDTO> GetActivityCategoryByIdAsync(int activityCategoryId)
        {
            var chosenActivityCategory = await _activityRepository.GetActivityCategoryByIdAsync(activityCategoryId);

            if (chosenActivityCategory == null)
            {
                throw new Exception("Cannot find connection between specific activity and a category.");
            }

            return new ActivityCategoryShowDTO
            {
                ActivityCategoryId = chosenActivityCategory.ActivityCategoryId,
                ActivityId = chosenActivityCategory.Activity.ActivityId,
                ActivityName = chosenActivityCategory.Activity.ActivityName,
                CategoryId = chosenActivityCategory.Category.CategoryId,
                CategoryName = chosenActivityCategory.Category.CategoryName
            };

        }

        public async Task AddActivityCategoryAsync(ActivityCategoryCreateDTO activityCategoryCreateDTO)
        {
            var newActivityCategory = new ActivityCategory
            {
                ActivityId = activityCategoryCreateDTO.ActivityId,
                CategoryId = activityCategoryCreateDTO.CategoryId
            };

            await _activityRepository.AddActivityCategoryAsync(newActivityCategory);
        }

        public async Task DeleteActivityCategoryAsync(int activityCategoryId)
        {
            var activityCategoryToDelete = await _activityRepository.GetActivityCategoryByIdAsync(activityCategoryId);

            if (activityCategoryToDelete == null)
            {
                throw new Exception($"Activity in combination with category with ID {activityCategoryId} not found");
            }

            await _activityRepository.DeleteActivityCategoryAsync(activityCategoryToDelete);
        }
    }
}
