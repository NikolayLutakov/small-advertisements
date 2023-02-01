using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmallAdvertisements.Data.Entities;

namespace SmallAdvertisements.Data.Context
{
    public class AdvertisementsDbContext : IdentityDbContext<User>
    {
        public AdvertisementsDbContext(DbContextOptions<AdvertisementsDbContext> options) : base(options)
        {

        }
    }
}
