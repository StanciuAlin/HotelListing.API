using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Principal;

namespace HotelListing.Api.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "841673db-b543-4d16-b612-719d66ddc4e6",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "d0437005-a5d0-4110-9c42-67fc8ec12113",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}
