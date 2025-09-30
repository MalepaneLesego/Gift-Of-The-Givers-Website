using System.ComponentModel.DataAnnotations;

namespace FoundationWebApp.Models
{
    public class Disaster
    {
        [Key]
        public int Disaster_ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        // Navigation
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Donation> Donations { get; set; }
    }
}
