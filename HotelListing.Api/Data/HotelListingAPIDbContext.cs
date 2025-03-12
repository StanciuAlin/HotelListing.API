using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Data;

// Represents the contract between the application and the database
public class HotelListingAPIDbContext : DbContext
{
    public HotelListingAPIDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>().HasData(
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
        modelBuilder.Entity<Hotel>().HasData(
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
