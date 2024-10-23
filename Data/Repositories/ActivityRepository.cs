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

        
        public Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAll18PlusActivitiesAsync(bool is18Plus)
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesAwaitingApprovalAsync(bool isApproved)
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesByLocationAsync(string location)
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAllActivitiesByUsersPreferencesAsync(int userCategoryId)
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAllFamilyFriendlyActivitiesAsync(bool isFamilyFriendly)
        {
            throw new NotImplementedException();  
        }

        public Task<IEnumerable<Activity>> GetAllFreeActivitiesAsync(bool isFree)
        {
            throw new NotImplementedException();  
        }

        Task IActivityRepository.DeleteActivity(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
