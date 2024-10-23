using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var token = await _userService.LoginAsync(login.Email, login.Password);
            if (token != null)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO regUser)
        {
            await _userService.RegisterAsync(regUser.FirstName, regUser.LastName, regUser.UserLocation, regUser.UserName, regUser.Email, regUser.PhoneNumber, regUser.Password);
            return Ok();
        }
    }
}
