using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace agendamento_recursos.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public int IntervalMinutes { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
