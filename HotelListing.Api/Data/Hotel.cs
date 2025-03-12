﻿namespace HotelListing.Api.Data;

// Data is also called a resource in RESTful API and I need an endpoint to access/interact this resource
public class Hotel 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
}
