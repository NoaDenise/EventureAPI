using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserShowDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserCreateEditDTO userDto);
        Task EditUserAsync(int userId, UserCreateEditDTO userDto);
        Task DeleteUserAsync(int userId);
        Task<UserShowDTO> GetUserByIdAsync(int userId);
    }
}
