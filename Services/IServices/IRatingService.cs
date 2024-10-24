using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingShowDTO>> GetAllRatingsByActivityAsync(int activityId);
        Task AddRatingAsync(RatingCreateEditDTO ratingDto);
        Task EditRatingAsync(int ratingId, RatingCreateEditDTO ratingDto);
        Task DeleteRatingAsync(int ratingId);
        Task<RatingShowDTO> GetRatingByIdAsync(int ratingId);
        Task<double> GetAverageRatingForActivityAsync(int activityId);
    }
}
