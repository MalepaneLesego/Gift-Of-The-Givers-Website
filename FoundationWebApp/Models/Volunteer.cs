using System.ComponentModel.DataAnnotations;

namespace FoundationWebApp.Models
{
    public class Volunteer
    {
        [Key]
        public int Volunteer_ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Skills { get; set; }
        public string Location { get; set; }
        public bool AvailabilityStatus { get; set; }

        // Foreign key
        public int UserID { get; set; }
        public User User { get; set; }

        // Navigation
        public ICollection<Assignment> Assignments { get; set; }
    }
}
