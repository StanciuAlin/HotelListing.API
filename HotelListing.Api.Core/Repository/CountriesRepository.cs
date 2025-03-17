using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Exceptions;
using HotelListing.Api.Models.Country;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly HotelListingAPIDbContext _context;
    private readonly IMapper _mapper;

    public CountriesRepository(HotelListingAPIDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<CountryDto> GetDetails(int id)
    {
        var country = await _context.Countries
            //.Include(q => q.Hotels)
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (country == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }
        return country;

    }
}