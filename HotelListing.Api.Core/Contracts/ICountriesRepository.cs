using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;

namespace HotelListing.Api.Contracts;

public interface ICountriesRepository : IGenericRepository<Country>
{
    Task<CountryDto> GetDetails(int id);
}