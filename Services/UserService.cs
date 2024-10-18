using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace EventureAPI.Services
{
    public class UserService : IUserService
    {
        //Using the User and SigninManager from Identity
        private readonly IUserRepository _userRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(IUserRepository userRepo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<UserShowDTO>> GetAllUsersAsync()
        {
            var allUsers = await _userRepo.GetAllUsersAsync();

            return allUsers.Select(u => new UserShowDTO
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName,
                UserLocation = u.UserLocation,
                //UserCategories = u.UserCategories
            }).ToList();
        }

        // Method for creating a user manually. Using Identity to register and login
        public async Task AddUserAsync(UserCreateEditDTO userDto)
        {
            var newUser = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                UserName = userDto.UserName,
                UserLocation = userDto.UserLocation,
                PasswordHash = userDto.PasswordHash,

            };

            await _userRepo.AddUserAsync(newUser);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepo.DeleteUserAsync(userId);
        }

        public async Task EditUserAsync(int userId, UserCreateEditDTO userDto)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            // Replace the current values with the new ones based on the DTO
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.UserName = userDto.UserName;
            user.UserLocation = userDto.UserLocation;
            user.PasswordHash = userDto.PasswordHash;
            
            await _userRepo.EditUserAsync(user);
        }

        public async Task<UserShowDTO> GetUserByIdAsync(int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return new UserShowDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserLocation = user.UserLocation,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                //UserCategories = user.UserCategories, bugged?
            };
        }
    }
}
