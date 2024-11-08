using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace EventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UserShowDTO>>> GetAllUsers()
        {
            var allUsers = await _userService.GetAllUsersAsync();
            return Ok(allUsers);
        }

        // For manually creating users as admin etc.
        [HttpPost("addUser")]
        public async Task<ActionResult> AddUser(UserCreateEditDTO user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpDelete("deleteUser/{userId}")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }

        [HttpPut("editUser/{userId}")]
        public async Task<ActionResult> EditUser(string userId, UserCreateEditDTO user)
        {
            await _userService.EditUserAsync(userId, user);
            return Ok();
        }

        [HttpGet("getUserById/{userId}")]
        public async Task<ActionResult<UserShowDTO>> GetUserById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            // Call the UserService to authenticate the user and generate a JWT token
            var token = await _userService.LoginAsync(login.Email, login.Password);
            if (token != null)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO regUser, string role)
        {
            try
            {

                await _userService.RegisterAsync(
                regUser.FirstName,
                regUser.LastName,
                regUser.UserLocation,
                regUser.UserName,
                regUser.Email,
                regUser.PhoneNumber,
                regUser.Password,
                role
                );
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        // This method assigns a new role to a user
        [HttpPost("{userId}/assignRole")]
        public async Task<IActionResult> AssignRoleToUser(string userId, [FromBody] string role)
        {
            try
            {
                // Attempt to assign the role to the user via the UserService
                await _userService.AssignRoleToUserAsync(userId, role);
                return Ok(new { message = "Role assigned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]// Only authorized users can access this endpoint
        [HttpGet("getRole")]
        public async Task<IActionResult> GetUserRole()
        {
            // Retrieve the user ID from the JWT token (which is automatically provided by the authentication system)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // If the user ID is not found in the JWT token, return an Unauthorized response
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }

            // Call the UserService to get the roles associated with the user ID
            var roles = await _userService.GetUserRolesAsync(userId);

            if (roles == null || !roles.Any())
            {
                return NotFound("Roles not found for the user.");
            }

            // Return the first role from the roles list (if the user has multiple roles, this can be adjusted)
            return Ok(new { role = roles.First() });
        }

        // POST: api/user/{userId}/categories/{categoryId}
        [HttpPost("{userId}/categories/{categoryId}")]
        public async Task<IActionResult> AddCategoryToUser(string userId, int categoryId)
        {
            try
            {
                await _userService.AddCategoryToUserAsync(userId, categoryId);
                return Ok(new { message = "Category added to user's preferences successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{userId}/preferences")]
        public async Task<IActionResult> GetUserPreferences(string userId)
        {
            var preferences = await _userService.GetUserPreferencesAsync(userId);
            if (preferences == null || !preferences.Any())
            {
                return NotFound("No preferences found for this user.");
            }

            return Ok(preferences);
        }
       

    }
}
