using agendamento_recursos.DTOs.Booking;

namespace agendamento_recursos.Services.Booking
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<bool> DeleteBookingAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(int userId);
        Task<IEnumerable<BookingDto>> GetBookingsByResourceAsync(int resourceId);
        Task<IEnumerable<BookingDto>> GetBookingsByResourceAndDateAsync(int resourceId, DateTime date);
    }
}
