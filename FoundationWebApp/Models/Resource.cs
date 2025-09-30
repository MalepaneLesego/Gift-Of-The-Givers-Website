using System.ComponentModel.DataAnnotations;

namespace FoundationWebApp.Models
{
    public class Resource
    {
        [Key]
        public int Resource_ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Type { get; set; }
        public int Quantity { get; set; }
        public string StorageLocation { get; set; }
        public DateTime LastUpdated { get; set; }

        // Foreign key
        public int UserID { get; set; }
        public User User { get; set; }

        // Navigation
        public ICollection<Donation> Donations { get; set; }
    }
}
