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

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync() 
        {
            var allActivities = await _context.Activities.ToListAsync(); 
            return allActivities;
        }

        // 1. Lägg till en ny aktivitet
        public async Task AddActivityAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();  
        }

        // 2. Ta bort en aktivitet (ska denna va asynkron?)
        public async Task DeleteActivityAsync(Activity activity)
        {
            //had to have filter before adding navigationproperties
            //now we will be able to delete activity (since comments, attendances and ratings are connected to activity)
            var activityToDelete = _context.Activities    
                .Where(a => a.ActivityId == activity.ActivityId)
                .Include(a => a.Attendances)
                .Include(a => a.Comments)
                .Include(a => a.Ratings)
                .FirstOrDefault();

            if (activityToDelete == null)
            {
                throw new Exception("Activity not found");
            }

            //added the other connections to be removed before removing the activity itself
            _context.Attendances.RemoveRange(activityToDelete.Attendances);
            _context.Comments.RemoveRange(activityToDelete.Comments);
            _context.Ratings.RemoveRange(activityToDelete.Ratings);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();  // Sparar förändringar synkront eftersom det inte är async
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

        public async Task<IEnumerable<Activity>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly)
        {
            if (!isFamilyFriendly)
            {
                return null;
            }

            return await _context.Activities
                .Where (a => a.IsFamilyFriendly)
                .ToListAsync();
        }


        public async Task ApproveActivityAsync(int activityId)
        {
            //find the specific activity to update
            var activity = await _context.Activities.FindAsync(activityId);
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Activity>> GetActivitiesQueryableAsync()
        {
            return _context.Activities.AsQueryable();

        }

        public async Task<IEnumerable<ActivityCategory>> GetAllActivityCategoriesAsync()
        {
            return await _context.ActivityCategories.Include(a => a.Activity).Include(a => a.Category).ToListAsync();
        }

        public async Task<IEnumerable<ActivityCategory>> GetActivitysCategoriesAsync(int activityId)
        {
            return await _context.ActivityCategories.Include(a => a.Category).Where(a => a.ActivityId == activityId).ToListAsync();
        }

        public async Task<ActivityCategory> GetActivityCategoryByIdAsync(int activityCategoryId)
        {
            return await _context.ActivityCategories.Include(a => a.Activity).Include(a => a.Category).SingleOrDefaultAsync(a => a.ActivityCategoryId == activityCategoryId);
        }

        public async Task AddActivityCategoryAsync(ActivityCategory activityCategory)
        {
            await _context.ActivityCategories.AddAsync(activityCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActivityCategoryAsync(ActivityCategory activityCategory)
        {
            _context.ActivityCategories.Remove(activityCategory);
            await _context.SaveChangesAsync();
        }
    }
}
