using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceShowDTO>> GetAllAttendanceAsync();
        Task AddAttendanceAsync(AttendanceCreateEditDTO attendanceDto);
        Task EditAttendanceAsync(int attendanceId, AttendanceCreateEditDTO attendanceDto);
        Task DeleteAttendanceAsync(int attendanceId);
        Task<AttendanceShowDTO> GetAttendanceByIdAsync(int attendanceId);
    }
}
