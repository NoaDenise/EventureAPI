using EventureAPI.Data;
using EventureAPI.Data.Repositories;
using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
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

        // GET: api/activity/18plus
        [HttpGet("18plus")]
        public async Task<IActionResult> GetAll18PlusActivities([FromQuery] bool is18Plus)
        {
            try
            {
                var activities = await _activityService.GetAll18PlusActivitiesAsync(is18Plus);
                return Ok(activities); // Return 200 OK with the list of activities
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error
            }
        }
        // GET: api/activity/awaitingApproval
        [HttpGet("awaitingApproval")]
        public async Task<IActionResult> GetAllActivitiesAwaitingApproval([FromQuery] bool isApproved)
        {
            try
            {
                var activities = await _activityService.GetAllActivitiesAwaitingApprovalAsync(isApproved);
                return Ok(activities); // Return 200 OK with the list of activities
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error
            }
        }

        // GET: api/activity/familyFriendly
        [HttpGet("familyFriendly")]
        public async Task<IActionResult> GetAllFamilyFriendlyActivities([FromQuery] bool isFamilyFriendly)
        {
            try
            {
                var activities = await _activityService.GetAllFamilyFriendlyActivitiesAsync(isFamilyFriendly);
                return Ok(activities); // Return 200 OK with the list of activities
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error
            }
        }

        // GET: api/activity/free
        [HttpGet("free")]
        public async Task<IActionResult> GetAllFreeActivities([FromQuery] bool isFree)
        {
            try
            {
                var activities = await _activityService.GetAllFreeActivitiesAsync(isFree);
                return Ok(activities); // Return 200 OK with the list of activities
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error
            }
        }



        [HttpPost("addActivity")]
        public async Task<ActionResult> AddActivity(ActivityCreateEditDTO activityDto)
        {
            await _activityService.AddActivityAsync(activityDto);
            return Ok("Aktiviteten lades till.");
        }

        [HttpDelete("deleteActivity/{activityId}")]
        public async Task<ActionResult> DeleteActivity(int activityId)
        {
            // Eftersom DeleteActivity inte ska vara async (kontrollera detta)
            await _activityService.DeleteActivityAsync(activityId);
            return Ok($"Aktiviteten med id:{activityId} har raderats.");
        }

        [HttpPut("editActivity/{activityId}")]
        public async Task<ActionResult> EditActivity(int activityId, ActivityCreateEditDTO activityDto)
        {
            await _activityService.EditActivityAsync(activityId, activityDto);
            return Ok("Aktiviteten har uppdaterats.");
        }

        [HttpGet("getAllActivities")]
        public async Task<ActionResult> GetAllActivities()
        {
            var activities = await _activityService.GetAllActivitiesAsync();
            return Ok(activities);
        }

        //Endpoint for filtering, testing atm
        [HttpGet("getFilteredActivities")]
        public async Task<ActionResult> GetAllActivities(
            bool? isFree = null,
            bool? is18Plus = null,
            bool? isFamilyFriendly = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string location = null)
        {
            var activities = await _activityService.GetAllActivitiesAsync();

            if (isFree.HasValue)
            {
                activities = activities.Where(a => a.IsFree == isFree.Value);
            }
            if (is18Plus.HasValue)
            {
                activities = activities.Where(a => a.Is18Plus == is18Plus.Value);
            }
            if (isFamilyFriendly.HasValue)
            {
                activities = activities.Where(a => a.IsFamilyFriendly == isFamilyFriendly.Value);
            }
            if (startDate.HasValue)
            {
                activities = activities.Where(a => a.DateOfActivity >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                activities = activities.Where(a => a.DateOfActivity <= endDate.Value);
            }
            if (!string.IsNullOrEmpty(location))
            {
                activities = activities.Where(a => a.ActivityLocation.ToLower().Contains(location.ToLower()));
            }

            return Ok(activities);
        }
    }
}
