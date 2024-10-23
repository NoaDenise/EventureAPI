using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendanceAsync();
        Task AddAttendanceAsync(Attendance attendance);
        Task EditAttendanceAsync(Attendance attendance);
        Task DeleteAttendanceAsync(Attendance attendance);
        Task<Attendance> GetAttendanceByIdAsync(int attendanceId);
        Task<IEnumerable<Attendance>> GetAttendanceByActivityAsync(int activityId);
    }
}
