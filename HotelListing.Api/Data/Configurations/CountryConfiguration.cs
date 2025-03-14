using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Principal;

namespace HotelListing.Api.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "Cayman Islands",
                    ShortName = "CI"
                },
                new Country
                {
                    Id = 4,
                    Name = "Antigua and Barbuda",
                    ShortName = "AB"
                },
                new Country
                {
                    Id = 5,
                    Name = "Barbados",
                    ShortName = "BB"
                },
                new Country
                {
                    Id = 6,
                    Name = "Trinidad and Tobago",
                    ShortName = "TT"
                },
                new Country
                {
                    Id = 7,
                    Name = "St. Lucia",
                    ShortName = "LC"
                },
                new Country
                {
                    Id = 8,
                    Name = "St. Kitts and Nevis",
                    ShortName = "KN"
                },
                new Country
                {
                    Id = 9,
                    Name = "St. Vincent and the Grenadines",
                    ShortName = "VC"
                },
                new Country
                {
                    Id = 10,
                    Name = "Grenada",
                    ShortName = "GD"
                }
            );
        }
    }
}
