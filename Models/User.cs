using System.ComponentModel.DataAnnotations;

namespace agendamento_recursos.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public int MaxMinutesPerDay { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
