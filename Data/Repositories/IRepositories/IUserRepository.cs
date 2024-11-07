using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task EditUserAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByEmailAsync(string email);
        Task AddCategoryToUserAsync(string userId, int categoryId);
        Task<IEnumerable<Category>> GetUserPreferencesAsync(string userId);
        Task AddUserEvent(string userId, int activityId);
        Task<IEnumerable<UserEvent>> GetAllUserEventsAsync();
        Task<UserEvent> GetUserEventByIdAsync(int userEventId);
        Task DeleteUserEventAsync(UserEvent userEvent);
    }
}
