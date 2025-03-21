﻿using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsStaticController : ControllerBase
{
    private static List<Hotel> hotels = new List<Hotel>
    {
        new Hotel { Id = 1, Name = "Hotel One", Address = "Address One", Rating = 4.5 },
        new Hotel { Id = 2, Name = "Hotel Two", Address = "Address Two", Rating = 4.0 },
        new Hotel { Id = 3, Name = "Hotel Three", Address = "Address Three", Rating = 3.5 },
        new Hotel { Id = 4, Name = "Hotel Four", Address = "Address Four", Rating = 3.0 },
        new Hotel { Id = 5, Name = "Hotel Five", Address = "Address Five", Rating = 2.5 }
    };

    // GET: api/<HotelsStaticController>
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get() // ActionResult is the standard return type for a controller
    {
        return Ok(hotels);
    }

    // GET api/<HotelsStaticController>/5
    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = hotels.FirstOrDefault(hotel => hotel.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return Ok(hotel);
    }

    // POST api/<HotelsStaticController>
    [HttpPost]
    public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
    {
        if (hotels.Any(h => h.Id == newHotel.Id))
        {
            return BadRequest("Hotel with this Id already exists");
        }

        hotels.Add(newHotel);
        return CreatedAtAction(nameof(Get), new { id = newHotel.Id}, newHotel);
    }

    // PUT api/<HotelsStaticController>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
    {
        var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
        {
            return NotFound();
        }

        existingHotel.Name = updatedHotel.Name;
        existingHotel.Address = updatedHotel.Address;
        existingHotel.Rating = updatedHotel.Rating;

        return NoContent();
    }

    // DELETE api/<HotelsStaticController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound(new { message = "Hotel not found" });
        }

        hotels.Remove(hotel);

        return NoContent();
    }
}
