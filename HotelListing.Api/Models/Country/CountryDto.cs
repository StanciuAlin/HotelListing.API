using HotelListing.Api.Models.Hotel;

namespace HotelListing.Api.Models.Country
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual List<HotelDto> Hotels { get; set; } = new List<HotelDto>();
    }
}
