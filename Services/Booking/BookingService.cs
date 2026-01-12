using agendamento_recursos.DTOs.Booking;
using agendamento_recursos.Repository.Booking;
using agendamento_recursos.Repository.Resource;
using agendamento_recursos.Repository.User;

namespace agendamento_recursos.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IUserRepository userRepository;
        private readonly IResourceRepository resourceRepository;

        public BookingService
            (
            IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IResourceRepository resourceRepository
            )
        {
            this.bookingRepository = bookingRepository;
            this.userRepository = userRepository;
            this.resourceRepository = resourceRepository;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        {
            // 1. usuario existe?
            var user = await userRepository.GetByIdAsync(dto.UserId);
            if (user == null) throw new Exception("Usuário não encontrado");

            // 2. recurso existe?
            var resource = await resourceRepository.GetByIdAsync(dto.ResourceId);
            if (resource == null) throw new Exception("Recurso não encontrado");

            // 3. datas válidas?
            if (dto.StartTime >= dto.EndTime) throw new Exception("Data de início deve ser anterior à data de término");


            if (dto.StartTime < DateTime.Now)
                throw new Exception("Data no passado inválida.");

            var duration = dto.EndTime - dto.StartTime;

            // 4. verificar o limite de horas agendadas por usuario
            var bookingsOnDay = await bookingRepository.GetByUserAndDateAsync(dto.UserId, dto.StartTime);
            var totalMinutesBooked = bookingsOnDay.Sum(b => (b.EndTime - b.StartTime).TotalMinutes);
            var totalMinutesWithInterval = totalMinutesBooked + duration.TotalMinutes;

            if (totalMinutesWithInterval > user.MaxMinutesPerDay)
            {
                var minutesLeft = user.MaxMinutesPerDay - totalMinutesBooked;
                throw new Exception(
                    $"Você já tem {totalMinutesBooked} minutos agendados hoje. " +
                    $"Restam {minutesLeft} minutos disponíveis (máximo: {user.MaxMinutesPerDay} minutos/dia).");
            }

            // 5. adicionar intervalo de limpeza e verificar conflitos (apenas verificação)
            //var endTimeWithCleaning = dto.EndTime.AddMinutes(resource.IntervalMinutes);

            // 6. não pode ter conflito de horarios (ja inclui reservas no mesmo horario) (considerando intervalo de limpeza)
            var hasConflict = await bookingRepository.HasConflictAsync(
                dto.ResourceId,
                dto.StartTime.AddMinutes(-resource.IntervalMinutes + 1),
                dto.EndTime.AddMinutes(resource.IntervalMinutes - 1)
            );

            if (hasConflict)
                throw new Exception($"Conflito de horário detectado. " +
                    $"{resource.Name} tem {resource.IntervalMinutes} minutos de intervalo para limpeza entre cada sessão");

            // 7. cria agendamento de fato
            var booking = new Models.Booking
            {
                UserId = dto.UserId,
                ResourceId = dto.ResourceId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            var created = await bookingRepository.CreateAsync(booking);
            var result = await bookingRepository.GetByIdAsync(created.Id);
            return MapToDto(result!);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await bookingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await bookingRepository.GetAllAsync();
            return bookings.Select(MapToDto);
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            var booking = await bookingRepository.GetByIdAsync(id);
            return booking != null ? MapToDto(booking) : null;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(int userId)
        {
            var bookings = await bookingRepository.GetByUserIdAsync(userId);
            return bookings.Select(MapToDto);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByResourceAsync(int resourceId)
        {
            var bookings = await bookingRepository.GetByResourceIdAsync(resourceId);
            return bookings.Select(MapToDto);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByResourceAndDateAsync(int resourceId, DateTime date)
        {
            var bookings = await bookingRepository.GetByResourceAndDateAsync(resourceId, date);
            return bookings.Select(MapToDto);
        }

        private static BookingDto MapToDto(Models.Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                UserName = booking.User.Name,
                ResourceId = booking.ResourceId,
                ResourceName = booking.Resource.Name,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                DurationMinutes = (int)(booking.EndTime - booking.StartTime).TotalMinutes
            };
        }
    }
}
