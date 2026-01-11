
using agendamento_recursos.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace agendamento_recursos.Repository.Booking
{
    public class BookingRepository : IBookingRepository
    {

        public readonly AppDbContext context;

        public BookingRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Models.Booking>> GetAllAsync()
        {
            return await context.Bookings
                .Include(b => b.User)
                .Include(b => b.Resource)
                .ToListAsync();
        }
        public async Task<Models.Booking?> GetByIdAsync(int id)
        {
            return await context.Bookings
                .Include(b => b.User)
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Models.Booking> CreateAsync(Models.Booking booking)
        {
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking == null) return false;

            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Models.Booking>> GetByUserIdAsync(int userId)
        {
            return await context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Resource)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.Booking>> GetByResourceIdAsync(int resourceId)
        {
            return await context.Bookings
                .Where(b => b.ResourceId == resourceId)
                .Include(b => b.User)
                .OrderBy(b => b.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.Booking>> GetByUserAndDateAsync(int userId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await context.Bookings
                .Where(b => b.UserId == userId &&
                           b.StartTime >= startOfDay &&
                           b.StartTime < endOfDay)
                .ToListAsync();
        }

        public async Task<bool> HasConflictAsync(int resourceId, DateTime startTime, DateTime endTime, int? excludeBookingId = null)
        {
            // starttime e endtime já tem o intervalos somados
            // conflito 1: hora inicio da nova reserva < hora final da reserva encontrada
            // conflito 2: hora fim da nova reserva > hora inicio da reserva encontrada
            var query = context.Bookings
                .Where(b => b.ResourceId == resourceId)
                .Where(b => startTime <= b.EndTime && endTime >= b.StartTime);

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            return await query.AnyAsync();
        }

    }
}
