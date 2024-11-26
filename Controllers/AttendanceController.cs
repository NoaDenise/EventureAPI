using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprache;

namespace EventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        //borde finnas en delete som  raderar activity OCH attendance om activity har attendance-datum
        //

        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("getAllAttendance")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllAttendance()
        {
            var attendances = await _attendanceService.GetAllAttendanceAsync();
            return Ok(attendances);
        }

        [HttpPost("addAttendance")]
        public async Task<ActionResult> AddAttendance([FromBody] AttendanceCreateEditDTO attendanceDto)
        {
            //checking all valid field are there
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _attendanceService.AddAttendanceAsync(attendanceDto);
                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request.");
            }
        }

        [HttpGet("getAttendanceById/{attendanceId}")]
        public async Task<ActionResult<AttendanceCreateEditDTO>> GetAttendanceById(int attendanceId)
        {
            if (attendanceId == null)
            {
                return BadRequest("Input attendance ID, please.");
            }

            var attendance = await _attendanceService.GetAttendanceByIdAsync(attendanceId);

            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        [HttpDelete("deleteAttendance/{attendancId}")]
        public async Task<ActionResult> DeleteAttendance(int attendancId)
        {
            if (attendancId == null)
            {
                return BadRequest("Input attendance ID, please.");
            }

            try
            {
                await _attendanceService.DeleteAttendanceAsync(attendancId);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpPut("editAttendance/{attendanceId}")]
        public async Task<ActionResult> EditAttendance(int attendanceId, [FromBody] AttendanceCreateEditDTO attendanceDto)
        {
            if (attendanceId == null)
            {
                return BadRequest("Input attendance ID, please.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _attendanceService.EditAttendanceAsync(attendanceId, attendanceDto);
                return NoContent();//code 204 if nothing should be returned
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpGet("getAttendanceByActivity/{activityId}")]
        public async Task<ActionResult<IEnumerable<AttendanceShowDTO>>> GetAttendanceByActivity(int activityId)
        {
            if (activityId == null)
            {
                return BadRequest("Input activity ID, please");
            }

            var attendance = await _attendanceService.GetAttendanceByActivityAsync(activityId);

            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        [HttpGet("getUsersAttendance/{userId}")]
        public async Task<ActionResult<IEnumerable<AttendanceShowDTO>>> GetUsersAttendance(string userId)
        {
            var attendance = await _attendanceService.GetUsersAttendanceAsync(userId);

            if (userId == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        [HttpGet("checkIfAttendanceExists/{userId}, {activityId}")]
        public async Task<ActionResult> AttendanceExists(string userId, int activityId)
        {
            var attendanceExists = await _attendanceService.AttendanceExistsAsync(userId, activityId);

            return Ok(attendanceExists);
        }
    }
}
