using System.ComponentModel.DataAnnotations;

namespace FoundationWebApp.Models
{
    public class Donation
    {
        [Key]
        public int Donation_ID { get; set; }

        [Required]
        public string DonorName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        // Foreign keys
        public int Resource_ID { get; set; }
        public Resource Resource { get; set; }

        public int Disaster_ID { get; set; }
        public Disaster Disaster { get; set; }
    }
}
