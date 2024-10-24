﻿using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventureAPI.Services
{
    public class UserService : IUserService
    {
        //Using the User and SigninManager from Identity
        private readonly IUserRepository _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepo, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
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

        public async Task DeleteUserAsync(string userId)
        {
            await _userRepo.DeleteUserAsync(userId);
        }

        public async Task EditUserAsync(string userId, UserCreateEditDTO userDto)
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

        public async Task<UserShowDTO> GetUserByIdAsync(string userId)
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

        public async Task<string> LoginAsync(string email, string password)
        {
            // First, retrieve the user by email
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;  // User doesn't exist
            }

            // Use the user object for password sign-in
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // If login is successful, generate a JWT token
                var token = GenerateJwtToken(user);
                return token;  // Return the generated token
            }
            // If login fails
            return null;
        }


        public async Task RegisterAsync(string firstName, string lastName, string userLocation, string userName, string email, string phoneNumber, string password)
        {
            var user = new User {FirstName = firstName, LastName = lastName, UserLocation = userLocation, UserName = userName, Email = email, PhoneNumber = phoneNumber };
            var result = await _userManager.CreateAsync(user, password);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),  // Set an appropriate expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
