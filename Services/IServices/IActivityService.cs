using EventureAPI.Models;
using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IActivityService
    {
        //Task<IEnumerable<ActivityShowAdminDTO>> GetAllActivitiesAsync();
        Task<IEnumerable<ActivityFilteredDTO>> GetAllActivitiesAsync();
        Task AddActivityAsync(ActivityCreateEditDTO activityDto);
        Task DeleteActivityAsync(int activityId);
        Task EditActivityAsync(int activityId, ActivityCreateEditDTO activityDto);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByCategoryAsync(int categoryId);
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByLocationAsync(string location);
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId);

        Task<IEnumerable<ActivityShowDTO>> GetAllFreeActivitiesAsync(bool isFree);
        Task<IEnumerable<ActivityShowDTO>> GetAll18PlusActivitiesAsync(bool is18Plus);

        Task<IEnumerable<ActivityShowDTO>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly);
        Task<IEnumerable<ActivityShowAdminDTO>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved);
        Task ApproveActivityAsync(int activityId);

        // ny metod för att använda query i sökning
        Task<IQueryable<Activity>> GetActivitiesQueryableAsync();

    }
}
