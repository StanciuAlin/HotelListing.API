using HotelListing.Api.Contracts;
using HotelListing.Api.Data;

namespace HotelListing.Api.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingAPIDbContext context) : base(context)
        {
        }
    }
}
