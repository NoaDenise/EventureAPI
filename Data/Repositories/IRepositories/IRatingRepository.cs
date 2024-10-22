using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRatingsByActivityAsync(int activityId);
        Task AddRatingAsync(Rating rating);
        Task EditRatingAsync(Rating rating);
        Task DeleteRatingAsync(int ratingId);
        Task<Rating> GetRatingByIdAsync(int ratingId);
    }
}
