using EventureAPI.Models;
using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IUserService
    {

        Task<IEnumerable<UserShowDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserCreateEditDTO userDto);
        Task EditUserAsync(string userId, UserCreateEditDTO userDto);
        Task DeleteUserAsync(string userId);
        Task<UserShowDTO> GetUserByIdAsync(string userId);
        Task<string> LoginAsync(string email, string password);
        Task RegisterAsync(string firstName, string lastName, string userLocation, string userName, string email, string phoneNumber, string password);
        Task AddCategoryToUserAsync(string userId, int categoryId);
        Task<IEnumerable<Category>> GetUserPreferencesAsync(string userId);


    }
}
