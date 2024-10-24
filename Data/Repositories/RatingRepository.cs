using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly EventureContext _context;

        public RatingRepository(EventureContext context)
        {
            _context = context;
        }

        // Lägger till en ny rating
        public async Task AddRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        // Tar bort en rating baserat på id
        public async Task DeleteRatingAsync(int ratingId)
        {
            var rating = await _context.Ratings.FindAsync(ratingId);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }

        // Uppdaterar en rating
        public async Task EditRatingAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        // Ej implementerad metod för att hämta alla ratings (kan användas senare, osäker just nu)
        public Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            throw new NotImplementedException();
        }

        // Hämtar alla ratings för en viss aktivitet
        public async Task<IEnumerable<Rating>> GetAllRatingsByActivityAsync(int activityId)
        {
            return await _context.Ratings
                .Include(r => r.Activity)
                .Where(r => r.Activity.ActivityId == activityId)
                .ToListAsync();
        }

        // Hämtar en specifik rating med id
        public async Task<Rating> GetRatingByIdAsync(int ratingId)
        {
            return await _context.Ratings
                .Include(r => r.Activity)
                .FirstOrDefaultAsync(r => r.RatingId == ratingId);
        }

        // Hämtar medelvärdet av ratings för en aktivitet
        public async Task<double> GetAverageRatingForActivityAsync(int activityId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.ActivityId == activityId)
                .ToListAsync();

            if (ratings.Count == 0) return 0;

            return ratings.Average(r => r.Score) ?? 0;
        }

    }
}
