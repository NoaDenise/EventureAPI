using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult DeleteActivity(int activityId)
        {
            // Eftersom DeleteActivity inte ska vara async (kontrollera detta)
            _activityService.DeleteActivity(activityId);
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

        [HttpGet("getActivityById/{activityId}")]
        public async Task<ActionResult> GetActivityById(int activityId)
        {
            var activity = await _activityService.GetActivityByIdAsync(activityId);
            return Ok(activity);
        }

    }
}
