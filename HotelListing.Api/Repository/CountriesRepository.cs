using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly HotelListingAPIDbContext _context;

    public CountriesRepository(HotelListingAPIDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<Country> GetDetails(int id)
    {
        var countryDetails = await _context.Countries
            .Include(q => q.Hotels)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (countryDetails == null)
        {
            return null;
        }
        return countryDetails;

    }
}