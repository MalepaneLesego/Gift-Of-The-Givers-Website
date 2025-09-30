using System.ComponentModel.DataAnnotations;

namespace FoundationWebApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime? LastLogin { get; set; }

        // Navigation properties
        public ICollection<Volunteer> Volunteers { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }
}
