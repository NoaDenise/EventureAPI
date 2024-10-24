using EventureAPI.Models.DTOs;
using EventureAPI.Services;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // Lägger till en ny rating
        [HttpPost("addRating")]
        public async Task<ActionResult> AddRating(RatingCreateEditDTO ratingDto)
        {
            await _ratingService.AddRatingAsync(ratingDto);
            return Ok("Rating added successfully.");
        }

        // Tar bort en rating med id
        [HttpDelete("deleteRating/{ratingId}")]
        public async Task<ActionResult> DeleteRating(int ratingId)
        {
            await _ratingService.DeleteRatingAsync(ratingId);
            return Ok($"Rating with id:{ratingId} has been deleted");
        }

        // Uppdaterar en rating med id
        [HttpPut("editRating/{ratingId}")]
        public async Task<ActionResult> EditRating(int ratingId, RatingCreateEditDTO ratingDto)
        {
            await _ratingService.EditRatingAsync(ratingId, ratingDto);
            return Ok("Rating updated successfully.");
        }

        // Hämtar alla ratings för en aktivitet
        [HttpGet("getAllRatingsByActivity/{activityId}")]
        public async Task<ActionResult> GetAllRatingsByActivity(int activityId)
        {
            var ratings = await _ratingService.GetAllRatingsByActivityAsync(activityId);
            return Ok(ratings);
        }

        // Hämtar en rating med id
        [HttpGet("getRatingById/{ratingId}")]
        public async Task<ActionResult> GetRatingById(int ratingId)
        {
            var rating = await _ratingService.GetRatingByIdAsync(ratingId);
            return Ok(rating);
        }

        // Hämtar medelvärdet av ratings för en aktivitet
        [HttpGet("getAverageRatingForActivity/{activityId}")]
        public async Task<ActionResult<double>> GetAverageRatingForActivity(int activityId)
        {
            var averageRating = await _ratingService.GetAverageRatingForActivityAsync(activityId);
            return Ok(averageRating);
        }

    }
}
