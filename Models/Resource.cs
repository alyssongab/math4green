using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace agendamento_recursos.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        public Boolean isAvailable { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
