using EventureAPI.Data;
using EventureAPI.Data.Repositories;
using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;


        public ActivityController(IActivityService activityService, ICategoryService categoryService, IUserService userService)
        {
            _activityService = activityService;
            _categoryService = categoryService;
            _userService = userService;


        }

        [HttpGet("getActivityById/{activityId}")]
        public async Task<ActionResult<ActivityShowDTO>> GetActivityById(int activityId)
        {
            if (activityId == null)
            {
                return BadRequest("Input activity ID, please");
            }

            var activity = await _activityService.GetActivityByIdAsync(activityId);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpGet("getAllActivitiesByCategory/{categoryId}")]
        public async Task<ActionResult<ActivityShowDTO>> GetAllActivitiesByCategory(int categoryId)
        {
            if (categoryId == null)
            {
                return BadRequest("Input category ID, please");
            }

            var activities = await _activityService.GetAllActivitiesByCategoryAsync(categoryId);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        [HttpGet("getAllActivitiesByLocation/{location}")]
        public async Task<ActionResult<ActivityShowDTO>> GetAllActivitiesByLocation(string location)
        {
            if (location == null)
            {
                return BadRequest("Input location, please");
            }

            var activities = await _activityService.GetAllActivitiesByLocationAsync(location);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        [HttpGet("getAllActivitiesByUsersPreference/{userCategoryId}")]
        public async Task<ActionResult<ActivityShowDTO>> GetAllActivitiesByUsersPreferences(int userCategoryId)
        {
            if (userCategoryId == null)
            {
                return BadRequest("Input category ID, please");
            }

            var activities = await _activityService.GetAllActivitiesByUsersPreferencesAsync(userCategoryId);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

    }
}
