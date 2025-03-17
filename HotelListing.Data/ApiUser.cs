using Microsoft.AspNetCore.Identity;

namespace HotelListing.Api.Data
{
    public class ApiUser : IdentityUser // custom user type
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
