using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;

namespace EventureAPI.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IActivityRepository _activityRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IUserRepository userRepository, IActivityRepository activityRepository)
        {
            _attendanceRepository = attendanceRepository;
            _userRepository = userRepository;

        }
        public async Task AddAttendanceAsync(AttendanceCreateEditDTO attendanceDto)
        {
            //var existingAttendance = await _attendanceRepository.Find - måste kolla att user inte redan gjort en atten.

            //if (existingAttendance != null)
            //{
            //    throw new Exception("User ia already attending")
            //}

            var newAttendance = new Attendance
            {
                UserId = attendanceDto.UserId,
                ActivityId = attendanceDto.ActivityId,
                IsAttending = attendanceDto.IsAttending
            };

            await _attendanceRepository.AddAttendanceAsync(newAttendance);
        }

        public async Task DeleteAttendanceAsync(int attendanceId)
        {
            var attendanceToDelete = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (attendanceToDelete == null)
            {
                throw new Exception($"Attendance with ID {attendanceId} does not exist.");
            }

            await _attendanceRepository.DeleteAttendanceAsync(attendanceToDelete);
        }

        public async Task EditAttendanceAsync(int attendanceId, AttendanceCreateEditDTO attendanceDto)
        {
            var chosenAttendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (chosenAttendance == null)
            {
                throw new Exception($"Attendance with ID {attendanceId} does not exist.");
            }

            //re-writing old values, using dto
            chosenAttendance.UserId = attendanceDto.UserId;
            chosenAttendance.ActivityId = attendanceDto.ActivityId;
            chosenAttendance.IsAttending = attendanceDto.IsAttending;

            await _attendanceRepository.EditAttendanceAsync(chosenAttendance);
        }

        public async Task<IEnumerable<AttendanceShowDTO>> GetAllAttendanceAsync()
        {
            var attendances = await _attendanceRepository.GetAllAttendanceAsync();

            //using data from user and activity-tables also
            return attendances.Select(a => new AttendanceShowDTO
            {
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                ActivityId = a.ActivityId,
                ActivityName = a.Activity.ActivityName,
                IsAttending = a.IsAttending

            }).ToList();
        }

        public async Task<AttendanceShowDTO> GetAttendanceByIdAsync(int attendanceId)
        {
            var chosenAttendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

            if (chosenAttendance == null)
            {
                throw new Exception($"Attendance with ID {attendanceId} does not exist.");
            }

            return new AttendanceShowDTO
            {
                FirstName = chosenAttendance.User.FirstName,
                LastName = chosenAttendance.User.LastName,
                ActivityId = chosenAttendance.ActivityId,
                ActivityName = chosenAttendance.Activity.ActivityName,
                IsAttending = chosenAttendance.IsAttending

            };

        }

        public async Task<IEnumerable<AttendanceShowDTO>> GetAttendanceByActivityAsync(int activityId)
        {
            var attendances = await _attendanceRepository.GetAttendanceByActivityAsync(activityId);

            //using data from user and activity-tables also
            return attendances.Select(a => new AttendanceShowDTO
            {
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                ActivityId = a.ActivityId,
                ActivityName = a.Activity.ActivityName,
                IsAttending = a.IsAttending

            }).ToList();
        }
    }
}
