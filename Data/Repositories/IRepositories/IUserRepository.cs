using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task EditUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
    }
}
