using agendamento_recursos.Models;

namespace agendamento_recursos.Repository.Booking
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Models.Booking>> GetAllAsync();
        Task<Models.Booking?> GetByIdAsync(int id);
        Task<Models.Booking> CreateAsync(Models.Booking booking);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Models.Booking>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Models.Booking>> GetByResourceIdAsync(int resourceId);
        Task<IEnumerable<Models.Booking>> GetByUserAndDateAsync(int userId, DateTime date);
        Task<bool> HasConflictAsync(int resourceId, DateTime startTime, DateTime endTime, int? excludeBookingId = null);
    }
}