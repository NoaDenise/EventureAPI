using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;

namespace EventureAPI.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        // Lägger till en ny rating
        public async Task AddRatingAsync(RatingCreateEditDTO ratingDto)
        {
            var newRating = new Rating
            {
                UserId = ratingDto.UserId,
                ActivityId = ratingDto.ActivityId,
                Score = ratingDto.Score,
            };
            await _ratingRepository.AddRatingAsync(newRating);
        }

        // Tar bort en rating baserat på id
        public async Task DeleteRatingAsync(int ratingId)
        {
            await _ratingRepository.DeleteRatingAsync(ratingId);
        }

        // Uppdaterar en rating med nytt betyg
        public async Task EditRatingAsync(int ratingId, RatingCreateEditDTO ratingDto)
        {
            var rating = await _ratingRepository.GetRatingByIdAsync(ratingId);
            rating.Score = ratingDto.Score;
            await _ratingRepository.EditRatingAsync(rating);
        }

        // Hämtar alla ratings för en viss aktivitet
        public async Task<IEnumerable<RatingShowDTO>> GetAllRatingsByActivityAsync(int activityId)
        {
            var ratings = await _ratingRepository.GetAllRatingsByActivityAsync(activityId);
            return ratings.Select(r => new RatingShowDTO
            {
                ActivityName = r.Activity.ActivityName,
                Score = r.Score
            }).ToList();
        }

        // Hämtar en specifik rating baserat på id
        public async Task<RatingShowDTO> GetRatingByIdAsync(int ratingId)
        {
            var rating = await _ratingRepository.GetRatingByIdAsync(ratingId);
            return new RatingShowDTO
            {
                ActivityName = rating.Activity.ActivityName,
                Score = rating.Score
            };
        }
    }
}
