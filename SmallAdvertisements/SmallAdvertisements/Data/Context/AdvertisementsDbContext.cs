using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmallAdvertisements.Data.Context
{
    public class AdvertisementsDbContext : IdentityDbContext<IdentityUser>
    {
        public AdvertisementsDbContext(DbContextOptions<AdvertisementsDbContext> options) : base(options)
        {

        }
    }
}
