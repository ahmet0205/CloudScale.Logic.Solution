using System.ComponentModel.DataAnnotations;

namespace CloudScale.Logic.API.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string TrackingNumber { get; set; } = string.Empty; // Örn: CS-987654

        [Required]
        public string SenderName { get; set; } = string.Empty;

        [Required]
        public string ReceiverName { get; set; } = string.Empty;

        public string CurrentStatus { get; set; } = "Package Received"; // Paket Alındı, Yolda, Teslim Edildi

        public string DestinationCity { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}