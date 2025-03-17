using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Api.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    Rating = 4.5,
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Grand Palladium",
                    Address = "Nassau",
                    Rating = 4.0,
                    CountryId = 2
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Marriott Beach Resort",
                    Address = "Grand Cayman",
                    Rating = 4.3,
                    CountryId = 3
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Jumby Bay",
                    Address = "Antigua",
                    Rating = 4.7,
                    CountryId = 4
                },
                new Hotel
                {
                    Id = 5,
                    Name = "Sandy Lane",
                    Address = "St. James",
                    Rating = 4.9,
                    CountryId = 5
                },
                new Hotel
                {
                    Id = 6,
                    Name = "Coco Reef Resort",
                    Address = "Crown Point",
                    Rating = 4.2,
                    CountryId = 6
                },
                new Hotel
                {
                    Id = 7,
                    Name = "Sugar Beach",
                    Address = "Val des Pitons",
                    Rating = 4.8,
                    CountryId = 7
                },
                new Hotel
                {
                    Id = 8,
                    Name = "Park Hyatt",
                    Address = "St. Kitts",
                    Rating = 4.6,
                    CountryId = 8
                },
                new Hotel
                {
                    Id = 9,
                    Name = "Young Island",
                    Address = "St. Vincent",
                    Rating = 4.4,
                    CountryId = 9
                }
            );
        }
    }
}
