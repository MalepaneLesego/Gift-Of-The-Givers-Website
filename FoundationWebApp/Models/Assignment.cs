using System.ComponentModel.DataAnnotations;

namespace FoundationWebApp.Models
{
    public class Assignment
    {
        [Key]
        public int Assignment_ID { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime AssignedDate { get; set; }
        public string Status { get; set; }

        // Foreign keys
        public int Volunteer_ID { get; set; }
        public Volunteer Volunteer { get; set; }

        public int Disaster_ID { get; set; }
        public Disaster Disaster { get; set; }
    }
}
