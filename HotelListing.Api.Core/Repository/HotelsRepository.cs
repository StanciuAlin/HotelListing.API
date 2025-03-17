using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;

namespace HotelListing.Api.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingAPIDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
