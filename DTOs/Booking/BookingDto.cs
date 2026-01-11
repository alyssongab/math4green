namespace agendamento_recursos.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public string UserName { get; init; } = null!;

        public int ResourceId { get; init; }
        public string ResourceName { get; init; } = null!;
        public DateTime StartTime {  get; init; }
        public DateTime EndTime { get; init; }
        public int DurationMinutes { get; init; }
    }

    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int ResourceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
