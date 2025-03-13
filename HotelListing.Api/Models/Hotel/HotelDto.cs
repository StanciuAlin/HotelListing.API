using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Api.Models.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int Id { get; set; }
    }

    public class UpdateHotelDto : BaseHotelDto // Now, it is not needed to have a separate class for UpdateHotelDto, but it is a good practice to have it. Now, use HotelDto
    {
    }
}