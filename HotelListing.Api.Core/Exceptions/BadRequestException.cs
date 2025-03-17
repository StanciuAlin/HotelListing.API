namespace HotelListing.Api.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name, object key) : base($"Bad request for entity {name} with key ({key}).")
        {

        }
    }
}
