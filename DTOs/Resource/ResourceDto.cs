namespace agendamento_recursos.DTOs.Resource
{
    public class ResourceDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int IntervalMinutes { get; init; }

    }

    public class ResourceAvailabilityDto
    {
        public int Id { get; }
        public string Name { get; } = null!;
        public int IntervalMinutes { get; }

        // disponiblidade baseado na data e hora
        public DateTime CheckedStartTime { get; }
        public DateTime CheckedEndTime { get; }
        public bool IsAvailable { get; }
   
    }
}
