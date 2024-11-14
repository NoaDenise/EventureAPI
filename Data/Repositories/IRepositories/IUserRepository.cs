using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task EditUserAsync(User user);
        Task EditAdminInfoAsync(User user);
        Task EditAdminPasswordAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByEmailAsync(string email);
        Task AddCategoryToUserAsync(string userId, int categoryId);
        Task<IEnumerable<Category>> GetUserPreferencesAsync(string userId);
        Task<int> AddUserEvent(string userId, int activityId);
        Task <IEnumerable<int>> GetLikedActivities(string userId);
        Task<IEnumerable<UserEvent>> GetUsersSavedEventsAsync(string userId);
        Task<UserEvent> GetUserEventByIdAsync(int userEventId);
        Task DeleteUserEventAsync(UserEvent userEvent);
        Task<IEnumerable<UserEvent>> GetUserEventsByCategoryAsync(int categoryId);

        //seperate delete/remove for the like button, uses different inputs
        Task<bool> RemoveUserEvent(string userId, int activityId);
    }
}
