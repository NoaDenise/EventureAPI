using EventureAPI.Models;


namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task AddActivityAsync(Activity activity);
        Task DeleteActivityAsync(Activity activity);
        Task EditActivityAsync(Activity activity);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<IEnumerable<Activity>> GetAllActivitiesByCategoryAsync(int categoryId);
        Task<IEnumerable<Activity>> GetAllActivitiesByLocationAsync(string location);
        Task<IEnumerable<Activity>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId);
        Task<IEnumerable<Activity>> GetAllFreeActivitiesAsync(bool isFree);
        Task<IEnumerable<Activity>> GetAll18PlusActivitiesAsync(bool is18Plus);
        Task<IEnumerable<Activity>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly);
        Task<IEnumerable<Activity>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved);
        Task ApproveActivityAsync(int activityId);
        Task<IQueryable<Activity>> GetActivitiesQueryableAsync();
        Task<IEnumerable<ActivityCategory>> GetAllActivityCategoriesAsync();
        Task<IEnumerable<ActivityCategory>> GetActivitysCategoriesAsync(int activityId);
        Task<ActivityCategory> GetActivityCategoryByIdAsync(int activityCategoryId);
        Task AddActivityCategoryAsync(ActivityCategory activityCategory);
        Task DeleteActivityCategoryAsync(ActivityCategory activityCategory);
    }
}
