using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly EventureContext _context;

        public AttendanceRepository(EventureContext context)
        {
            _context = context;
        }
        public async Task AddAttendanceAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAttendanceAsync(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task EditAttendanceAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendanceAsync()
        {
            return await _context.Attendances.Include(a => a.User).Include(a => a.Activity).ToListAsync();
        }


        public async Task<Attendance> GetAttendanceByIdAsync(int attendanceId)
        {
            return await _context.Attendances.Include(a => a.User).Include(a => a.Activity).SingleOrDefaultAsync(a => a.AttendanceId == attendanceId);
        }

        public async Task<IEnumerable<Attendance>> GetAttendanceByActivityAsync(int activityId)
        {
            return await _context.Attendances.Include(a => a.User).Include(a => a.Activity).Where(a => a.ActivityId == activityId).ToListAsync();
        }

        //only need info from activity-model, so only include of activity
        public async Task<IEnumerable<Attendance>> GetUsersAttendanceAsync(string userId)
        {
            return await _context.Attendances.Include(a => a.Activity).Where(a => a.UserId == userId).ToListAsync();
        }
    }
}
