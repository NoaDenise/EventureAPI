﻿using EventureAPI.Models;
using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesAsync();
        Task AddActivityAsync(ActivityCreateEditDTO activityDto);
        Task DeleteActivity(int activityId);
        Task EditActivityAsync(int activityId, ActivityCreateEditDTO activityDto);
        Task AddActivityAsync(Activity activity);
        Task DeleteActivity(Activity activity);
        Task EditActivityAsync(Activity activity);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByCategoryAsync(int categoryId);
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByLocationAsync(string location);
        Task<IEnumerable<ActivityShowDTO>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId);

        Task<IEnumerable<ActivityShowDTO>> GetAllFreeActivitiesAsync(bool isFree);
        Task<IEnumerable<ActivityShowDTO>> GetAll18PlusActivitiesAsync(bool is18Plus);

        Task<IEnumerable<ActivityShowDTO>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly);
        Task<IEnumerable<ActivityShowAdminDTO>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved);

    }
}
