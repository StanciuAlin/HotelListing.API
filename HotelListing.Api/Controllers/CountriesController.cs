using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;
using AutoMapper;
using HotelListing.Api.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using HotelListing.Api.Common;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICountriesRepository _countriesRepository;

    public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
    {
        this._mapper = mapper;
        this._countriesRepository = countriesRepository;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
    {
        // <=> SELECT * FROM Countries
        //return await _context.Countries.ToListAsync(); // To be more explicit, we could use return Ok(await ..) 
        var countries = await _countriesRepository.GetAllAsync();
        var records = _mapper.Map<IList<GetCountryDto>>(countries).ToList();
        return Ok(records);
    }

    // When delete the template by mistake and write only [HttpGet], we see the Microsoft.AspNetCore.Routing.Matchin.AmbiguousMatchException: The request matched multiple endpoints. Matches:
    //     'Function 1'
    //     'Function 2'
    //  To fix this, we need to add the [HttpGet("{id}")] attribute to the GetCountry method
    //
    // Extend: [HttpGet("{id}/hotelId/{hotelId}")], GetCountry(int id, int hotelId)
    //     then get GET: api/Countries/5/hotelId/1

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDto>> GetCountry(int id)
    {
        var country = await _countriesRepository.GetDetails(id);

        if (country == null)
        {
            return NotFound();
        }
        var record = _mapper.Map<CountryDto>(country);

        return Ok(record);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
    {
        if (id != updateCountryDto.Id)
        {
            return BadRequest("Invalid Record Id");
        }

        // _context.Entry(country).State = EntityState.Modified;

        var country = await _countriesRepository.GetAsync(id); // because we retrieved this data, EF tracks this data
        if (country == null)
        {
            return NotFound();
        }

        // This mapping line told EF that the country has been modified and changed the state to Modified
        _mapper.Map(updateCountryDto, country);

        try
        {
            // If two users try to update the same record at the same time,
            //   the first one will succeed and the second one will fail
            await _countriesRepository.UpdateAsync(country);
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check to see if the country exists at all, because maybe you would have loaded the country
            //   and you were editing where somebody else also edited/deleted the country
            if (!await _countriesRepository.Exists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto) // Changed from Country model to CreateCountryDto DTO model (view-model) 
                                                                                            // preventing the user from submitting data that we don't want or could harmful that could pottentially be harmful for our system
    {
        //var country = new Country
        //{
        //    Name = createCountryDto.Name,
        //    ShortName = createCountryDto.ShortName
        //};

        // Now, preventing Overposting
        var country = _mapper.Map<Country>(createCountryDto);
        await _countriesRepository.AddAsync(country);

        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _countriesRepository.GetAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        await _countriesRepository.DeleteAsync(id);

        return NoContent();
    }
}
