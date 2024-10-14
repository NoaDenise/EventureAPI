using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();

        Task AddRatingAsync(Rating rating);

        Task EditRatingAsync(Rating rating);

        Task DeleteRatingAsync(Rating rating);

        Task<Rating> GetRatingByIdAsync(int ratingId);
    }
}
