using System.ComponentModel.DataAnnotations;

namespace Projektverwaltung.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
