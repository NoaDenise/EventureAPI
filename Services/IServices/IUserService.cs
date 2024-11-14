using EventureAPI.Models;
using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IUserService
    {

        Task<IEnumerable<UserShowDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserCreateEditDTO userDto);
        Task EditUserAsync(string userId, UserCreateEditDTO userDto);
        Task EditAdminInfoAsync(string userId, AdminEditInfoDTO adminEditInfoDTO);
        Task EditAdminPasswordAsync(string userId, AdminEditPasswordDTO adminEditPasswordDTO);
        Task DeleteUserAsync(string userId);
        Task<UserShowDTO> GetUserByIdAsync(string userId);
        Task<string> LoginAsync(string email, string password);
        Task RegisterAsync(string firstName, string lastName, string userLocation, string userName, string email, string phoneNumber, string password, string role);
        Task AddCategoryToUserAsync(string userId, int categoryId);
        Task<IEnumerable<Category>> GetUserPreferencesAsync(string userId);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        // Assigns a role to a user
        Task AssignRoleToUserAsync(string userId, string role);
        Task<int> AddUserEvent(string userId, int activityId);
        Task<IEnumerable<int>> GetLikedActivities(string userId);
        Task<IEnumerable<UserEventMyPagesDTO>> GetUsersSavedEventsAsync(string userId);
        Task<UserEventMyPagesDTO> GetUserEventByIdAsync(int userEventId);
        Task DeleteUserEventAsync(int userEventId);
        Task<IEnumerable<UserEventMyPagesDTO>> GetUserEventsByCategory(int categoryId);

        Task<bool> RemoveUserEvent(string userId, int activityId);
    }
}
