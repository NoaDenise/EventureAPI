using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace EventureAPI.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly EventureContext _context;

        public ActivityRepository(EventureContext context)
        {
            _context = context;
        }


        // 1. Lägg till en ny aktivitet
        public async Task AddActivityAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();  
        }

        // 2. Ta bort en aktivitet (ska denna va asynkron?)
        public void DeleteActivity(Activity activity)
        {
            _context.Activities.Remove(activity);
            _context.SaveChanges();  // Sparar förändringar synkront eftersom det inte är async
        }

        // 3. Redigera en aktivitet
        public async Task EditActivityAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();  
        }

        // 4. Hämta en aktivitet baserat på ID
        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(a => a.ActivityId == activityId);  // Hämtar aktivitet baserat på ID
        }


        public async Task<IEnumerable<Activity>> GetAll18PlusActivitiesAsync(bool is18Plus)
        {
            // If is18Plus is false, return an empty list
            if (!is18Plus)
            {
                return Enumerable.Empty<Activity>(); // Returns an empty list if not 18+
            }
                        return await _context.Activities
                .Where(a => a.Is18Plus) // Filter for 18+ activities
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByCategoryAsync(int categoryId)
        {
            //include tables to find categoryId
            return await _context.Activities
                .Include(a => a.ActivityCategories)
                .ThenInclude(ac => ac.Category)
                .Where(a => a.ActivityCategories.Any(ac => ac.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByLocationAsync(string location)
        {
            return await _context.Activities.Where(a => a.ActivityLocation == location).ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId)
        {
            //finding a user's preferences by filtering among userCategories
            return await _context.Activities
                .Include(a => a.ActivityCategories)
                .ThenInclude(a => a.Category)
                .Where(a => a.ActivityCategories
                .Any(a => a.Category.UserCategories
                .Any(a => a.UserCategoryId == userCategoryId)))
                .ToListAsync();
        }



        public async Task<IEnumerable<Activity>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved)
        {

            if (isApproved)
            {
                return await _context.Activities
                    .Where(a => !a.IsApproved) // Filter for activities that are awaiting approval
                    .ToListAsync();
            }

            return await _context.Activities
                .Where(a => a.IsApproved) // Filter for approved activities
                .ToListAsync();

        }

        public async Task<IEnumerable<Activity>> GetAllFreeActivitiesAsync(bool isFree)
        {

            // If isFree is false, return an empty list
            if (!isFree)
            {
                return Enumerable.Empty<Activity>(); // Returns an empty list if not free
            }

            return await _context.Activities
                .Where(a => a.IsFree) // Filter for free activities
                .ToListAsync();


           
        }

    }
}
