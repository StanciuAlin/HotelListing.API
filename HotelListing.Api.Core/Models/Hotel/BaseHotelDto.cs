using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Models.Hotel
{
    public abstract class BaseHotelDto
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Range(0.0, 5.0)]
        public double? Rating { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; } // Make Required because it's a foreign key
    }
}